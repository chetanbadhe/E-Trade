using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace MarketCurrency.Classes
{
    public class YahooFinance
    {
        public static Quotes Parse(string csvData, string symbol)
        {
            Quotes price = new Quotes();
            XDocument doc = XDocument.Parse(csvData);
            XElement xmlelement = doc.Root.Element("results");
            XElement q = xmlelement.Elements("quote").First(w => w.Attribute("symbol").Value == symbol.ToUpper());
            if (q.Element("Ask").Value != "")
                price.Ask = (decimal)GetDecimal(q.Element("Ask").Value);
            if(q.Element("Bid").Value!="")
                price.Bid = (decimal)GetDecimal(q.Element("Bid").Value);
            if (q.Element("Symbol").Value != "")
                price.Symbol = (string)(q.Element("Symbol").Value);
            if (q.Element("AskRealtime").Value != "")
                price.AskRealtime = (decimal)GetDecimal(q.Element("AskRealtime").Value);
            if (q.Element("BidRealtime").Value != "")
                price.BidRealtime = (decimal)GetDecimal(q.Element("BidRealtime").Value);
            if (q.Element("BookValue").Value != "")
                price.BookValue = (decimal)GetDecimal(q.Element("BookValue").Value);
            if (q.Element("Change_PercentChange").Value != "")
                price.Change_PercentChange = (string)(q.Element("Change_PercentChange").Value);
            if (q.Element("Change").Value != "")
                price.Change = (decimal)GetDecimal(q.Element("Change").Value);
            if (q.Element("ChangeRealtime").Value != "")
                price.ChangeRealtime = (decimal)GetDecimal(q.Element("ChangeRealtime").Value);
            if (q.Element("AfterHoursChangeRealtime").Value != "")
                price.AfterHoursChangeRealtime = (string)(q.Element("AfterHoursChangeRealtime").Value);
            if (q.Element("DividendShare").Value != "")
                price.DividendShare = (decimal)GetDecimal(q.Element("DividendShare").Value);
            if (q.Element("LastTradeDate").Value != "")
                price.LastTradeDate = (q.Element("LastTradeDate").Value);
            if (q.Element("EarningsShare").Value != "")
                price.EarningsShare = (decimal)GetDecimal(q.Element("EarningsShare").Value);

            if (q.Element("EPSEstimateCurrentYear").Value != "")
                price.EPSEstimateCurrentYear = (decimal)GetDecimal(q.Element("EPSEstimateCurrentYear").Value);
            if (q.Element("EPSEstimateNextYear").Value != "")
                price.EPSEstimateNextYear = (decimal)GetDecimal(q.Element("EPSEstimateNextYear").Value);
            if (q.Element("EPSEstimateNextQuarter").Value != "")
                price.EPSEstimateNextQuarter = (decimal)GetDecimal(q.Element("EPSEstimateNextQuarter").Value);
            if (q.Element("DaysLow").Value != "")
                price.DaysLow = (decimal)GetDecimal(q.Element("DaysLow").Value);
            if (q.Element("DaysHigh").Value != "")
                price.DaysHigh = (decimal)GetDecimal(q.Element("DaysHigh").Value);
            if (q.Element("YearLow").Value != "")
                price.YearLow = (decimal)GetDecimal(q.Element("YearLow").Value);
            if (q.Element("YearHigh").Value != "")
                price.YearHigh = (decimal)GetDecimal(q.Element("YearHigh").Value);
            if (q.Element("HoldingsGainPercent").Value != "")
                price.HoldingsGainPercent = (q.Element("HoldingsGainPercent").Value);
            if (q.Element("HoldingsGainPercentRealtime").Value != "")
                price.HoldingsGainPercentRealtime = (q.Element("HoldingsGainPercentRealtime").Value);
            if (q.Element("MarketCapitalization").Value != "")
                price.MarketCapitalization = (q.Element("MarketCapitalization").Value);
            if (q.Element("EBITDA").Value != "")
                price.EBITDA = (q.Element("EBITDA").Value);
            if (q.Element("ChangeFromYearLow").Value != "")
                price.ChangeFromYearLow = (decimal)GetDecimal(q.Element("ChangeFromYearLow").Value);
            if (q.Element("PercentChangeFromYearLow").Value != "")
                price.PercentChangeFromYearLow = (decimal)GetDecimal(q.Element("PercentChangeFromYearLow").Value);

            if (q.Element("LastTradeRealtimeWithTime").Value != "")
                price.LastTradeRealtimeWithTime = (q.Element("LastTradeRealtimeWithTime").Value);
            if (q.Element("ChangePercentRealtime").Value != "")
                price.ChangePercentRealtime = (q.Element("ChangePercentRealtime").Value);
            if (q.Element("ChangeFromYearHigh").Value != "")
                price.ChangeFromYearHigh = (decimal)GetDecimal(q.Element("ChangeFromYearHigh").Value);
            if (q.Element("PercebtChangeFromYearHigh").Value != "")
                price.PercebtChangeFromYearHigh = (decimal)GetDecimal(q.Element("PercebtChangeFromYearHigh").Value);
            if (q.Element("LastTradeWithTime").Value != "")
                price.LastTradeWithTime = (q.Element("LastTradeWithTime").Value);
            if (q.Element("LastTradePriceOnly").Value != "")
                price.LastTradePriceOnly = (decimal)GetDecimal(q.Element("LastTradePriceOnly").Value);
            if (q.Element("DaysRange").Value != "")
                price.DaysRange = (q.Element("DaysRange").Value);
            if (q.Element("DaysRangeRealtime").Value != "")
                price.DaysRangeRealtime = (q.Element("DaysRangeRealtime").Value);
            if (q.Element("FiftydayMovingAverage").Value != "")
                price.FiftydayMovingAverage = (decimal)GetDecimal(q.Element("FiftydayMovingAverage").Value);
            if (q.Element("TwoHundreddayMovingAverage").Value != "")
                price.TwoHundreddayMovingAverage = (decimal)GetDecimal(q.Element("TwoHundreddayMovingAverage").Value);
            if (q.Element("ChangeFromTwoHundreddayMovingAverage").Value != "")
                price.ChangeFromTwoHundreddayMovingAverage = (decimal)GetDecimal(q.Element("ChangeFromTwoHundreddayMovingAverage").Value);
            if (q.Element("PercentChangeFromTwoHundreddayMovingAverage").Value != "")
                price.PercentChangeFromTwoHundreddayMovingAverage = (decimal)GetDecimal(q.Element("PercentChangeFromTwoHundreddayMovingAverage").Value);
            if (q.Element("ChangeFromFiftydayMovingAverage").Value != "")
                price.ChangeFromFiftydayMovingAverage = (decimal)GetDecimal(q.Element("ChangeFromFiftydayMovingAverage").Value);

            if (q.Element("PercentChangeFromFiftydayMovingAverage").Value != "")
                price.PercentChangeFromFiftydayMovingAverage = (decimal)GetDecimal(q.Element("PercentChangeFromFiftydayMovingAverage").Value);
            if (q.Element("Name").Value != "")
                price.Name = (q.Element("Name").Value);
            if (q.Element("Open").Value != "")
                price.Open = (q.Element("Open").Value);
            if (q.Element("PreviousClose").Value != "")
                price.PreviousClose = (decimal)GetDecimal(q.Element("PreviousClose").Value);
            if (q.Element("ChangeinPercent").Value != "")
                price.ChangeinPercent = (decimal)GetDecimal(q.Element("ChangeinPercent").Value);
            if (q.Element("PriceSales").Value != "")
                price.PriceSales = (decimal)GetDecimal(q.Element("PriceSales").Value);
            if (q.Element("PriceBook").Value != "")
                price.PriceBook = (decimal)GetDecimal(q.Element("PriceBook").Value);
            if (q.Element("ExDividendDate").Value != "")
                price.ExDividendDate = (q.Element("ExDividendDate").Value);
            if (q.Element("PERatio").Value != "")
                price.PERatio = (decimal)GetDecimal(q.Element("PERatio").Value);
            if (q.Element("DividendPayDate").Value != "")
                price.DividendPayDate = (q.Element("DividendPayDate").Value);
            if (q.Element("PEGRatio").Value != "")
                price.PEGRatio = (decimal)GetDecimal(q.Element("PEGRatio").Value);
            if (q.Element("PriceEPSEstimateCurrentYear").Value != "")
                price.PriceEPSEstimateCurrentYear = (decimal)GetDecimal(q.Element("PriceEPSEstimateCurrentYear").Value);
            if (q.Element("PriceEPSEstimateNextYear").Value != "")
                price.PriceEPSEstimateNextYear = (decimal)GetDecimal(q.Element("PriceEPSEstimateNextYear").Value);

            if (q.Element("ShortRatio").Value != "")
                price.ShortRatio = (q.Element("ShortRatio").Value);
            if (q.Element("LastTradeTime").Value != "")
                price.LastTradeTime = (q.Element("LastTradeTime").Value);
            if (q.Element("TickerTrend").Value != "")
                price.TickerTrend = (string)(q.Element("TickerTrend").Value);
            if (q.Element("OneyrTargetPrice").Value != "")
                price.OneyrTargetPrice = (decimal)GetDecimal(q.Element("OneyrTargetPrice").Value);
            if (q.Element("Volume").Value != "")
                price.Volume = (decimal)GetDecimal(q.Element("Volume").Value);
            if (q.Element("YearRange").Value != "")
                price.YearRange = (q.Element("YearRange").Value);
            if (q.Element("DaysValueChange").Value != "")
                price.DaysValueChange = (string)(q.Element("DaysValueChange").Value);
            if (q.Element("DaysValueChangeRealtime").Value != "")
                price.DaysValueChangeRealtime = (q.Element("DaysValueChangeRealtime").Value);
            if (q.Element("StockExchange").Value != "")
                price.StockExchange = (q.Element("StockExchange").Value);
            if (q.Element("DividendYield").Value != "")
                price.DividendYield = (decimal)GetDecimal(q.Element("DividendYield").Value);
            return price;
        }

        private static decimal? GetDecimal(string input)
        {
            if (input == null) return null;
            input = input.Replace("%", "");
            decimal value;
            if (Decimal.TryParse(input, out value)) return value;
            return null;
        }

        private static DateTime? GetDateTime(string input)
        {
            if (input == null) return null;
            DateTime value = Convert.ToDateTime(input);
            if (value  != null) 
                return value;
            return null;
        }
    }

    
}