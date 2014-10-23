using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace eTrade
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HeadLoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Session["user"]=null;
            Session["profileid"]=null;
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(Session["username"].ToString(), false);
            Session["username"]= null;
            authCookie.Expires = System.DateTime.Now;
        }
    }
}
