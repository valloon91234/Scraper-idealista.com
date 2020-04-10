using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace idealista
{
    static class Config
    {
        public const String FILE_SETTING = "config.ini";
        public const String KEY_DB_HOST = "db.host";
        public const String KEY_DB_PORT = "db.port";
        public const String KEY_DB_USER = "db.user";
        public const String KEY_DB_PASSWORD = "db.password";
        public const String KEY_DB_DATABASE = "db.database";

        public static String Get(String key, String defaultValue = null)
        {
            try
            {
                String[] lines = File.ReadAllLines(FILE_SETTING, Encoding.UTF8);
                foreach (String line in lines)
                {
                    if (line.StartsWith("#") || line.StartsWith("//")) continue;
                    String[] array = line.Split(new Char[] { '=' }, 2);
                    String name = array[0];
                    String value = array.Length > 1 ? array[1] : null;
                    if (name == key)
                    {
                        if (String.IsNullOrWhiteSpace(value))
                            return defaultValue;
                        return value.Trim();
                    }
                }
            }
            catch { }
            return defaultValue;
        }

        public static T Get<T>(String key, T defaultValue = default(T))
        {
            try
            {
                String[] lines = File.ReadAllLines(FILE_SETTING, Encoding.UTF8);
                foreach (String line in lines)
                {
                    if (line.StartsWith("#") || line.StartsWith("//")) continue;
                    String[] array = line.Split(new Char[] { '=' }, 2);
                    String name = array[0];
                    String value = array.Length > 1 ? array[1] : null;
                    if (name == key)
                    {
                        if (String.IsNullOrWhiteSpace(value))
                            return defaultValue;
                        return (T)Convert.ChangeType(value.Trim(), typeof(T));
                    }
                }
            }
            catch { }
            return defaultValue;
        }

        public static Boolean Write(String key, Object value)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(FILE_SETTING);
                if (fileInfo.Exists)
                {
                    String[] lines = File.ReadAllLines(FILE_SETTING, Encoding.UTF8);
                    using (StreamWriter file = new StreamWriter(FILE_SETTING, false, Encoding.UTF8))
                    {
                        Boolean foundKey = false;
                        foreach (String line in lines)
                        {
                            if (line.StartsWith("#") || line.StartsWith("//"))
                            {
                                file.WriteLine(line);
                                continue;
                            }
                            String[] array = line.Split(new Char[] { '=' }, 2);
                            String name = array[0];
                            String value0 = array.Length > 1 ? array[1] : null;
                            if (name == key)
                            {
                                foundKey = true;
                                file.WriteLine(name + "=" + value);
                            }
                            else
                            {
                                file.WriteLine(line);
                            }
                        }
                        if (!foundKey) file.WriteLine(key + "=" + value);
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(FILE_SETTING, false, Encoding.UTF8))
                    {
                        file.WriteLine(key + "=" + value);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
