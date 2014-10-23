using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using eTrade.DataContext;

namespace eTrade.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                ViewState["ContinueDestinationPageUrl"] = Request.QueryString["ReturnUrl"];
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();

            var euser = (from p in dbcontext.eUsers
                        where p.EmailID == Email.Text.Trim() || p.UserName == UserName.Text.Trim()
                        select p).SingleOrDefault();
            if (euser == null)
            {
                eUser user = new eUser();
                user.UserName = UserName.Text.Trim();
                user.Password = Password.Text.Trim();
                user.EmailID = Email.Text.Trim();
                user.IsActive = true;
                user.RegisterDate = DateTime.Now;
                dbcontext.AddToeUsers(user);
                dbcontext.SaveChanges();

                Profile profile1 = new Profile();
                Profile profile2 = new Profile();
                Profile profile3 = new Profile();
                profile1.ProfileName = "Profile1";
                profile1.UserID = user.UserID;
                profile1.isDefault = true;
                profile2.ProfileName = "Profile2";
                profile2.UserID = user.UserID;
                profile2.isDefault = false;
                profile3.ProfileName = "Profile3";
                profile3.UserID = user.UserID;
                profile3.isDefault = false;
                dbcontext.AddToProfiles(profile1);
                dbcontext.AddToProfiles(profile2);
                dbcontext.AddToProfiles(profile3);
                dbcontext.SaveChanges();

                string userData = user.UserName + "|" + user.IsActive + "|" + user.UserID + "|" + user.EmailID + "|" + profile1.ProfileID;
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(user.UserName, false);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userData);
                authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                Response.Cookies.Add(authCookie);

                Session["user"] = user;
                Session["profileid"] = profile1.ProfileID;
                Session["username"] = user.UserName;

                string continueUrl = ViewState["ContinueDestinationPageUrl"].ToString();
                if (String.IsNullOrEmpty(continueUrl))
                {
                    continueUrl = "~/";
                }
                Response.Redirect(continueUrl);
            }
            else if(euser!=null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if(euser.UserName == UserName.Text.Trim())
                    ErrorMessage.Text = "User Name already exists!!!";
                else
                    ErrorMessage.Text = "EmailID already exists!!!";
            }
        }

    }
}
