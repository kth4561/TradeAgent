using System;
using System.Runtime.InteropServices.ComTypes;
using XA_DATASETLib;

namespace TradeAgent
{
    class AbstractReal //: _IXARealEvents
    {
        IXAReal m_query;

        AbstractReal(string regName)
        {
            m_query = register(regName);
        }
        
        /// <summary>
        /// 실시간조회를 등록하고 해당 객체를 리턴한다.
        /// </summary>
        private IXAReal register(string resName)
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
