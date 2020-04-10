using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace idealista.Service
{
    class HttpClient
    {
        public static void Print(String text, ConsoleColor? color = null)
        {
            if (color == null)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)color;
                Console.WriteLine(text);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private String Cookie = "_pxhd=f1965837c42913427065dc4a806b59d84b132289d29304150baea6b547fcf5a1:c38d05c1-2e97-11ea-b904-65e863ab515c; atuserid=%7B%22name%22%3A%22atuserid%22%2C%22val%22%3A%22c989ef98-f2a9-4c1e-bca4-2ef5ada57f7f%22%2C%22options%22%3A%7B%22end%22%3A%222021-02-04T02%3A12%3A23.783Z%22%2C%22path%22%3A%22%2F%22%7D%7D; atidvisitor=%7B%22name%22%3A%22atidvisitor%22%2C%22val%22%3A%7B%22vrn%22%3A%22-582065-%22%7D%2C%22options%22%3A%7B%22path%22%3A%22%2F%22%2C%22session%22%3A15724800%2C%22end%22%3A15724800%7D%7D; utag_main=v_id:016f6e532f55002bdfc5df62b68803072004f06a00bd0$_sn:1$_ss:1$_st:1578105742999$ses_id:1578103942999%3Bexp-session$_pn:1%3Bexp-session$dc_visit:1$dc_event:1%3Bexp-session$dc_region:eu-central-1%3Bexp-session; _pxvid=c38d05c1-2e97-11ea-b904-65e863ab515c; _hjid=903c8a5c-7993-4974-b818-09c90ef5b922; _px2=eyJ1IjoiYzM4ZDA1YzAtMmU5Ny0xMWVhLWI5MDQtNjVlODYzYWI1MTVjIiwidiI6ImMzOGQwNWMxLTJlOTctMTFlYS1iOTA0LTY1ZTg2M2FiNTE1YyIsInQiOjE1NzgxMDQ1NDExOTMsImgiOiJjZWZlMzc3MjI5NjA2YWU5ZmI5YjM3ZjUzNDEzNTY4MzkxMTg3MDVhNDM5MzMyMDA5MTEwZDBmNmU2NzE3YWY1In0=";
        private CookieCollection Cookies = null;
        public int NextProxyIndex = 0;
        public String[] ProxyArray = null;

        public HttpClient(String Cookie = null)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            this.Cookie = Cookie;
        }

        public String RequestGet(String url, int proxyIndex = -1)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = 10000;
            httpWebRequest.ReadWriteTimeout = 10000;
            httpWebRequest.UseDefaultCredentials = true;
            if (Cookies != null)
            {
                CookieContainer cookieContainer = new CookieContainer();
                cookieContainer.Add(Cookies);
                httpWebRequest.CookieContainer = cookieContainer;
            }
            else if (!String.IsNullOrWhiteSpace(Cookie))
            {
                httpWebRequest.Headers.Add("Cookie", Cookie);
            }
            if (proxyIndex >= 0)
            {
                String p = ProxyArray[proxyIndex];
                String[] pArray = p.Split(':');
                WebProxy myproxy = new WebProxy(pArray[0], Convert.ToInt32(pArray[1]));
                myproxy.BypassProxyOnLocal = false;
                myproxy.Credentials = CredentialCache.DefaultCredentials;
                httpWebRequest.Proxy = myproxy;
            }
            httpWebRequest.Method = "GET";
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            //httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36";

            String headers = @"Purpose: prefetch
Sec-Fetch-Site: none
Sec-Fetch-Mode: navigate
Accept-Language: en-US,en;q=0.9
";
            String[] listOfHeaders = headers.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var header in listOfHeaders)
                httpWebRequest.Headers.Add(header);
            try
            {
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    Cookies = httpWebResponse.Cookies;
                    //String cookieNew = httpWebResponse.Headers.Get("Set-Cookie");
                    //if (cookieNew != null)
                    //    Cookie = cookieNew;
                    using (Stream receiveStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                var httpWebResponse = (HttpWebResponse)ex.Response;
                if (httpWebResponse != null)
                {
                    HttpStatusCode statusCode = httpWebResponse.StatusCode;
                    Print(ex.Message, ConsoleColor.Red);
                    if (statusCode == HttpStatusCode.Forbidden)
                    {

                    }
                    Cookies = httpWebResponse.Cookies;
                    using (Stream receiveStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            //Print(streamReader.ReadToEnd());
                        }
                    }
                }
                throw;
            }
        }

        public void DownloadFile(String url, String fileName)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = 10000;
            httpWebRequest.ReadWriteTimeout = 10000;
            httpWebRequest.Method = "GET";
            //httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36";
            using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var stream = File.Create(fileName))
                {
                    httpWebResponse.GetResponseStream().CopyTo(stream);
                }
            }
        }
    }
}
