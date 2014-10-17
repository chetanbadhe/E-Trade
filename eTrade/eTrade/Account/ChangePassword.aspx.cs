using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTrade.DataContext;

namespace eTrade.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CancelPushButton_Click(object sender, EventArgs e)
        {

        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            //eTradeDbEntities dbcontext = new eTradeDbEntities();
            //var euser = (from p in dbcontext.eUsers where p.Password == ChangeUserPassword.CurrentPassword.Trim() select p).SingleOrDefault();
            //if (euser != null)
            //{
            //    //FormsAuthentication.SetAuthCookie(euser.UserName.Trim(), false /* createPersistentCookie */);

            //    string continueUrl = ViewState["ContinueDestinationPageUrl"].ToString();
            //    if (String.IsNullOrEmpty(continueUrl))
            //    {
            //        continueUrl = "~/";
            //    }
            //    Response.Redirect(continueUrl);
            //}
            //else
            //{
            //    ChangeUserPassword.FailureText = "User not Found!!!";
            //}
        }
    }
}
