using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPhim
{
    public partial class Pass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPass_Click(object sender, EventArgs e)
        {
            Session["pass"] = txtPass.Value.Trim();
            Response.Redirect("./Home.aspx");
        }
    }
}