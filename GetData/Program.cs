using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Threading;
using System.Configuration;
using System.Xml.Linq;

namespace GetData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string strConn = ConfigurationManager.AppSettings["strConn"].ToString();
            string url_Phim18 = ConfigurationManager.AppSettings["url_Phim18"].ToString();
            string url_PhimCR = ConfigurationManager.AppSettings["url_PhimCR"].ToString();
            int soPage_Phim18 = int.Parse(ConfigurationManager.AppSettings["soPage_Phim18"].ToString());
            int soPage_PhimCR = int.Parse(ConfigurationManager.AppSettings["soPage_PhimCR"].ToString());

            Console.WriteLine("Chọn: 1 Để lấy video Phim 18");
            Console.WriteLine("Chọn: 2 Để lấy video Phim Chiếu Rạp");
            string mode = Console.ReadLine();

            if (mode == "1") GetDataPhim18(url_Phim18, soPage_Phim18, strConn);
            else if (mode == "2") GetDataPhimCR(url_PhimCR, soPage_PhimCR, strConn);
        }

        private static void GetDataPhim18(string url, int soPage, string strConn)
        {
            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {
                conn.Open();
                string queryDelete = "DELETE FROM tblPhim18";
                using (SQLiteCommand cmdDelete = new SQLiteCommand(queryDelete, conn))
                {
                    int check = cmdDelete.ExecuteNonQuery();
                    if (check > 0)
                    {
                        Console.WriteLine("Xóa table Phim18");
                    }
                }

                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless");

                IWebDriver web = new ChromeDriver(chromeDriverService, chromeOptions);

                using (WebClient webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    for (int i = 1; i <= soPage; i++)
                    {
                        string urlLast = url + i;
                        string html = webClient.DownloadString(urlLast);
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(html);

                        var ul = htmlDocument.DocumentNode.Descendants("ul").Where(uls => uls.Attributes.Contains("class") && uls.Attributes["class"].Value == "list-movies");
                        foreach (var lis in ul)
                        {
                            var li = lis.Descendants("li").Where(lii => lii.Attributes["class"].Value == "item-movie");
                            foreach (var db in li)
                            {
                                var a = db.Descendants("a").FirstOrDefault();
                                string title = a.InnerText.Trim();
                                string linkTitle = a.Attributes["href"].Value;
                                var iamge = db.Descendants("div").Where(image => image.Attributes.Contains("class") && image.Attributes["class"].Value == "movie-thumbnail").FirstOrDefault();
                                string linkImage = iamge.Attributes["style"].Value.Replace("background-image:url('", "").Replace("')", "");

                                web.Navigate().GoToUrl(linkTitle);

                                WebDriverWait wait = new WebDriverWait(web, TimeSpan.FromSeconds(10));
                                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                                string htmlContent = web.PageSource;

                                web.Quit();

                                HtmlDocument doc = new HtmlDocument();
                                doc.LoadHtml(htmlContent);

                                var ifarme = doc.DocumentNode.Descendants("div").Where(_ifarme => _ifarme.Attributes.Contains("class") && _ifarme.Attributes["class"].Value == "pframe").FirstOrDefault();
                                string link_video = "";
                                try
                                {
                                    var div = doc.DocumentNode.Descendants("div").Where(_div => _div.Attributes.Contains("id") && _div.Attributes["id"].Value == "video").FirstOrDefault();
                                    var iframe = div.Descendants("iframe").FirstOrDefault();
                                    link_video = iframe.Attributes["src"].Value;
                                }
                                catch (Exception ex)
                                {
                                    link_video = "";
                                }

                                string query = "INSERT INTO tblPhim18 VALUES (@name,@linkImage,@linkVideo)";
                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@name", title);
                                    cmd.Parameters.AddWithValue("@linkImage", linkImage);
                                    cmd.Parameters.AddWithValue("@linkVideo", link_video);

                                    int check = cmd.ExecuteNonQuery();
                                    if (check > 0)
                                    {
                                        Console.WriteLine("Add Done");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void GetDataPhimCR(string url, int soPage, string strConn)
        {
            using (SQLiteConnection conn = new SQLiteConnection(strConn))
            {
                conn.Open();
                string queryDelete = "DELETE FROM tblPhimChieuRap";
                using (SQLiteCommand cmdDelete = new SQLiteCommand(queryDelete, conn))
                {
                    int check = cmdDelete.ExecuteNonQuery();
                    if (check > 0)
                    {
                        Console.WriteLine("Xóa table Phim Chiếu Rạp");
                    }
                }
                using (WebClient webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    for (int i = 1; i <= soPage; i++)
                    {
                        string urlLast = url + i;
                        string html = webClient.DownloadString(urlLast);
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(html);

                        var article = htmlDocument.DocumentNode.Descendants("article").Where(_article => _article.Attributes.Contains("class") && _article.Attributes["class"].Value == "item movies");
                        foreach (var content in article)
                        {
                            string link_image = content.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");
                            string name = content.Descendants("img").FirstOrDefault().GetAttributeValue("alt", "");
                            string link_name = content.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                            chromeDriverService.HideCommandPromptWindow = true;

                            ChromeOptions chromeOptions = new ChromeOptions();
                            chromeOptions.AddArgument("--headless");

                            using (IWebDriver web = new ChromeDriver(chromeDriverService, chromeOptions))
                            {
                                web.Navigate().GoToUrl(link_name);

                                WebDriverWait wait = new WebDriverWait(web, TimeSpan.FromSeconds(10));
                                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                                string htmlContent = web.PageSource;
                                web.Quit();

                                HtmlDocument doc = new HtmlDocument();
                                doc.LoadHtml(htmlContent);

                                var ifarme = doc.DocumentNode.Descendants("div").Where(_ifarme => _ifarme.Attributes.Contains("class") && _ifarme.Attributes["class"].Value == "pframe").FirstOrDefault();
                                string link_video = "";
                                try
                                {
                                    link_video = ifarme.Descendants("iframe").FirstOrDefault().GetAttributeValue("src", "");
                                    link_video = link_video.Replace("/api/embed.html?link=", "");
                                }
                                catch (Exception ex)
                                {
                                    link_video = "";
                                }


                                string query = "INSERT INTO tblPhimChieuRap VALUES (@name,@linkImage,@linkVideo)";
                                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Parameters.AddWithValue("@linkImage", link_image);
                                    cmd.Parameters.AddWithValue("@linkVideo", link_video);

                                    int check = cmd.ExecuteNonQuery();
                                    if (check > 0)
                                    {
                                        Console.WriteLine("Add Done");
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
