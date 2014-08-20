using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;
using TradeAgent.Model;

namespace TradeAgent.Transactions.TR
{
    class t1405_MoreBadStockListTR : XADataAdapter, _IXAQueryEvents
    {
        public delegate void OnReceiveDataComplete(List<Stock> stocks, int chk);
        public event OnReceiveDataComplete OnReceiveComplete;
        public static string getType(int chk)
        {
            switch (chk)
            {
                case 1: return "투자경고종목";
                case 2: return "매매정지종목";
                case 3: return "정리매매종목";
                case 4: return "투자주의종목";
                case 5: return "투자위험종목";
                case 6: return "위험예고종목";
                case 7: return "단기과열지정종목";
                case 8: return "단기과열지졍예고종목";
            }
            return "";
        }

        List<Stock> data = new List<Stock>();

        public t1405_MoreBadStockListTR()
        {
            resName = "t1405";
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
            //해제일,edate,edate,char,8;
            //종목코드,shcode,shcode,char,6;
            for (int i = 0; i < count; i++)
            {
                //Console.WriteLine(i + " : " + query.GetFieldData(resName + "OutBlock", "hname", i));
                data.Add(new Stock(query.GetFieldData(outblock1, "shcode", i)));
            }

            string nextKey = query.GetFieldData(resName + "OutBlock", "cts_shcode", 0);
            if (nextKey.Equals(""))
            {
                // 데이터 처리가 완료되면 
                input = null;
                if (OnReceiveComplete != null)
                {
                    OnReceiveComplete.Invoke(data, Convert.ToInt32(query.GetFieldData(resName + "InBlock", "jongchk", 0)));
                    data = new List<Stock>();
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
