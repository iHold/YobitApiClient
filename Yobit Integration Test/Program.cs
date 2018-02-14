using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yobit;
using static Yobit.YobitApiClient;

namespace Yobit_Integration_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var yobitClient = new Yobit.YobitApiClient("key", "secret", new YobitClientLogger());

            // PublicAPI
            //var info = yobitClient.Info();
            //var coinPairs = new CoinsPairs { new CoinPair ("ETH", "USD") };
            //var depth = yobitClient.Depth(coinPairs);
            //var ticker = yobitClient.Ticker(coinPairs);
            //var trades = yobitClient.Trades(coinPairs);

            // TradeAPI
            //var getInfo = yobitClient.GetInfo();
            //Thread.Sleep(1000);
            //var trade = yobitClient.Trade(new CoinPair("btc", "usd"), TradeOperationType.Sell, 15000, 0.10000001);

            // Total value of my wealth in rubles
            Console.WriteLine("TotalWealth in RUR:");
            var myTotalWealth = TotalWealth(yobitClient, "RUR");
            Thread.Sleep(1000);
            Console.WriteLine("==========================================================================");
            Console.WriteLine("TotalWealth in USD:");
            myTotalWealth = TotalWealth(yobitClient, "USD");
            //Console.WriteLine("==========================================================================");
            //Console.WriteLine("TotalWealth in BTC:");
            //myTotalWealth = TotalWealth(yobitClient, "BTC"); // wrong result !

            Console.ReadLine();
        }

        static float TotalWealth(Yobit.YobitApiClient yobitApiClient, string resultCoin)
        {
            resultCoin = resultCoin.ToLower();

            var accountInfo = yobitApiClient.GetInfo();
            if (accountInfo.success == 0) throw new Exception("Метод GetInfo возвратил ошибку: " + accountInfo.error);
            var accountCoinPairs = new CoinsPairs(accountInfo.@return.funds_incl_orders.Select(pair => new CoinPair(pair.Key, resultCoin)));
            var ticker = yobitApiClient.Ticker(accountCoinPairs);
            float sumWealth = 0;
            foreach(var coin in accountInfo.@return.funds_incl_orders.Keys)
            {
                if (coin == resultCoin) {
                    sumWealth += accountInfo.@return.funds_incl_orders[coin];
                    Console.WriteLine($"On balance: {accountInfo.@return.funds_incl_orders[resultCoin]}{resultCoin}");
                    continue;
                }
                var coinPair = new CoinPair(coin, resultCoin);
                var coinSumPrice = accountInfo.@return.funds_incl_orders[coin] * ticker[coinPair.ToString()].sell;
                if(coinPair.Reverted) coinSumPrice = accountInfo.@return.funds_incl_orders[coin] / ticker[coinPair.ToString()].sell; // на случай usd_rur
                sumWealth += coinSumPrice;

                Console.WriteLine($"{accountInfo.@return.funds_incl_orders[coin]}{coin} {(!coinPair.Reverted ? "*" : "/")} {ticker[coinPair.ToString()].sell}{resultCoin} = {coinSumPrice}{resultCoin}");
            }
            Console.WriteLine($"Total: {sumWealth}{resultCoin}");
            return sumWealth;
        }

        public class YobitClientLogger : IYobitClientLogger
        {
            public void Log(string message)
            {
                //Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}: {message}");
            }
        }
    }
}
