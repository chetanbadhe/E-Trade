using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eTrade
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            mp1.Show();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Do work

            mp1.Show();
        }
    }
}
