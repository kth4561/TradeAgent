using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;

namespace TradeAgent.Transactions
{
    class t8430_주식종목조회TR : XADataAdapter, _IXAQueryEvents
    {
        public delegate void OnReceiveDataComplete(List<Stock> data);
        public event OnReceiveDataComplete OnReceiveComplete;

        public t8430_주식종목조회TR()
        {
            resName = "t8430";
            query = getTR();
        }
        
        void _IXAQueryEvents.ReceiveData(string szTrCode)
        {
            string outblock = resName + "OutBlock";
            int count = query.GetBlockCount(outblock);
            List<Stock> list = new List<Stock>(count);
            Stock stock;
            for (int i = 0; i < count; i++)
            {
                stock = new Stock();
                stock.hname = query.GetFieldData(outblock, "hname", i);
                stock.shcode = query.GetFieldData(outblock, "shcode", i);
                stock.expcode = query.GetFieldData(outblock, "expcode", i);
                stock.ETF여부 = query.GetFieldData(outblock, "etfgubun", i).Equals("1");
                stock.상한가 = Convert.ToInt64(query.GetFieldData(outblock, "uplmtprice", i));
                stock.하한가 = Convert.ToInt64(query.GetFieldData(outblock, "dnlmtprice", i));
                stock.전일가 = Convert.ToInt64(query.GetFieldData(outblock, "jnilclose", i));
                stock.주문수량단위 = Convert.ToInt64(query.GetFieldData(outblock, "memedan", i));
                stock.기준가 = Convert.ToInt64(query.GetFieldData(outblock, "recprice", i));
                stock.구분 = Convert.ToInt32(query.GetFieldData(outblock, "gubun", i));
                stock.우선주여부 = !stock.shcode.Substring(stock.shcode.Length - 1).Equals("0");
                //Console.WriteLine(stock.shcode.Substring(stock.shcode.Length - 1) + " : " + stock.shcode + " : " + stock.hname + " : " + stock.isPreferred + " : " + (!stock.shcode.Substring(stock.shcode.Length - 1).Equals("0")));

                list.Add(stock);
            }

            // 데이터 처리가 완료되면 
            input = null;
            if (OnReceiveComplete != null)
            {
                OnReceiveComplete.Invoke(list);
            }           
        }

        void _IXAQueryEvents.ReceiveMessage(bool bIsSystemError, string nMessageCode, string szMessage)
        {
            //tx.ReceiveMessage(m_query, bIsSystemError, nMessageCode, szMessage);
        }
    }
}
