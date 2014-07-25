using System;
using System.Runtime.InteropServices.ComTypes;
using XA_DATASETLib;
using TradeAgent.Transactions;

namespace TradeAgent
{
    public class TransactionCtrl : _IXAQueryEvents
    {
        private IXAQuery m_query;
        private ITransitionTR tx;

        TransactionCtrl()
        {
        }

        public TransactionCtrl(ITransitionTR tx)
        {
            this.tx = tx;
            setResource(tx.getResName());
        }

        private IXAQuery setResource(string resName)
        {
            m_query = register(resName);
            return m_query;
        }

        
        public IXAQuery getXA()
        {
            return m_query;
        }

        /// <summary>
        /// 쿼리를 등록하고 해당 객체를 리턴한다.
        /// 서버와 통신하기 위한 포맷을 맞추는 작업 정도로 이해하면 된다.
        /// </summary>
        public IXAQuery register(string resName)
        {
            int dwCookie = 0;
            IConnectionPoint icp;
            IConnectionPointContainer icpc;

            IXAQuery query = new XAQuery();
            query.ResFileName = @"\Res\" + resName + ".res";
            icpc = (IConnectionPointContainer)query;
            Guid IID_QueryEvents = typeof(_IXAQueryEvents).GUID;
            icpc.FindConnectionPoint(ref IID_QueryEvents, out icp);
            icp.Advise(this, out dwCookie);

            return query;
        }

        void _IXAQueryEvents.ReceiveData(string szTrCode)
        {
            tx.ReceiveData(m_query, szTrCode);
        }

        void _IXAQueryEvents.ReceiveMessage(bool bIsSystemError, string nMessageCode, string szMessage)
        {
            tx.ReceiveMessage(m_query, bIsSystemError, nMessageCode, szMessage);
        }
    }
}
