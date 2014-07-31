using System;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using XA_DATASETLib;
using TradeAgent.Transactions;

namespace TradeAgent.Transactions
{
    public class XADataAdapter
    {
        #region 타입에 대한 정의
        public static int ALL = 0;
        public static int KOSPI = 1;
        public static int KOSDAQ = 2;
        #endregion

        protected IXAQuery query;
        protected Hashtable input = null;
        protected string resName;

        public void request(Hashtable ht)
        {
            // null이면 진행중이 아님.
            if (input == null)
            {
                input = ht;
                ICollection keys = input.Keys;
                foreach (object key in keys)
                {
                   // Console.WriteLine(key + " : " + input[key]);
                    query.SetFieldData(resName + "InBlock", key.ToString(), 0, input[key].ToString());
                }
                query.Request(false);
            }

        }

        /// <summary>
        /// 쿼리를 등록하고 해당 객체를 리턴한다.
        /// 서버와 통신하기 위한 포맷을 맞추는 작업 정도로 이해하면 된다.
        /// </summary>
        public IXAQuery getTR()
        {
            int dwCookie = 0;
            IConnectionPoint icp;
            IConnectionPointContainer icpc;

            IXAQuery query = new XAQuery();
            //query.ResFileName = @"\Res\" + resName + ".res";
            icpc = (IConnectionPointContainer)query;
            Guid IID_QueryEvents = typeof(_IXAQueryEvents).GUID;
            icpc.FindConnectionPoint(ref IID_QueryEvents, out icp);
            icp.Advise(this, out dwCookie);

            return query;
        }

        /// <summary>
        /// 실시간조회를 등록하고 해당 객체를 리턴한다.
        /// </summary>
        private IXAReal getReal()
        {
            IConnectionPoint icp;
            IConnectionPointContainer icpc;

            ////            S3_.res; 용도?
            int dwCookie = 0;
            IXAReal real = new XAReal();
            //real.ResFileName = "Res\\" + resName + ".res";     //KOSPI체결
            icpc = (IConnectionPointContainer)real;
            Guid IID_RealEvents = typeof(_IXARealEvents).GUID;
            icpc.FindConnectionPoint(ref IID_RealEvents, out icp);
            icp.Advise(this, out dwCookie);

            return real;
        }
    }
}
