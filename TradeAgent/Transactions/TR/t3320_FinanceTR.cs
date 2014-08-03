using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;
using TradeAgent.Model;

namespace TradeAgent.Transactions.TR
{
    class t3320_FinanceTR : XADataAdapter, _IXAQueryEvents
    {
        public delegate void OnReceiveDataComplete(StockFinance data);
        public event OnReceiveDataComplete OnReceiveComplete;

        public t3320_FinanceTR()
        {
            resName = "t3320";
            query = getTR();
        }

        void _IXAQueryEvents.ReceiveData(string szTrCode)
        {
            string outblock = resName + "OutBlock";
            string outblock1 = resName + "OutBlock1";

            int count = query.GetBlockCount(outblock);
            StockFinance stock = new StockFinance();
            //t3320OutBlock
            //업종구분명,upgubunnm,upgubunnm,char,20;
            //최근결산년월,gsym,gsym,char,6;
            //주식수,gstock,gstock,long,12;
            //외국인,foreignratio,foreignratio,float,6.2;
            //자본금,capital,capital,float,12.0;
            //시가총액,sigavalue,sigavalue,float,12.0;
            //배당금,cashsis,cashsis,float,12.0;
            //배당수익율,cashrate,cashrate,float,13.2;
            //현재가,price,price,long,8;

            //t3320OutBlock1,기업재무정보,output;
            //결산구분,gsgb,gsgb,char,1;
            //PER,per,per,float,13.2;
            //EPS,eps,eps,float,13.0;
            //PBR,pbr,pbr,float,13.2;
            //ROA,roa,roa,float,13.2;
            //ROE,roe,roe,float,13.2;
            //EBITDA,ebitda,ebitda,float,13.2;
            //EVEBITDA,evebitda,evebitda,float,13.2;
            //액면가,par,par,float,13.2;
            //SPS,sps,sps,float,13.2;
            //CPS,cps,cps,float,13.2;
            //BPS,bps,bps,float,13.0;
            //T.PER,t_per,t_per,float,13.2;
            //T.EPS,t_eps,t_eps,float,13.0;
            //PEG,peg,peg,float,13.2;
            //T.PEG,t_peg,t_peg,float,13.2;
            //최근분기년도,t_gsym,t_gsym,char,6;
            try
            {
                for (int i = 0; i < count; i++)
                {
                    stock.upgubunnm = query.GetFieldData(outblock, "upgubunnm", i);
                    stock.gsym = query.GetFieldData(outblock, "gsym", i);
                    stock.gstock = Convert.ToInt64(query.GetFieldData(outblock, "gstock", i));
                    stock.foreignratio = Convert.ToSingle(query.GetFieldData(outblock, "foreignratio", i));
                    stock.capital = Convert.ToSingle(query.GetFieldData(outblock, "capital", i));
                    stock.sigavalue = Convert.ToSingle(query.GetFieldData(outblock, "sigavalue", i));
                    stock.cashsis = Convert.ToSingle(query.GetFieldData(outblock, "cashsis", i));
                    stock.cashrate = Convert.ToSingle(query.GetFieldData(outblock, "cashrate", i));
                    stock.price = Convert.ToInt32(query.GetFieldData(outblock, "price", i));


                    stock.gsgb = query.GetFieldData(outblock1, "gsgb", i);
                    stock.per = Convert.ToSingle(query.GetFieldData(outblock1, "per", i));
                    stock.eps = Convert.ToSingle(query.GetFieldData(outblock1, "eps", i));
                    stock.pbr = Convert.ToSingle(query.GetFieldData(outblock1, "pbr", i));
                    stock.roa = Convert.ToSingle(query.GetFieldData(outblock1, "roa", i));
                    stock.roe = Convert.ToSingle(query.GetFieldData(outblock1, "roe", i));
                    stock.ebitda = Convert.ToSingle(query.GetFieldData(outblock1, "ebitda", i));
                    stock.evebitda = Convert.ToSingle(query.GetFieldData(outblock1, "evebitda", i));
                    stock.sps = Convert.ToSingle(query.GetFieldData(outblock1, "sps", i));
                    stock.cps = Convert.ToSingle(query.GetFieldData(outblock1, "cps", i));
                    stock.bps = Convert.ToSingle(query.GetFieldData(outblock1, "bps", i));
                    stock.peg = Convert.ToSingle(query.GetFieldData(outblock1, "peg", i));
                    stock.t_per = Convert.ToSingle(query.GetFieldData(outblock1, "t_per", i));
                    stock.t_eps = Convert.ToSingle(query.GetFieldData(outblock1, "t_eps", i));
                    stock.t_peg = Convert.ToSingle(query.GetFieldData(outblock1, "t_peg", i));
                    stock.t_gsym = Convert.ToSingle(query.GetFieldData(outblock1, "t_gsym", i));
                }
            }
            catch (System.FormatException e)
            {
                //Console.WriteLine(e.Message);
            }
            
            // 데이터 처리가 완료되면 
            input = null;
            if (OnReceiveComplete != null)
            {
                OnReceiveComplete.Invoke(stock);
            }
        }

        void _IXAQueryEvents.ReceiveMessage(bool bIsSystemError, string nMessageCode, string szMessage)
        {
            //tx.ReceiveMessage(m_query, bIsSystemError, nMessageCode, szMessage);
        }
    }
}
