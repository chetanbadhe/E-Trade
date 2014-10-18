using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTrade.DataContext;
using System.Web.Security;

namespace eTrade.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!IsPostBack)
                ViewState["ContinueDestinationPageUrl"] = Request.QueryString["ReturnUrl"];
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
            var user = (from p in dbcontext.eUsers where p.UserName == LoginUser.UserName.Trim() && p.Password == LoginUser.Password.Trim() select p).SingleOrDefault();
            if (user != null)
            {

                var pdefault = (from p in dbcontext.Profiles where p.UserID == user.UserID && p.isDefault == true select p).Single();
                //FormsAuthentication.SetAuthCookie(euser.UserName.Trim(), false /* createPersistentCookie */);
                string userData = user.UserName + "|" + user.IsActive + "|" + user.UserID + "|" + user.EmailID + "|" + pdefault.ProfileID;
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(user.UserName, false);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userData);
                authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                Response.Cookies.Add(authCookie);

                string continueUrl = ViewState["ContinueDestinationPageUrl"].ToString();
                if (String.IsNullOrEmpty(continueUrl))
                {
                    continueUrl = "~/";
                }
                Response.Redirect(continueUrl);
            }
            else
            {
                LoginUser.FailureText = "User not Found!!!";
            }
        }
    }
}
