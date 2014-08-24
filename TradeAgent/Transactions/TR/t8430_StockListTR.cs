using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;
using TradeAgent.Model;

namespace TradeAgent.Transactions.TR
{
    class t8430_StockListTR : XADataAdapter, _IXAQueryEvents
    {
        public delegate void OnReceiveDataComplete(List<Stock> data);
        public event OnReceiveDataComplete OnReceiveComplete;

        public t8430_StockListTR()
        {
            resName = "t8430";
            query = getTR();
        }
        
        void _IXAQueryEvents.ReceiveData(string szTrCode)
        {
            string outblock = resName + "OutBlock";
            int count = query.GetBlockCount(outblock);
            List<Stock> data = new List<Stock>(count);
            Stock stock;
            //종목명,hname,hname,char,20;
            //단축코드,shcode,shcode,char,6;
            //확장코드,expcode,expcode,char,12;
            //ETF구분(1:ETF),etfgubun,etfgubun,char,1;
            //상한가,uplmtprice,uplmtprice,long,8;
            //하한가,dnlmtprice,dnlmtprice,long,8;
            //전일가,jnilclose,jnilclose,long,8;
            //주문수량단위,memedan,memedan,char,5;
            //기준가,recprice,recprice,long,8;
            //구분(1:코스피2:코스닥),gubun,gubun,char,1;
            for (int i = 0; i < count; i++)
            {
                stock = new Stock();
                stock.hname = query.GetFieldData(outblock, "hname", i);
                stock.shcode = query.GetFieldData(outblock, "shcode", i);
                stock.expcode = query.GetFieldData(outblock, "expcode", i);
                stock.etfgubun = query.GetFieldData(outblock, "etfgubun", i).Equals("1");
                stock.uplmtprice = Convert.ToInt64(query.GetFieldData(outblock, "uplmtprice", i));
                stock.dnlmtprice = Convert.ToInt64(query.GetFieldData(outblock, "dnlmtprice", i));
                stock.jnilclose = Convert.ToInt64(query.GetFieldData(outblock, "jnilclose", i));
                stock.memedan = Convert.ToInt64(query.GetFieldData(outblock, "memedan", i));
                stock.recprice = Convert.ToInt64(query.GetFieldData(outblock, "recprice", i));
                stock.gubun = Convert.ToInt32(query.GetFieldData(outblock, "gubun", i));
                stock.isPreferred = !stock.shcode.Substring(stock.shcode.Length - 1).Equals("0");
                //Console.WriteLine(stock.shcode.Substring(stock.shcode.Length - 1) + " : " + stock.shcode + " : " + stock.hname + " : " + stock.isPreferred + " : " + (!stock.shcode.Substring(stock.shcode.Length - 1).Equals("0")));
                
                data.Add(stock);
            }

            // 데이터 처리가 완료되면 
            input = null;
            if (OnReceiveComplete != null)
            {
                OnReceiveComplete.Invoke(data);
            }           
        }

        void _IXAQueryEvents.ReceiveMessage(bool bIsSystemError, string nMessageCode, string szMessage)
        {
            //tx.ReceiveMessage(m_query, bIsSystemError, nMessageCode, szMessage);
        }
    }
}
