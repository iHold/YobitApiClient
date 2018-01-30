using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yobit;
using static Yobit.YobitClient;

namespace Yobit_Integration_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var yobitClient = new YobitClient("key", "secret", new YobitClientLogger());
            //var info = yobitClient.Info();
            //var coinPairs = new CoinsPairs { { "ETH", "USD" } };
            //var depth = yobitClient.Depth(coinPairs);
            //var ticker = yobitClient.Ticker(coinPairs);
            //var trades = yobitClient.Trades(coinPairs);
            var getInfo = yobitClient.GetInfo();
            Thread.Sleep(1000);
            var trade = yobitClient.Trade(new CoinPair("btc", "usd"), TradeOperationType.Sell, 15000, 0.10000001);
            Console.ReadLine();
        }

        public class YobitClientLogger : IYobitClientLogger
        {
            public void Log(string message)
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}: {message}");
            }
        }
    }
}
