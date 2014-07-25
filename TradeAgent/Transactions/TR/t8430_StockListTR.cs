using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;

namespace TradeAgent.Transactions.TR
{
    class t8430_StockListTR : ITransitionTR
    {
        public t8430_StockListTR()
        {
            resName = "t84300";
        }

        public void ReceiveData(IXAQuery query, string szTrCode)
        {
            int count = query.GetBlockCount("t8430OutBlock");
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
                Console.WriteLine(i + " : " + query.GetFieldData("t8430OutBlock", "hname", i));
            }
        }

        public void ReceiveMessage(IXAQuery query, bool bIsSystemError, string nMessageCode, string szMessage)
        {
           
        }
    }
}
