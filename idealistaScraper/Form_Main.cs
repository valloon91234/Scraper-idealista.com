using idealista.Dao;
using idealista.Service;
using System;
using System.Windows.Forms;

namespace idealista
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }



        private void Form_Main_Load(object sender, EventArgs e)
        {
            String dbHost = Config.Get(Config.KEY_DB_HOST);
            String dbPort = Config.Get(Config.KEY_DB_PORT);
            String dbUser = Config.Get(Config.KEY_DB_USER);
            String dbPassword = Config.Get(Config.KEY_DB_PASSWORD);
            String dbDatabase = Config.Get(Config.KEY_DB_DATABASE);
            textBox_DBHost.Text = dbHost == null ? BaseDao.DEFAULT_HOST : dbHost;
            textBox_DBPort.Text = dbPort == null ? BaseDao.DEFAULT_PORT.ToString() : dbPort;
            textBox_DBUser.Text = dbUser == null ? BaseDao.DEFAULT_USER : dbUser;
            textBox_DBPassword.Text = dbPassword == null ? BaseDao.DEFAULT_PASSWORD : dbPassword;
            textBox_DBDatabase.Text = dbDatabase == null ? BaseDao.DEFAULT_DATABASE : dbDatabase;
        }

        private void button_SaveSetting_Click(object sender, EventArgs e)
        {
            String dbHost = textBox_DBHost.Text;
            String dbPort = textBox_DBPort.Text;
            String dbUser = textBox_DBUser.Text;
            String dbPassword = textBox_DBPassword.Text;
            String dbDatabase = textBox_DBDatabase.Text;
            if (dbHost != null) Config.Write(Config.KEY_DB_HOST, dbHost);
            if (dbPort != null) Config.Write(Config.KEY_DB_PORT, dbPort);
            if (dbUser != null) Config.Write(Config.KEY_DB_USER, dbUser);
            if (dbPassword != null) Config.Write(Config.KEY_DB_PASSWORD, dbPassword);
            if (dbDatabase != null) Config.Write(Config.KEY_DB_DATABASE, dbDatabase);
        }

        public const String COUNTRY_NAME = "Spain";
        public const String INTERNAL_PREFIX = "idl";

        private void button_Start_Click(object sender, EventArgs e)
        {
            String baseURL = "/venta-terrenos/madrid-provincia/";
            String baseDownloadDirectory = textBox_RootPath.Text.Trim();
            String cookie = textBox_Cookie.Text.Trim();
            Boolean forceUpdate = checkBox_ForceUpdate.Checked;
            Scraper scraper = new Scraper(COUNTRY_NAME, INTERNAL_PREFIX, baseURL, baseDownloadDirectory, cookie);
            scraper.ScrapeStart(forceUpdate);
        }

    }
}
