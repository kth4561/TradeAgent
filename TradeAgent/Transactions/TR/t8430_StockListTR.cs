using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;

namespace TradeAgent.Transactions.TR
{
    class t8430_StockListTR : XADataAdapter, _IXAQueryEvents
    {
        public delegate void OnReceiveDataComplete(List<t8430_OutputTR> data);
        public event OnReceiveDataComplete OnReceiveComplete;

        public t8430_StockListTR()
        {
            resName = "t8430";
            query = getTR();
        }
        
        void _IXAQueryEvents.ReceiveData(string szTrCode)
        {
            //tx.ReceiveData(m_query, szTrCode);

            List<t8430_OutputTR> data = new List<t8430_OutputTR>();
            t8430_OutputTR stock;
            int count = query.GetBlockCount(resName + "OutBlock");
            ////종목명,hname,hname,char,20;
            ////단축코드,shcode,shcode,char,6;
            ////확장코드,expcode,expcode,char,12;
            ////ETF구분(1:ETF),etfgubun,etfgubun,char,1;
            ////상한가,uplmtprice,uplmtprice,long,8;
            ////하한가,dnlmtprice,dnlmtprice,long,8;
            ////전일가,jnilclose,jnilclose,long,8;
            ////주문수량단위,memedan,memedan,char,5;
            ////기준가,recprice,recprice,long,8;
            ////구분(1:코스피2:코스닥),gubun,gubun,char,1;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i + " : " + query.GetFieldData("OutBlock", "hname", i));
                stock = new t8430_OutputTR();
                stock.hname = query.GetFieldData("OutBlock", "hname", i);
                //...




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
