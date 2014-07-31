using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeAgent
{
    class TransactionEventArgs : EventArgs
    {
        List<t8430_OutputTR> _args;
        public List<t8430_OutputTR> args
        {
            get
            {
                Console.WriteLine("get : " + this._args.Count);
                return this._args;
            }
        }
        public TransactionEventArgs(List<t8430_OutputTR> data)
        {
            Console.WriteLine("생성자 : " + data.Count);
            this._args = data;
        }


    }
}
