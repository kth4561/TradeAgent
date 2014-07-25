using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;

namespace TradeAgent.Transactions
{
    public abstract class ITransitionTR
    { 
        protected string resName;
        public string getResName()
        {
            return this.resName;
        }
        public void ReceiveData(IXAQuery query, string szTrCode);
        public void ReceiveMessage(IXAQuery query, bool bIsSystemError, string nMessageCode, string szMessage);
    }
}
