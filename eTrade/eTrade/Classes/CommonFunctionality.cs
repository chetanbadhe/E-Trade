using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace eTrade.Classes
{
    public class CommonFunctionality
    {
        public string _username { get; set; }
        public int _userid { get; set; }
        public bool _isactive { get; set; }
        public string _emailid { get; set; }
        public int _profileid { get; set; }

        public CommonFunctionality()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    string data = authTicket.UserData;
                    List<String> userdata = data.Split('|').ToList<String>();
                    if (userdata.Count != 0)
                    {
                        _username = userdata[0];
                        _isactive = Convert.ToBoolean(userdata[1]);
                        _userid = Convert.ToInt32(userdata[2]);
                        _emailid = userdata[3];
                        _profileid = Convert.ToInt32(userdata[4]);
                    }
                }
            }
        }
 
            
    }
}