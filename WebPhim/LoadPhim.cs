using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace WebPhim
{
    public class LoadPhim
    {
        string stcConn = ConfigurationManager.AppSettings["strConn"].ToString();
        public List<ListVideo> GetlistVideo(string query)
        {

            List<ListVideo> list = new List<ListVideo>();
            using (SQLiteConnection conn = new SQLiteConnection(stcConn))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            string link_image = reader.GetString(1);
                            string link_video = reader.GetString(2);

                            list.Add(new ListVideo { name = name, link_image = link_image, link_video = link_video });
                        }
                    }
                }
            }
            return list;
        }
    }
}