using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using MarketCurrency.Classes;

namespace eTrade
{
    public partial class WatchList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
            }
        }

        protected void btnGetSymbol_Click(object sender, EventArgs e)
        {
            Quotes q;
            if (txtSymbol.Text != null && txtSymbol.Text.Trim().Length != 0)
            {
                q = getObject(txtSymbol.Text);
                List<Quotes> lstQuotes = new List<Quotes>();
                lstQuotes.Add(q);
                dvWatchList.DataSource = lstQuotes;
                dvWatchList.DataBind();
                //upWatchListouter.DataBind();
                //upWatchListouter.Update();
            }
        }

        public Quotes getObject(string symbol)
        {
            string csvData;
            Quotes q;
            using (WebClient web = new WebClient())
            {
                csvData = web.DownloadString("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22" + symbol + "%22)&env=store://datatables.org/alltableswithkeys");
                q = (YahooFinance.Parse(csvData, symbol));
            }
            return q;
        }
    }
}