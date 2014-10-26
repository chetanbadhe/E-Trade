using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Collections;
using eTrade.CurrencyConvertorWS;


namespace eTrade
{
    public partial class CurrencyMarket : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                CurrencyConvertorWS.Currency cs = new CurrencyConvertorWS.Currency();
                Array arr;
                arr = Enum.GetValues(cs.GetType());
                int n = arr.Length;
                ddlcurrencyfrom.DataSource = arr;
                ddlcurrencyfrom.DataBind();
                ddlcurrencyto.DataSource = arr;
                ddlcurrencyto.DataBind();
            }
        }

        protected void btnGetRate_Click(object sender, EventArgs e)
        {
            double rate = 0, result = 0;
            Currency curr1, curr2;
            try
            {
                CurrencyConvertor ws = new CurrencyConvertor();
                curr1 = (Currency)Enum.Parse(typeof(Currency), ddlcurrencyfrom.SelectedItem.Value);
                curr2 = (Currency)Enum.Parse(typeof(Currency), ddlcurrencyto.SelectedItem.Value);
                rate = ws.ConversionRate(curr1, curr2);
                result = System.Convert.ToDouble(txtcurrencyfrom.Text) * rate;
                txtcurrencyto.Text = result.ToString();
            }
            catch
            {
               
            }
        }    
    }
}
