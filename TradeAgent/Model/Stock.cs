using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeAgent
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
        public string hname; //hname; hname,hname,char,20
        public string shcode; //shcode; shcode,shcode,char,6
        public string expcode; //expcode; expcode,expcode,char,12
        public bool ETF여부; //etfgubun; (1:ETF),etfgubun,etfgubun,char,1
        public long 상한가; //uplmtprice; ,uplmtprice,uplmtprice,long,8
        public long 하한가; //dnlmtprice; dnlmtprice,dnlmtprice,long,8
        public long 전일가; //jnilclose; jnilclose,jnilclose,long,8
        public long 주문수량단위; //memedan; memedan,memedan,char,5
        public long 기준가; // recprice; recprice,recprice,long,8
        public int 구분; // gubun; (1:코스피2:코스닥),gubun,gubun,char
        
        public bool 불량종목여부;          // 불량종목
        public bool 우선주여부;    // 우선주
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

        public void Merge(Stock target)
        {
            foreach (var pi in typeof(Stock).GetProperties())
            {
                var priValue = pi.GetGetMethod().Invoke(this, null);
                var secValue = pi.GetGetMethod().Invoke(target, null);
                if (priValue == null || (pi.PropertyType.IsValueType && priValue.Equals(Activator.CreateInstance(pi.PropertyType))))
                {
                    pi.GetSetMethod().Invoke(this, new object[] { secValue });
                }
            }
        }
	}
}
