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
                gvWatchListSymbol.DataSource = lstQuotes;
                gvWatchListSymbol.DataBind();
                divService.InnerHtml = getChart(txtSymbol.Text);
                Panel1.Visible = true;
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

        protected void gvWatchListSymbol_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CheckChart")
            {
                
                mp1.Show();
               
                //int i = 0;
                //string symbols = Convert.ToString(e.CommandArgument.ToString());;
                //Random random = new Random();
                //_innerHtml += "<img id='imgChart_" +
                //    i.ToString() +
                //    "' src='http://ichart.finance.yahoo.com/b?s=" +
                //    symbols.Trim().ToUpper() + "& " +
                //    random.Next() + "' border=0><br />";
                //// 1 days
                //_innerHtml +=
                //  "<a style='font-family: Arial, Helvetica, sans-serif; " +
                //  "font-size: 14px; color: Blue;' " +
                //  "href='javascript:changeChart(0," +
                //  i.ToString() + ", \"" + symbols.ToLower() +
                //  "\");'><span id='div1d_" + i.ToString() +
                //  "'><b>1d</b></span></a>  ";
                //// 5 days
                //_innerHtml +=
                //  "<a style='font-family: Arial, Helvetica, sans-serif; " +
                //  "font-size: 14px; color: Blue;' " +
                //  "href='javascript:changeChart(1," +
                //  i.ToString() + ", \"" + symbols.ToLower() +
                //  "\");'><span id='div5d_" + i.ToString() +
                //  "'>5d</span></a>  ";
                //// 3 months
                //_innerHtml +=
                //  "<a style='font-family: Arial, Helvetica, sans-serif; " +
                //  "font-size: 14px; color: Blue;' " +
                //  "href='javascript:changeChart(2," +
                //  i.ToString() + ", \"" + symbols.ToLower() +
                //  "\");'><span id='div3m_" + i.ToString() +
                //  "'>3m</span></a>&  ";
                //// 6 months
                //_innerHtml +=
                //  "<a style='font-family: Arial, Helvetica, sans-serif; " +
                //  "font-size: 14px; color: Blue;' " +
                //  "href='javascript:changeChart(3," +
                //  i.ToString() + ", \"" + symbols.ToLower() +
                //  "\");'><span id='div6m_" + i.ToString() +
                //  "'>6m</span></a>  ";
                //// 1 yeas
                //_innerHtml +=
                //  "<a style='font-family: Arial, Helvetica, sans-serif; " +
                //  "font-size: 14px; color: Blue;' " +
                //  "href='javascript:changeChart(4," +
                //  i.ToString() + ", \"" + symbols.ToLower() +
                //  "\");'><span id='div1y_" + i.ToString() +
                //  "'>1y</span></a>  ";
                //// 2 years
                //_innerHtml +=
                //  "<a style='font-family: Arial, Helvetica, sans-serif; " +
                //  "font-size: 14px; color: Blue;' " +
                //  "href='javascript:changeChart(5," +
                //  i.ToString() + ", \"" + symbols.ToLower() +
                //  "\");'><span id='div2y_" + i.ToString() +
                //  "'>2y</span></a>  ";
                //// 5 years
                //_innerHtml +=
                //  "<a style='font-family: Arial, Helvetica, sans-serif; " +
                //  "font-size: 14px; color: Blue;' " +
                //  "href='javascript:changeChart(6," +
                //  i.ToString() + ", \"" + symbols.ToLower() +
                //  "\");'><span id='div5y_" + i.ToString() +
                //  "'>5y</span></a>  ";
                //// Max
                //_innerHtml +=
                //  "<a style='font-family: Arial, Helvetica, sans-serif; " +
                //  "font-size: 14px; color: Blue;' " +
                //  "href='javascript:changeChart(7," +
                //  i.ToString() + ", \"" + symbols.ToLower() +
                //  "\");'><span id='divMax_" + i.ToString() +
                //  "'>Max</span></a>" +
                //  "<br><br /><br />  ";
            }
        }

        public string getChart(string symbols)
        {
            int i = 0;
            
            Random random = new Random();
            string _innerHtml = "";
            _innerHtml += "<img id='imgChart_" +
                i.ToString() +
                "' src='http://ichart.finance.yahoo.com/b?s=" +
                symbols.Trim().ToUpper() + "& " +
                random.Next() + "' border=0><br />";
            // 1 days
            _innerHtml +=
              "<a style='font-family: Arial, Helvetica, sans-serif; " +
              "font-size: 14px; color: Blue;' " +
              "href='javascript:changeChart(0," +
              i.ToString() + ", \"" + symbols.ToLower() +
              "\");'><span id='div1d_" + i.ToString() +
              "'><b>1d</b></span></a>  ";
            // 5 days
            _innerHtml +=
              "<a style='font-family: Arial, Helvetica, sans-serif; " +
              "font-size: 14px; color: Blue;' " +
              "href='javascript:changeChart(1," +
              i.ToString() + ", \"" + symbols.ToLower() +
              "\");'><span id='div5d_" + i.ToString() +
              "'>5d</span></a>  ";
            // 3 months
            _innerHtml +=
              "<a style='font-family: Arial, Helvetica, sans-serif; " +
              "font-size: 14px; color: Blue;' " +
              "href='javascript:changeChart(2," +
              i.ToString() + ", \"" + symbols.ToLower() +
              "\");'><span id='div3m_" + i.ToString() +
              "'>3m</span></a>  ";
            // 6 months
            _innerHtml +=
              "<a style='font-family: Arial, Helvetica, sans-serif; " +
              "font-size: 14px; color: Blue;' " +
              "href='javascript:changeChart(3," +
              i.ToString() + ", \"" + symbols.ToLower() +
              "\");'><span id='div6m_" + i.ToString() +
              "'>6m</span></a>  ";
            // 1 yeas
            _innerHtml +=
              "<a style='font-family: Arial, Helvetica, sans-serif; " +
              "font-size: 14px; color: Blue;' " +
              "href='javascript:changeChart(4," +
              i.ToString() + ", \"" + symbols.ToLower() +
              "\");'><span id='div1y_" + i.ToString() +
              "'>1y</span></a>  ";
            // 2 years
            _innerHtml +=
              "<a style='font-family: Arial, Helvetica, sans-serif; " +
              "font-size: 14px; color: Blue;' " +
              "href='javascript:changeChart(5," +
              i.ToString() + ", \"" + symbols.ToLower() +
              "\");'><span id='div2y_" + i.ToString() +
              "'>2y</span></a>  ";
            // 5 years
            _innerHtml +=
              "<a style='font-family: Arial, Helvetica, sans-serif; " +
              "font-size: 14px; color: Blue;' " +
              "href='javascript:changeChart(6," +
              i.ToString() + ", \"" + symbols.ToLower() +
              "\");'><span id='div5y_" + i.ToString() +
              "'>5y</span></a>  ";
            // Max
            _innerHtml +=
              "<a style='font-family: Arial, Helvetica, sans-serif; " +
              "font-size: 14px; color: Blue;' " +
              "href='javascript:changeChart(7," +
              i.ToString() + ", \"" + symbols.ToLower() +
              "\");'><span id='divMax_" + i.ToString() +
              "'>Max</span></a>" +
              "<br><br /><br />  ";
            return _innerHtml;
        }
    }
}