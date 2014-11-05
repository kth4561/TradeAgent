using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeAgent
{
    public class ChartData
    {
        public ChartData()
        {

        }

        public ChartData(string shcode)
        {
            this.shcode = shcode;
        }

        public ChartData(string shcode, string date)
        {
            this.shcode = shcode;
            this.date = date;
        }

        public ChartData(string shcode, string date, string time)
        {
            this.shcode = shcode;
            this.date = date;
            this.time = time;
        }

        ////////////////
        /// t8412, t8413
        ////////////////
        public string shcode;
        public string date; //,date,char,8;
        public string time; //,time,char,10;
		public long 시가;//,open,open,long,8;
		public long 고가; //high,high,long,8;
		public long 저가;//,low,low,long,8;
		public long 종가;//,close,close,long,8;
		public long 거래량;//,jdiff_vol,jdiff_vol,long,12;
		public long 거래대금;//,value,value,long,12;
        //public long 수정구분;//,jongchk,jongchk,long,13;
        //public double 수정비율;//,rate,rate,double,6.2;
        //public long 수정주가반영항목;//,pricechk,pricechk,long,13;
        //public long 수정비율반영거래대금;//,ratevalue,ratevalue,long,12;
        //public int 종가등락구분;//(1:상한2:상승3:보합4:하한5:하락주식일만사용),sign,sign,char,1;


        //public bool Equals(Stock stock)
        //{
        //    if (stock == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (this.shcode.Equals(stock.shcode))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public override bool Equals(Object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        Stock stockobj = obj as Stock;
        //        if (stockobj == null)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return Equals(stockobj);
        //        }
        //    }
        //}

        //public void Merge(Stock target)
        //{
        //    foreach (var pi in typeof(Stock).GetProperties())
        //    {
        //        var priValue = pi.GetGetMethod().Invoke(this, null);
        //        var secValue = pi.GetGetMethod().Invoke(target, null);
        //        if (priValue == null || (pi.PropertyType.IsValueType && priValue.Equals(Activator.CreateInstance(pi.PropertyType))))
        //        {
        //            pi.GetSetMethod().Invoke(this, new object[] { secValue });
        //        }
        //    }
        //}
	}
}
