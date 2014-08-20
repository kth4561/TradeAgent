using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeAgent.Model
{
    public class Stock : IEquatable<Stock>
    {
        public Stock()
        {

        }

        public Stock(string shcode)
        {
            this.shcode = shcode;
        }

        ////////////////
        /// t8430
        ////////////////

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
        public string hname;
        public string shcode;
        public string expcode;
        public bool etfgubun;
        public long uplmtprice;
        public long dnlmtprice;
        public long jnilclose;
        public long memedan;
        public long recprice;
        public int gubun;
        
        public bool isBad;
        public StockFinance finance;

        public bool Equals(Stock stock)
        {
            if (stock == null)
            {
                return false;
            }
            else
            {
                if (this.shcode.Equals(stock.shcode))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                Stock stockobj = obj as Stock;
                if (stockobj == null)
                {
                    return false;
                }
                else
                {
                    return Equals(stockobj);
                }
            }
        }
	}
}
