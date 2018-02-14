# YobitApiClient

Yobit.net Api Client


var yobitClient = new YobitClient("key", "secret", new YobitClientLogger());

var getInfo = yobitClient.GetInfo();

Thread.Sleep(1000);

var trade = yobitClient.Trade(new CoinPair("btc", "usd"), TradeOperationType.Sell, 15000, 0.10000001);
