using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;
using TradeAgent.Model;

namespace TradeAgent.Transactions.TR
{
    class t1404_BadStockListTR : XADataAdapter, _IXAQueryEvents
    {
        public delegate void OnReceiveDataComplete(List<string> names, int chk);
        public event OnReceiveDataComplete OnReceiveComplete;
        public static string getType(int chk) {
            switch (chk)
            {
                case 1: return "관리종목";
                case 2: return "불성실공시종목";
                case 3: return "투자유의종목";
                case 4: return "투자환기종목";
            }
            return "";
        }

        List<string> data = new List<string>();
            
        public t1404_BadStockListTR()
        {
            resName = "t1404";
            query = getTR();
        }

        void _IXAQueryEvents.ReceiveData(string szTrCode)
        {
            string outblock1 = resName + "OutBlock1";
            int count = query.GetBlockCount(outblock1);
            //한글명,hname,hname,char,20;
            //현재가,price,price,long,8;
            //전일대비구분,sign,sign,char,1;
            //전일대비,change,change,long,8;
            //등락율,diff,diff,float,6.2;
            //누적거래량,volume,volume,long,12;
            //지정일,date,date,char,8;
            //지정일주가,tprice,tprice,long,8;
            //지정일대비,tchange,tchange,long,8;
            //대비율,tdiff,tdiff,float,6.2;
            //사유,reason,reason,char,4;
            //종목코드,shcode,shcode,char,6;
            //해제일,edate,edate,char,8;
            for (int i = 0; i < count; i++)
            {
                //Console.WriteLine(i + " : " + query.GetFieldData(resName + "OutBlock", "hname", i));
                data.Add(query.GetFieldData(outblock1, "hname", i));
            }

            string nextKey = query.GetFieldData(resName + "OutBlock", "cts_shcode", 0);
            if (nextKey.Equals(""))
            {
                // 데이터 처리가 완료되면 
                input = null;
                if (OnReceiveComplete != null)
                {
                    OnReceiveComplete.Invoke(data, Convert.ToInt32(query.GetFieldData(resName + "InBlock", "jongchk", 0)));
                    data = new List<string>();
                }
            }
            else
            {
                query.SetFieldData(resName + "InBlock", "cts_shcode", 0, nextKey);
                query.Request(true);
            }
        }

        void _IXAQueryEvents.ReceiveMessage(bool bIsSystemError, string nMessageCode, string szMessage)
        {
            //tx.ReceiveMessage(m_query, bIsSystemError, nMessageCode, szMessage);
        }
    }
}
