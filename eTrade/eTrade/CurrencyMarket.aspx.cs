using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Collections;

namespace eTrade
{
    public partial class CurrencyMarket : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                CurrencyConvertor.Currency cs = new CurrencyConvertor.Currency();
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
            CurrencyConvertor.CurrencyConvertorSoapClient objCurCon = new CurrencyConvertor.CurrencyConvertorSoapClient();
            CurrencyConvertor.CurrencyConvertorSoapClient objCurrency = new CurrencyConvertor.CurrencyConvertorSoapClient();
            double dt = new double();
            dt = objCurCon.ConversionRate((CurrencyConvertor.Currency)Enum.Parse(objCurrency.GetType(), ddlcurrencyfrom.Text),
                (CurrencyConvertor.Currency)Enum.Parse(objCurrency.GetType(), ddlcurrencyto.Text));
            txtcurrencyto.Text = Convert.ToString(dt * Convert.ToDouble(txtcurrencyfrom.Text));
        }    
    }
}
