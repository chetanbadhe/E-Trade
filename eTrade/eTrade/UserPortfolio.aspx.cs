using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTrade.DataContext;
using MarketCurrency.Classes;
using System.Net;
using System.Transactions;
using System.Data;
using eTrade.Classes;

namespace eTrade
{
    public partial class UserPortfolio : System.Web.UI.Page
    {
        Quotes searchquote;
        List<Quotes> lstQuotes;
        PortfolioManager pm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    BuyPanel.Visible = false;
                    //SellPanel.Visible = false;
                    eUser user = (eUser)Session["user"];
                    gvGetSymbol.DataSource = null;
                    gvGetSymbol.DataBind();
                    pm = new PortfolioManager();
                    List<PortfolioManager> upm = new List<PortfolioManager>();
                    upm = pm.getPortfolioManager(Convert.ToInt32(Session["profileid"].ToString()));
                    ViewState["upm"] = upm;
                    gvPortfolio.DataSource = upm;
                    gvPortfolio.DataBind();
                    upWatchListouter.Update();
                }
            }
        }

        protected void btnGetSymbol_Click(object sender, EventArgs e)
        {
            if (txtSymbol.Text != null && txtSymbol.Text.Trim().Length != 0)
            {
                getgvGetSymbolData(txtSymbol.Text.Trim());
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
            BuyPanel.Visible = true;
            txtSymbol.Text = "";
            txtBuyStock.Text = hdnFieldSymbol.Value.ToString();
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

        protected void btnBuyStock_Click(object sender, EventArgs e)
        {
            putBuyOrder();
        }

        public void putBuyOrder()
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
            var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted });
            int profileid = Convert.ToInt32(Session["profileid"].ToString());
            try
            {
                using (scope)
                {
                    var userportfolio = (from p in dbcontext.Portfolios where p.Symbol == hdnFieldSymbol.Value && p.ProfileID == profileid && p.isActive == true select p).SingleOrDefault();
                    BuyOrder userbuyorder = new BuyOrder();
                    if (userportfolio == null)
                    {
                        userportfolio = new Portfolio();
                        userportfolio.Symbol = hdnFieldSymbol.Value.ToString();
                        userportfolio.ProfileID = profileid;
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
            //putSellOrder();
        }

        //public void putSellOrder()
        //{
        //    eTradeDbEntities dbcontext = new eTradeDbEntities();
        //    var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted });
        //    int profileid = Convert.ToInt32(Session["profileid"].ToString());
        //    try
        //    {
        //        using (scope)
        //        {
        //            var userportfolio = (from p in dbcontext.Portfolios where p.Symbol == hdnFieldSymbol.Value && p.ProfileID == profileid && p.isActive == true select p).SingleOrDefault();
        //            SellOrder sellorder = new SellOrder();
        //            if (userportfolio == null)
        //            {
        //                InvalidExpressionException ex = new InvalidExpressionException();
        //                throw (ex);
        //            }
        //            else
        //            {

        //                userportfolio.TotalVolumes = userportfolio.TotalVolumes - Convert.ToDecimal(txtSellVolume.Text.ToString());
        //                if (userportfolio.TotalVolumes == 0)
        //                {
        //                    userportfolio.isActive = false;
        //                    List<BuyOrder> buyorders = (from b in dbcontext.BuyOrders where b.PortfolioID == userportfolio.PortfolioID select b).ToList<BuyOrder>();
        //                    List<SellOrder> sellorders = (from b in dbcontext.SellOrders where b.PortfolioID == userportfolio.PortfolioID select b).ToList<SellOrder>();
        //                    userportfolio.Profit = (sellorders.Sum(i => i.UnitPrice * i.Volume)) + (Convert.ToDecimal(txtSellPrice.Text) * Convert.ToDecimal(txtSellVolume.Text)) - (buyorders.Sum(i => i.UnitPrice * i.Volume));
        //                }
        //                dbcontext.SaveChanges();
        //            }
        //            sellorder.DateofSell = Convert.ToDateTime(txtDateSell.Text);
        //            sellorder.UnitPrice = Convert.ToDecimal(txtSellPrice.Text);
        //            sellorder.Volume = Convert.ToDecimal(txtSellVolume.Text);
        //            sellorder.PortfolioID = userportfolio.PortfolioID;
        //            dbcontext.AddToSellOrders(sellorder);
        //            dbcontext.SaveChanges();
        //            scope.Complete();
        //            setControlVisibility();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        scope.Dispose();
        //    }
        //    finally
        //    {
        //        if (dbcontext.Connection.State == ConnectionState.Open)
        //        {
        //            dbcontext.Connection.Close();
        //        }
        //    }
        //}

        public void setControlVisibility()
        {
            hdnFieldSymbol.Value = "";
            txtBuyDateofPurchase.Text = "";
            txtBuyPrice.Text = "";
            txtBuyStock.Text = "";
            txtBuyVolume.Text = "";
            ddlAction.SelectedValue = "No Selection";
            txtSymbol.Text = "";
            Panel1.Visible = false;
            Panel2.Visible = false;
            BuyPanel.Visible = false;
            //SellPanel.Visible = false;
            gvGetSymbol.DataSource = null;
            gvGetSymbol.DataBind();
            pm = new PortfolioManager();
            gvPortfolio.DataSource = pm.getPortfolioManager(Convert.ToInt32(Session["profileid"].ToString()));
            gvPortfolio.DataBind();
            upWatchListouter.Update();
        }

        protected void gvPortfolio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                BuyPanel.Visible = true;
                List<PortfolioManager> upm = ViewState["upm"] as List<PortfolioManager>;
                string arg = e.CommandArgument.ToString();
                string [] slist = arg.Split(',');
                MyAccordion.DataSource = upm.Where(i => i.portfolioID == Convert.ToInt64(slist[2]) && i.symbol == slist[0]);
                MyAccordion.DataBind();
                upWatchListouter.Update();
            }
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAction.SelectedValue == "No Selection")
            {
                btnBuyStock.Text = "Action";
                txtBuyVolume.Enabled = false;
                txtBuyPrice.Enabled = false;
                txtBuyDateofPurchase.Enabled = false;
                btnBuyStock.Enabled = false;
            }
            else if (ddlAction.SelectedValue == "Buy")
            {
                btnBuyStock.Text = "Buy";
                txtBuyVolume.Enabled = true;
                txtBuyPrice.Enabled = true;
                txtBuyDateofPurchase.Enabled = true;
                btnBuyStock.Enabled = true;
            }
            else if (ddlAction.SelectedValue == "Sell")
            {
                btnBuyStock.Text = "Sell";
                txtBuyVolume.Enabled = true;
                txtBuyPrice.Enabled = true;
                txtBuyDateofPurchase.Enabled = true;
                btnBuyStock.Enabled = true;
            }
        }

    }
}