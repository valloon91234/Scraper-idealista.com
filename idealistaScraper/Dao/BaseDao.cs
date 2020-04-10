using MySql.Data.MySqlClient;
using System;
using System.Globalization;

namespace idealista.Dao
{
    abstract class BaseDao : IDisposable
    {
        public const String DEFAULT_HOST = "localhost";
        public const int DEFAULT_PORT = 3306;
        public const String DEFAULT_USER = "root";
        public const String DEFAULT_PASSWORD = "";
        public const String DEFAULT_DATABASE = "ilog";

        public const String DATE_FORMAT = @"yyyy-MM-dd";
        public const String DATETIME_FORMAT = @"yyyy-MM-dd HH:mm:ss";
        public MySqlConnection connection { get; }
        public BaseDao()
        {
            String dbHost = Config.Get(Config.KEY_DB_HOST, DEFAULT_HOST);
            int dbPort = Config.Get<int>(Config.KEY_DB_PORT, DEFAULT_PORT);
            String dbUser = Config.Get(Config.KEY_DB_USER, DEFAULT_USER);
            String dbPassword = Config.Get(Config.KEY_DB_PASSWORD, DEFAULT_PASSWORD);
            String dbDatabase = Config.Get(Config.KEY_DB_PASSWORD, DEFAULT_DATABASE);
            String connectionString = $"server={dbHost};user={dbUser};database={dbDatabase};port={dbPort};password={dbPassword}";
            connection = new MySqlConnection(connectionString);
#if DEBUG
            Console.WriteLine("Connection has created.");
#endif
        }

        public void Dispose()
        {
            connection.Close();
            connection.Dispose();
        }

        public static T GetValue<T>(object obj)
        {
            Type type = typeof(T);
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else if (type == typeof(DateTime))
            {
                return (T)(Object)DateTime.ParseExact((String)obj, DATETIME_FORMAT, CultureInfo.CurrentCulture);
            }
            else
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
        }

        //public static String ToDateString(DateTime dt)
        //{
        //    return dt.ToString(DATE_FORMAT);
        //}

        //public static DateTime ParseDateString(String s)
        //{
        //    return DateTime.ParseExact(s, DATE_FORMAT, CultureInfo.CurrentCulture);
        //}

        //public static String ToDateTimeString(DateTime dt)
        //{
        //    return dt.ToString(DATETIME_FORMAT);
        //}

        //public static DateTime ParseDateTimeString(String s)
        //{
        //    return DateTime.ParseExact(s, DATETIME_FORMAT, CultureInfo.CurrentCulture);
        //}

    }
}
