using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using MarketCurrency.Classes;
using System.Web.Security;
using eTrade.DataContext;
using eTrade.Classes;

namespace eTrade
{
    public partial class UsersWatchList : System.Web.UI.Page
    {
        static CommonFunctionality _userinfo;
        static Quotes searchquote;
        static List<Quotes> lstWatchQuotes;
        static List<Quotes> lstQuotes = new List<Quotes>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    _userinfo = new CommonFunctionality();
                    gvGetSymbol.DataSource = lstQuotes;
                    gvGetSymbol.DataBind();
                    BindWatchList();
                    upWatchListouter.Update();
                }
            }
        }

        protected void btnGetSymbol_Click(object sender, EventArgs e)
        {
            if (txtSymbol.Text != null && txtSymbol.Text.Trim().Length != 0)
            {
                getgvGetSymbolData(txtSymbol.Text.Trim());
                btnAdd.Visible = true;
                btnDelete.Visible = false;
            }
        }

        public void getgvGetSymbolData(string symbol)
        {
            hdnFieldSymbol.Value = symbol;
            searchquote = getObject(symbol);
            lstQuotes = new List<Quotes>();
            lstQuotes.Add(searchquote);
            gvGetSymbol.DataSource = lstQuotes;
            gvGetSymbol.DataBind();
            divService.InnerHtml = getChart(txtSymbol.Text);
            dvStock.DataSource = lstQuotes;
            dvStock.DataBind();
            Panel1.Visible = true;
            Panel2.Visible = true;
            txtSymbol.Text = "";
            upWatchListouter.Update();
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

        public void BindWatchList()
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
            var lstWatchList = (from s in dbcontext.WatchLists where s.ProfileID == _userinfo._profileid && s.isActive == true select s).ToList<WatchList>();
            lstWatchQuotes = new List<Quotes>();
            Quotes q;
            foreach (WatchList _w in lstWatchList)
            {
                q = getObject(_w.Symbol.Trim());
                lstWatchQuotes.Add(q);
            }
            gvWatchListSymbol.DataSource = lstWatchQuotes;
            gvWatchListSymbol.DataBind();
            upWatchListouter.Update();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
            var isexist = (from s in dbcontext.WatchLists where s.ProfileID == _userinfo._profileid && s.Symbol == searchquote.Symbol  select s).SingleOrDefault();
            if (isexist == null)
            {
                WatchList watchlist = new WatchList();
                watchlist.Symbol = searchquote.Symbol;
                watchlist.WatchDate = System.DateTime.Now;
                watchlist.isActive = true;
                watchlist.ProfileID = _userinfo._profileid;
                dbcontext.AddToWatchLists(watchlist);
                dbcontext.SaveChanges();
                BindWatchList();
                gvGetSymbol.DataSource = null;
                gvGetSymbol.DataBind();
                Panel1.Visible = false;
                Panel2.Visible = false;
                btnAdd.Visible = false;
                upWatchListouter.Update();

            }
            else
            {
                if (isexist.isActive == false)
                {
                    isexist.isActive = true;
                    isexist.WatchDate = System.DateTime.Now;
                    dbcontext.SaveChanges();
                    BindWatchList();
                }
                gvGetSymbol.DataSource = null;
                gvGetSymbol.DataBind();
                Panel1.Visible = false;
                Panel2.Visible = false;
                btnAdd.Visible = false;
                upWatchListouter.Update();

            }
        }

        protected void gvWatchListSymbol_RowCommand(object sender, GridViewCommandEventArgs e)
        {  
            if (e.CommandName == "Select")
            {
                getgvGetSymbolData(e.CommandArgument.ToString());
                btnDelete.Visible = true;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
            WatchList wsymbol = dbcontext.WatchLists.First(w => w.Symbol == hdnFieldSymbol.Value && w.ProfileID == _userinfo._profileid);
            wsymbol.isActive = false;
            dbcontext.SaveChanges();
            Panel1.Visible = false;
            Panel2.Visible = false;
            btnDelete.Visible = false;
            gvGetSymbol.DataSource = null;
            gvGetSymbol.DataBind();
            BindWatchList();
            hdnFieldSymbol.Value = "";
            upWatchListouter.Update();
        }
    }
}