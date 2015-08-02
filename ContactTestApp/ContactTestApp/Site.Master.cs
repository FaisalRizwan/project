using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactTestApp
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLoggedIn = HttpContext.Current.User.Identity.IsAuthenticated;
            if (isLoggedIn)
            {

                NavigationMenu.Visible = true;

                maindiv.Visible = true;
            }
            else
            {
                string url = Request.Url.ToString();
                if (url.EndsWith("Login.aspx"))
                {
                    NavigationMenu.Visible = true;

                    maindiv.Visible = true;
                }
                else
                {
                    NavigationMenu.Visible = false;
                    maindiv.Visible = false;
                }

            }
        }
    }
}
