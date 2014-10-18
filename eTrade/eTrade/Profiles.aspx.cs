using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTrade.DataContext;
using System.Web.Security;

namespace eTrade
{
    public partial class Profiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (authCookie != null)
                    {
                        var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                        string data = authTicket.UserData;
                        List<String> userdata = data.Split('|').ToList<String>();

                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void btnCreateProfile_Click(object sender, EventArgs e)
        {
                        
        }
    }
}