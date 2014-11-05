using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;

namespace TradeAgent.Transactions
{
    class t8412_주식분봉차트TR : XADataAdapter, _IXAQueryEvents
    {
        public delegate void OnReceiveDataComplete(List<ChartData> data);
        public event OnReceiveDataComplete OnReceiveComplete;

        public t8412_주식분봉차트TR()
        {
            resName = "t8412";
            query = getTR();
        }
        
        void _IXAQueryEvents.ReceiveData(string szTrCode)
        {
            string outblock = resName + "OutBlock1";
            // 압축해제
            query.Decompress(outblock);
            int count = query.GetBlockCount(outblock);
            List<ChartData> list = new List<ChartData>(count);


            string shcode = query.GetFieldData(resName + "OutBlock", "shcode", 0);
            ChartData data;
            for (int i = 0; i < count; i++)
            {
                data = new ChartData(shcode);
                data.date = query.GetFieldData(outblock, "date", i);
                data.time = query.GetFieldData(outblock, "time", i);
                data.시가 = Convert.ToInt64(query.GetFieldData(outblock, "open", i));
                data.고가 = Convert.ToInt64(query.GetFieldData(outblock, "high", i));
                data.저가 = Convert.ToInt64(query.GetFieldData(outblock, "low", i));
                data.종가 = Convert.ToInt64(query.GetFieldData(outblock, "close", i));
                data.거래량 = Convert.ToInt64(query.GetFieldData(outblock, "jdiff_vol", i));
                data.거래대금 = Convert.ToInt64(query.GetFieldData(outblock, "value", i));
                //data.수정구분 = Convert.ToInt64(query.GetFieldData(outblock, "jongchk", i));
                //data.수정비율 = Convert.ToDouble(query.GetFieldData(outblock, "rate", i));
                //data.종가등락구분 = Convert.ToInt16(query.GetFieldData(outblock, "sign", i));

                Console.WriteLine("[" + shcode + "] " + data.date + " " + data.time + " - " + data.종가);// + " => " + data.종가등락구분 + " - " + data.수정구분 + " " + data.수정비율);
                list.Add(data);
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
