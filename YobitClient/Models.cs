using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YobitApiClient.Models.PublicApi.Info
{
    public class InfoResponse
    {
        public int server_time { get; set; }
        public Pairs pairs { get; set; }
    }

    public class Pairs : Dictionary<string, PairInfo>
    {
    }

    public class PairInfo
    {
        /// <summary>
        /// количество разрешенных знаков после запятой
        /// </summary>
        public int decimal_places { get; set; }
        /// <summary>
        /// минимальная разрешенная цена
        /// </summary>
        public float min_price { get; set; }
        /// <summary>
        /// максимальная разрешенная цена
        /// </summary>
        public int max_price { get; set; }
        /// <summary>
        /// минимальное разрешенное количество для покупки или продажи
        /// </summary>
        public float min_amount { get; set; }
        /// <summary>
        /// пара скрыта (0 или 1)
        /// </summary>
        public int hidden { get; set; }
        /// <summary>
        /// комиссия пары
        /// </summary>
        public float fee { get; set; }
    }

}

namespace YobitApiClient.Models.PublicApi.Ticker
{
    public class TickerResponse : Dictionary<string, PairInfo>
    {
    }

    public class PairInfo
    {
        /// <summary>
        /// макcимальная цена
        /// </summary>
        public float high { get; set; }
        /// <summary>
        /// минимальная цена
        /// </summary>
        public float low { get; set; }
        /// <summary>
        /// средняя цена
        /// </summary>
        public float avg { get; set; }
        /// <summary>
        /// объем торгов
        /// </summary>
        public float vol { get; set; }
        /// <summary>
        /// объем торгов в валюте
        /// </summary>
        public float vol_cur { get; set; }
        /// <summary>
        /// цена последней сделки
        /// </summary>
        public float last { get; set; }
        /// <summary>
        /// цена покупки
        /// </summary>
        public float buy { get; set; }
        /// <summary>
        /// цена продажи
        /// </summary>
        public float sell { get; set; }
        /// <summary>
        /// последнее обновление кэша
        /// </summary>
        public int updated { get; set; }
    }
}

namespace YobitApiClient.Models.TradeApi
{
    public class YobitTradeApiResponse
    {
        public int success { get; set; }
        public string error { get; set; }
    }
}

namespace YobitApiClient.Models.TradeApi.GetInfo
{
    public class GetInfoResponse : YobitTradeApiResponse
    {
        public Return @return { get; set; }
    }

    public class Return
    {
        /// <summary>
        /// баланс аккаунта, доступный к использованию (не включает деньги на открытых ордерах)
        /// </summary>
        public Funds funds { get; set; }
        /// <summary>
        /// баланс аккаунта, доступный к использованию (включает деньги на открытых ордерах)
        /// </summary>
        public Funds_Incl_Orders funds_incl_orders { get; set; }
        /// <summary>
        /// привилегии ключа. withdraw не используется (зарезервировано)
        /// </summary>
        public Rights rights { get; set; }
        [Obsolete]
        /// <summary>
        /// всегда 0 (устарело)
        /// </summary>
        public int transaction_count { get; set; }
        [Obsolete]
        /// <summary>
        /// всегда 0 (устарело)
        /// </summary>
        public int open_orders { get; set; }
        /// <summary>
        /// время сервера
        /// </summary>
        public int server_time { get; set; }
    }

    public class Funds : Dictionary<string, float>
    {
    }

    public class Funds_Incl_Orders : Dictionary<string, float>
    {
    }

    public class Rights
    {
        public int info { get; set; }
        public int trade { get; set; }
        public int withdraw { get; set; }
    }
}

namespace YobitApiClient.Models.TradeApi.Trade
{
    public class TradeResponse : YobitTradeApiResponse
    {
        public Return @return { get; set; }
    }

    public class Return
    {
        /// <summary>
        /// сколько валюты куплено/продано
        /// </summary>
        public float received { get; set; }
        /// <summary>
        /// сколько валюты осталось купить/продать
        /// </summary>
        public int remains { get; set; }
        /// <summary>
        /// ID созданного ордера
        /// </summary>
        public int order_id { get; set; }
        /// <summary>
        /// балансы, актуальные после запроса
        /// </summary>
        public Funds funds { get; set; }
    }

    public class Funds : Dictionary<string, float>
    {
    }

}
