using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MarketCurrency.Classes;
using System.Net;
using eTrade.Classes;
using AjaxControlToolkit;
using eTrade.DataContext;
using System.Transactions;
using System.Data;


namespace eTrade
{
    public partial class UserPortfolio : System.Web.UI.Page
    {
        static CommonFunctionality _userinfo;
        static Quotes searchquote;
        static List<Quotes> lstWatchQuotes;
        static List<Quotes> lstQuotes = new List<Quotes>();
        static PortfolioManager pm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    ChartPanel.Visible = false;
                    DetailPanel.Visible = false;
                    BuyPanel.Visible = false;
                    SellPanel.Visible = false;
                    _userinfo = new CommonFunctionality();
                    pm = new PortfolioManager();
                    gvPortfolio.DataSource = pm.getPortfolioManager(_userinfo._profileid);
                    gvPortfolio.DataBind();
                    gvSearchSymbol.DataSource = lstQuotes;
                    gvSearchSymbol.DataBind();
                    upPortfolioouter.Update();
                }
            }
        }

        protected void btnGetSymbol_Click(object sender, EventArgs e)
        {
            if (txtSymbol.Text != null && txtSymbol.Text.Trim().Length != 0)
            {
                getgvSearchSymbol(txtSymbol.Text.Trim());
            }
        }

        public void getgvSearchSymbol(string symbol)
        {
            hdnFieldSymbol.Value = symbol;
            searchquote = getObject(symbol);
            lstQuotes = new List<Quotes>();
            lstQuotes.Add(searchquote);
            gvSearchSymbol.DataSource = lstQuotes;
            gvSearchSymbol.DataBind();
            divService.InnerHtml = getChart(txtSymbol.Text);
            dvStock.DataSource = lstQuotes;
            dvStock.DataBind();
            ChartPanel.Visible = true;
            DetailPanel.Visible = true;
            BuyPanel.Visible = true;
            txtSymbol.Text = "";
            upPortfolioouter.Update();
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

        protected void btnBuyStock_Click(object sender, EventArgs e)
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
             var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions()
                {IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted});

             try
             {
                 using (scope)
                 {
                     var userportfolio = (from p in dbcontext.Portfolios where p.Symbol == hdnFieldSymbol.Value && p.ProfileID == _userinfo._profileid && p.isActive == true select p).SingleOrDefault();
                     BuyOrder userbuyorder = new BuyOrder();
                     if (userportfolio == null)
                     {
                         userportfolio = new Portfolio();
                         userportfolio.Symbol = hdnFieldSymbol.Value.ToString();
                         userportfolio.ProfileID = _userinfo._profileid;
                         userportfolio.TotalVolumes = Convert.ToDecimal(txtBuyVolume.Text.ToString());
                         userportfolio.isActive = true;
                         dbcontext.AddToPortfolios(userportfolio);
                     }
                     else
                     {
                         userportfolio.TotalVolumes = userportfolio.TotalVolumes + Convert.ToDecimal(txtBuyVolume.Text.ToString());
                         dbcontext.SaveChanges();
                     }
                     userbuyorder.DateofPurchase = Convert.ToDateTime(txtBuyDateofPurchase.Text.ToString());
                     userbuyorder.UnitPrice = Convert.ToDecimal(txtBuyPrice.Text.ToString());
                     userbuyorder.Volume = Convert.ToDecimal(txtBuyVolume.Text.ToString());
                     userbuyorder.PortfolioID = userportfolio.PortfolioID;
                     dbcontext.AddToBuyOrders(userbuyorder);
                     dbcontext.SaveChanges();
                     scope.Complete();
                     setControlVisibility();
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

        protected void btnSellStock_Click(object sender, EventArgs e)
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
            var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted });

            try
            {
                using (scope)
                {
                    var userportfolio = (from p in dbcontext.Portfolios where p.Symbol == hdnFieldSymbol.Value && p.ProfileID == _userinfo._profileid && p.isActive == true select p).SingleOrDefault();
                    SellOrder sellorder = new SellOrder();
                    if (userportfolio == null)
                    {
                        InvalidExpressionException ex = new InvalidExpressionException();
                        throw(ex);
                    }
                    else
                    {

                        userportfolio.TotalVolumes = userportfolio.TotalVolumes - Convert.ToDecimal(txtSellVolume.Text.ToString());
                        if (userportfolio.TotalVolumes == 0)
                        {
                            userportfolio.isActive = false;
                            List<BuyOrder> buyorders = (from b in dbcontext.BuyOrders where b.PortfolioID == userportfolio.PortfolioID select b).ToList<BuyOrder>();
                            List<SellOrder> sellorders = (from b in dbcontext.SellOrders where b.PortfolioID == userportfolio.PortfolioID select b).ToList<SellOrder>();
                            userportfolio.Profit = (sellorders.Sum(i => i.UnitPrice * i.Volume)) + (Convert.ToDecimal(txtSellPrice.Text) * Convert.ToDecimal(txtSellVolume.Text)) - (buyorders.Sum(i => i.UnitPrice * i.Volume));
                        }
                        dbcontext.SaveChanges();
                    }
                    sellorder.DateofSell = Convert.ToDateTime(txtDateSell.Text);
                    sellorder.UnitPrice = Convert.ToDecimal(txtSellPrice.Text);
                    sellorder.Volume = Convert.ToDecimal(txtSellVolume.Text);
                    sellorder.PortfolioID = userportfolio.PortfolioID;
                    dbcontext.AddToSellOrders(sellorder);
                    dbcontext.SaveChanges();
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

        public void setControlVisibility()
        {
            hdnFieldSymbol.Value = "";
            txtBuyDateofPurchase.Text = "";
            txtBuyPrice.Text = "";
            txtBuyStock.Text = "";
            txtBuyVolume.Text = "";
            txtDateSell.Text = "";
            txtSellPrice.Text = "";
            txtSellStock.Text = "";
            txtSellVolume.Text = "";
            txtSymbol.Text = "";
            ChartPanel.Visible = false;
            DetailPanel.Visible = false;
            BuyPanel.Visible = false;
            SellPanel.Visible = false;
            upPortfolioouter.Update();
        }
    }
}