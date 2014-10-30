using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eTrade.DataContext;
using MarketCurrency.Classes;
using System.Net;
using System.Transactions;
using System.Data;

namespace eTrade.Classes
{
    [Serializable]
    public class PortfolioManager
    {
        public Int64 portfolioID {get;set;}
        public Int64 profileID { get; set; }
        public string symbol { get; set; }
        public decimal avgprice {get;set;}
        public decimal remainingvolume {get;set;}
        public decimal livePrice { get; set; }
        public decimal change { get; set; }
        public decimal profit {get;set;}
        public List<BuyOrder> buyorder {get;set;}
        public List<SellOrder> sellorder {get;set;}
        public List<Portfolio> portfoliolist;

        public PortfolioManager()
        {
        }

        public List<PortfolioManager> getPortfolioManager(int profile_id)
        {
            eTradeDbEntities dbcontext = new eTradeDbEntities();
            var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted });
            List<PortfolioManager> pmlist = new List<PortfolioManager>();
            try
            {
                using (scope)
                {
                    long pid = Convert.ToInt64(profile_id);
                    List<Portfolio> plist = (from p in dbcontext.Portfolios where p.ProfileID == pid && p.isActive == true select p).ToList<Portfolio>();
                   
                    decimal buyVolumetotal;
                    decimal sellVolumetotal;
                    decimal buyTotalPrice;
                    decimal sellTotalPrice;
                    foreach (var p in plist)
                    {
                        PortfolioManager pmgr = new PortfolioManager();
                        pmgr.profileID = profile_id;
                        pmgr.symbol = p.Symbol;
                        pmgr.portfolioID = p.PortfolioID;
                        pmgr.buyorder = (from b in dbcontext.BuyOrders where b.PortfolioID == p.PortfolioID select b).ToList<BuyOrder>();
                        pmgr.sellorder = (from b in dbcontext.SellOrders where b.PortfolioID == p.PortfolioID select b).ToList<SellOrder>();
                        pmgr.livePrice = getObject(p.Symbol);
                        buyVolumetotal = pmgr.buyorder.Sum(i => i.Volume);
                        sellVolumetotal = pmgr.sellorder.Sum(j => j.Volume);
                        buyTotalPrice = pmgr.buyorder.Sum(i => i.Volume * i.UnitPrice);
                        sellTotalPrice = pmgr.sellorder.Sum(j => j.Volume * j.UnitPrice);
                        pmgr.avgprice = buyTotalPrice / buyVolumetotal;
                        pmgr.change = pmgr.livePrice - pmgr.avgprice;
                        pmgr.remainingvolume = buyVolumetotal - sellVolumetotal;
                        pmgr.profit = (pmgr.remainingvolume * pmgr.livePrice) - buyTotalPrice + (sellTotalPrice);
                        pmlist.Add(pmgr);
                    }
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
                    //dbcontext.Connection.Close();
                }
            }
            return pmlist;
        }

        public decimal getObject(string symbol)
        {
            string csvData;
            Quotes q;
            using (WebClient web = new WebClient())
            {
                csvData = web.DownloadString("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22" + symbol + "%22)&env=store://datatables.org/alltableswithkeys");
                q = (YahooFinance.Parse(csvData, symbol));
            }
            return q.LastTradePriceOnly;
        }
    }
}