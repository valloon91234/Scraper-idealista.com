using HtmlAgilityPack;
using idealista.Dao;
using idealista.Model;
using Newtonsoft.Json.Linq;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace idealista.Service
{
    class Scraper
    {
        public const String HOST_ADDRESS = "https://www.idealista.com";
        public const int PAGE_COUNT_MAX = 4;// 60;
        public String CountryName;
        public String InternalPrefix;
        public String BaseURL;
        public String BaseDownloadDirectory;
        private HttpClient httpClient;

        public Scraper(String CountryName, String InternalPrefix, String BaseURL, String BaseDownloadDirectory = null, String cookie = null)
        {
            this.CountryName = CountryName;
            this.InternalPrefix = InternalPrefix;
            this.BaseURL = BaseURL;
            this.BaseDownloadDirectory = BaseDownloadDirectory;
            httpClient = new HttpClient(cookie);
        }

        public static String GetNumbers(String input)
        {
            return new String(input.Where(c => char.IsDigit(c)).ToArray());
        }

        public List<String> ScrapeURLFormListPage(String url, out int count)
        {
            List<String> urlList = new List<string>();
            String page = httpClient.RequestGet(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlNode documentNode = doc.DocumentNode;
            {
                var node = documentNode.CssSelect("#h1-container").First();
                String text = node.GetDirectInnerText();
                count = Convert.ToInt32(GetNumbers(text));
            }
            {
                var nodes = documentNode.CssSelect("#main-content a.item-link");
                foreach (HtmlNode node in nodes)
                {
                    String href = node.GetAttributeValue("href");
                    urlList.Add(href);
                }
            }
            return urlList;
        }

        public List<String> ScrapeURLALL(String usage, String usageURL)
        {
            const int ROWS = 30;
            List<String> urlList = new List<string>();
            int count;
            while (true)
            {
                String url = HOST_ADDRESS + BaseURL + usageURL + "/";
                HttpClient.Print($"Scraping list for \"{usage}\"\t{ROWS}\t\t{BaseURL}{usageURL}/");
                try
                {
                    List<String> list = ScrapeURLFormListPage(url, out count);
                    urlList.AddRange(list);
                    break;
                }
                catch (Exception e)
                {
                    HttpClient.Print(e.Message, ConsoleColor.Red);
                    //return urlList;
                }
            }
            int pageCount = count / ROWS;
            if (pageCount > PAGE_COUNT_MAX) pageCount = PAGE_COUNT_MAX;
            for (int page = 2; page <= pageCount; page++)
            {
                String url = HOST_ADDRESS + BaseURL + $"{usageURL}/pagina-{page}.htm";
                HttpClient.Print($"Scraping list for \"{usage}\"\t{page * ROWS} / {(count)}\t{BaseURL}{usageURL}/");
                List<String> list = ScrapeURLFormListPage(url, out count);
                urlList.AddRange(list);
            }
            return urlList;
        }

        public void ScrapePages(List<String> urlList, String usage, Boolean forceUpdate)
        {
            int urlCount = urlList.Count;
            using (PropertybDao dao = new PropertybDao())
            {
                for (int i = 0; i < urlCount; i++)
                {
                    String url = urlList[i];
                    String page = httpClient.RequestGet(HOST_ADDRESS + url);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(page);
                    HtmlNode documentNode = doc.DocumentNode;
                    Propertyb m = new Propertyb();
                    {
                        // SourceID
                        var nodes = documentNode.CssSelect("input[name='adId']");
                        if (nodes.Count() < 1) throw new NullReferenceException("SourceID not found.");
                        var node = nodes.First();
                        m.SourceID = node.GetAttributeValue("value");
                    }
                    {
                        // Price
                        var nodes = documentNode.CssSelect("div.toggle-price section.price-features__container p strong");
                        if (nodes.Count() < 1) throw new NullReferenceException("Price not found.");
                        var node = nodes.First();
                        m.Price = Convert.ToInt32(GetNumbers(node.GetDirectInnerText()));
                    }
                    {
                        // Country
                        m.Country = CountryName;
                    }
                    {
                        // City
                        var nodes = documentNode.CssSelect("div#headerMap ul li");
                        if (nodes.Count() < 1) throw new NullReferenceException("City not found.");
                        var node = nodes.Last();
                        String location = node.GetDirectInnerText();
                        String[] array = location.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        m.City = array.Last().Trim();
                    }
                    {
                        // URLPost
                        m.URLPost = url;
                    }
                    {
                        // InternalPrefix
                        m.InternalPrefix = InternalPrefix;
                    }
                    {
                        // ContactName
                        var nodes = documentNode.CssSelect("aside.side-content section a.about-advertiser-name");
                        if (nodes.Count() > 0)
                        {
                            var node = nodes.First();
                            m.ContactName = node.GetDirectInnerText().Trim();
                        }
                    }
                    // contact email //TODO
                    // contact mobile //TODO
                    {
                        // Title
                        var nodes = documentNode.CssSelect("div.main-info__title span.main-info__title-main");
                        if (nodes.Count() < 1) throw new NullReferenceException("Title not found.");
                        var node = nodes.First();
                        m.Title = node.GetDirectInnerText().Trim();
                    }
                    {
                        // Address
                        var nodes = documentNode.CssSelect("div#headerMap ul li");
                        if (nodes.Count() < 1) throw new NullReferenceException("Address not found.");
                        var node = nodes.First();
                        String address = node.GetDirectInnerText().Trim();
                        m.Address = address + ", " + m.City;
                    }
                    {
                        // Usage
                        m.Usage = usage;
                    }
                    {
                        //Description
                        var nodes = documentNode.CssSelect("div.commentsContainer div.adCommentsLanguage");
                        if (nodes.Count() < 1) throw new NullReferenceException("Description not found.");
                        var node = nodes.First();
                        m.Description = node.InnerText.Trim();
                    }
                    // InitialPrice
                    {
                        var nodes = documentNode.CssSelect("section#simulator-container div.item-form");
                        if (nodes.Count() < 1) throw new NullReferenceException("Last Price not found.");
                        nodes = nodes.First().CssSelect("span");
                        if (nodes.Count() < 1) throw new NullReferenceException("Last Price not found.");
                        var node = nodes.Last();
                        m.LastPrice = Convert.ToInt32(GetNumbers(node.GetDirectInnerText()));
                    }
                    // NumberPriceChanges
                    {
                        // Expenses
                        var nodes = documentNode.CssSelect("section#simulator-container div.item-form.js-taxes");
                        if (nodes.Count() < 1) throw new NullReferenceException("Expenses Price not found.");
                        nodes = nodes.First().CssSelect("span");
                        if (nodes.Count() < 1) throw new NullReferenceException("Expenses Price not found.");
                        var node = nodes.Last();
                        m.Expenses = Convert.ToInt32(GetNumbers(node.GetDirectInnerText()));
                    }
                    {
                        // PriceWithExp
                        var nodes = documentNode.CssSelect("section#simulator-container div.item-form.js-price-with-expenses");
                        if (nodes.Count() < 1) throw new NullReferenceException("PriceWithExp not found.");
                        nodes = nodes.First().CssSelect("span");
                        if (nodes.Count() < 1) throw new NullReferenceException("PriceWithExp not found.");
                        var node = nodes.Last();
                        m.PriceWithExp = Convert.ToInt32(GetNumbers(node.GetDirectInnerText()));
                    }
                    {
                        // BasicCharacteristics
                        var nodes = documentNode.CssSelect("section#details div.details-property-feature-one div.details-property_features");
                        if (nodes.Count() > 0)
                        {
                            nodes = nodes.First().CssSelect("ul");
                            if (nodes.Count() > 0)
                            {
                                var node = nodes.First();
                                m.BasicCharacteristics = node.InnerText.Trim();
                            }
                        }
                    }
                    {
                        // TotalSurface, MiniumSellSurface, SurfaceEdificable, Access
                        var nodes = documentNode.CssSelect("section#details div.details-property-feature-one div.details-property_features ul");
                        if (nodes.Count() > 0)
                        {
                            nodes = nodes.First().CssSelect("li");
                            if (nodes.Count() > 0)
                            {
                                foreach (var node in nodes)
                                {
                                    String text = node.GetDirectInnerText();
                                    if (text.Contains("total"))
                                        m.TotalSurface = Convert.ToInt32(GetNumbers(text));
                                    else if (text.Contains("minima"))
                                        m.MiniumSellSurface = Convert.ToInt32(GetNumbers(text));
                                    else if (text.Contains("edificable"))
                                        m.SurfaceEdificable = Convert.ToInt32(GetNumbers(text));
                                    else if (text.Contains("Acceso via"))
                                        m.Access = text.Substring("Acceso via ".Length).Trim();
                                }
                            }
                        }
                    }
                    {
                        // UrbanisticSituation
                        var nodes = documentNode.CssSelect("section#details div.details-property-feature-two div.details-property_features ul");
                        if (nodes.Count() > 0)
                        {
                            var node = nodes.First();
                            m.UrbanisticSituation = node.InnerText.Trim();
                        }
                    }
                    {
                        // Equipment
                        var nodes = documentNode.CssSelect("section#details div.details-property-feature-two div.details-property_features ul");
                        if (nodes.Count() > 1)
                        {
                            var node = nodes.ElementAt(1);
                            m.Equipment = node.InnerText.Trim();
                        }
                    }

                    // postal code  //TODO
                    // lat, long
                    // image, video //

                    Propertyb o = dao.Select(m.SourceID);
                    if (o == null)
                    {
                        dao.Insert(m);
                        HttpClient.Print($"Added\t\t\"{usage}\" ({i + 1}/{urlCount}) : {url}");
                    }
                    else if (forceUpdate)
                    {
                        if (m.LastPrice != o.LastPrice)
                        {
                            m.InitialPrice = o.LastPrice;
                            m.NumberPriceChanges++;
                            dao.Update(m);
                            HttpClient.Print($"Updated + Price\t\"{usage}\" ({i + 1}/{urlCount}) : {url}");
                        }
                        else
                        {
                            dao.Update(m);
                            HttpClient.Print($"Updated (Force)\t\"{usage}\" ({i + 1}/{urlCount}) : {url}");
                        }
                    }
                    else if (m.LastPrice != o.LastPrice)
                    {
                        m.InitialPrice = o.LastPrice;
                        m.NumberPriceChanges++;
                        dao.Update(m);
                        HttpClient.Print($"Updated + Price\t\"{usage}\" ({i + 1}/{urlCount}) : {url}");
                    }

                    if (forceUpdate && o != null)
                    {
                        String pattern = "var adMultimediasInfo=";
                        int p1 = page.IndexOf(pattern);
                        p1 += pattern.Length;
                        int p2 = page.IndexOf(";", p1);
                        String jsonString = page.Substring(p1, p2 - p1);
                        var json = JObject.Parse(jsonString);
                        JArray array = (JArray)json["fullScreenGalleryPics"];
                        if (array.Count > 0)
                        {
                            String downloadPath = BaseDownloadDirectory + $"\\{InternalPrefix}{m.SourceID}";
                            DirectoryInfo downloadDirectoryInfo = new DirectoryInfo(downloadPath);
                            if (!downloadDirectoryInfo.Exists) downloadDirectoryInfo.Create();
                            String image = "";
                            foreach (JToken j in array)
                            {
                                String src = (String)j["src"];
                                String filename = Path.GetFileName(src);
                                image += filename + ";";
                                HttpClient.Print($"\tDownloading : " + src);
                                httpClient.DownloadFile(src, filename);
                            }
                        }
                    }
                }
            }
        }

        private int ThreadCount = 0;

        public void ScrapeThread(int usageIndex, Boolean forceUpdate)
        {
            ThreadCount++;
            String usage = Propertyb.USAGE_ARRAY[usageIndex];
            String usageURL = Propertyb.USAGE_URL_ARRAY[usageIndex];
            List<String> urlList = ScrapeURLALL(usage, usageURL);
            ScrapePages(urlList, usage, forceUpdate);
            ThreadCount--;
            if (ThreadCount < 1)
            {
                HttpClient.Print("- Completed -");
            }
        }

        public void ScrapeStart(Boolean forceUpdate = false)
        {
            int USAGE_COUNT = Propertyb.USAGE_ARRAY.Length;
            USAGE_COUNT = 1;
            for (int usageIndex = 0; usageIndex < USAGE_COUNT; usageIndex++)
            {
                int k = usageIndex;
                Thread thread = new Thread(() => ScrapeThread(k, forceUpdate));
                thread.Start();
            }
        }

    }
}
