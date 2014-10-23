using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eTrade.DataContext;
using MarketCurrency.Classes;
using System.Net;

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
            try
            {
                eTradeDbEntities dbcontext = new eTradeDbEntities();
                List<Portfolio> plist = (from p in dbcontext.Portfolios where p.ProfileID == profile_id && p.isActive == true select p).ToList<Portfolio>();
                List<PortfolioManager> pmlist = new List<PortfolioManager>();
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
                    pmgr.profit =  (pmgr.remainingvolume * pmgr.livePrice) - buyTotalPrice + (sellTotalPrice); 
                    pmlist.Add(pmgr);
                }
                return pmlist;
            }
            catch (Exception ex)
            {
                return null;
            }
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