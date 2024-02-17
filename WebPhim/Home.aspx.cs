using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPhim
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            radPhimChieuRap.Checked = true;
            LoadPhimChieuRap();
        }

        protected void radPhim18_Click(object sender, EventArgs e)
        {
            radPhimChieuRap.Checked = false;

            string html_Check = "<script>\r\n    var start = false;\r\n    do {\r\n        var pass = \"kienDev2003.,.@\";\r\n        var passInput = prompt(\"Nhập pass rồi vào nhé !\");\r\n        if(pass == passInput){\r\n            start = true;\r\n        }\r\n    } while (start == false);\r\n</script>";
            Response.Write(html_Check);

            LoadPhim18();
        }

        protected void radPhimChieuRap_Click(object sender, EventArgs e)
        {
            radPhim18.Checked = false;
            LoadPhimChieuRap();
        }

        private void LoadPhim18()
        {
            LoadPhim _loadPhim = new LoadPhim();
            string query = "SELECT * FROM tblPhim18";
            List<ListVideo> listVideos = _loadPhim.GetlistVideo(query);
            ul_list_phim.Controls.Clear();
            foreach (var content in listVideos)
            {
                string htmlContent = $"<li class=\"item-movie\"><a title=\"{content.name}\" href=\"{content.link_video}\"><div class=\"image\"><div class=\"movie-thumbnail\" style=\"background-image:url('{content.link_image}')\"></div></div><div class=\"title-movie\">{content.name}</div><div></div></a></li>";

                LiteralControl literalControl = new LiteralControl(htmlContent);
                ul_list_phim.Controls.Add(literalControl);
            }
        }

        private void LoadPhimChieuRap()
        {
            LoadPhim _loadPhim = new LoadPhim();
            string query = "SELECT * FROM tblPhimChieuRap";
            List<ListVideo> listVideos = _loadPhim.GetlistVideo(query);
            ul_list_phim.Controls.Clear();
            foreach (var content in listVideos)
            {

                string htmlContent = $"<li class=\"item-movie\"><a title=\"{content.name}\" href=\"{content.link_video}\"><div class=\"image\"><div class=\"movie-thumbnail\" style=\"background-image:url('{content.link_image}')\"></div></div><div class=\"title-movie\">{content.name}</div><div></div></a></li>";

                LiteralControl literalControl = new LiteralControl(htmlContent);
                ul_list_phim.Controls.Add(literalControl);
            }
        }
    }
}