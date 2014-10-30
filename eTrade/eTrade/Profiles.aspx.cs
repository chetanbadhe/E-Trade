using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTrade.DataContext;
using System.Web.Security;
using System.Transactions;
using System.Data;

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
                        eTradeDbEntities dbcontext = new eTradeDbEntities();
                        eUser user = (eUser)Session["user"];
                        var defaultprofile = (from p in dbcontext.Profiles where p.isDefault == true && p.UserID == user.UserID select p).SingleOrDefault();
                        EDSProfiles.WhereParameters.Clear();
                        EDSProfiles.AutoGenerateWhereClause = true;
                        EDSProfiles.WhereParameters.Add("UserID", TypeCode.Int64, user.UserID.ToString());
                        ddlprofiles.DataSource = EDSProfiles;
                        ddlprofiles.DataBind();
                        ddlprofiles.Items.FindByValue(defaultprofile.ProfileID.ToString()).Selected = true;
                        gvGetProfiles.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void btnSetDefault_Click(object sender, EventArgs e)
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
            eUser user = (eUser) Session["user"];
            var scope = new System.Transactions.TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted });
            try
            {
                using (scope)
                {
                    long selectval = Convert.ToInt64(ddlprofiles.SelectedValue.ToString());
                    var prevprofile = (from p in dbcontext.Profiles where p.isDefault == true && p.UserID == user.UserID select p).SingleOrDefault();
                    var defaultprofile = (from p in dbcontext.Profiles where p.ProfileID == selectval && p.UserID == user.UserID select p).SingleOrDefault();
                    defaultprofile.isDefault = true;
                    prevprofile.isDefault = false;
                    dbcontext.SaveChanges();
                    Session["profileid"] = defaultprofile.ProfileID.ToString();
                    EDSProfiles.WhereParameters.Clear();
                    EDSProfiles.AutoGenerateWhereClause = true;
                    EDSProfiles.WhereParameters.Add("UserID", TypeCode.Int64, user.UserID.ToString());                   
                    gvGetProfiles.DataBind();
                    scope.Complete();
                 }

            }
            catch (Exception ex)
            {
                scope.Dispose();
            }
            finally
            {
                if (dbcontext.Connection.State == ConnectionState.Open)
                {
                    dbcontext.Connection.Close();
                }
            }
        }
    }
}