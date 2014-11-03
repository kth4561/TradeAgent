using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XA_DATASETLib;

namespace TradeAgent.Transactions
{
    class t3320_재무요약TR : XADataAdapter, _IXAQueryEvents
    {
        public delegate void OnReceiveDataComplete(StockFinance data);
        public event OnReceiveDataComplete OnReceiveComplete;

        public t3320_재무요약TR()
        {
            resName = "t3320";
            query = getTR();
        }

        void _IXAQueryEvents.ReceiveData(string szTrCode)
        {
            string outblock = resName + "OutBlock";
            string outblock1 = resName + "OutBlock1";
            string shcode = query.GetFieldData(outblock1, "gicode", 0);

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

            StockFinance stock = new StockFinance();
            stock.업종구분명 = query.GetFieldData(outblock, "upgubunnm", 0);
            stock.최근결산년월 = query.GetFieldData(outblock, "gsym", 0);
            stock.주식수 = Convert.ToInt64(query.GetFieldData(outblock, "gstock", 0));
            stock.외국인비율 = Convert.ToSingle(query.GetFieldData(outblock, "foreignratio", 0));
            stock.자본금 = Convert.ToSingle(query.GetFieldData(outblock, "capital", 0));
            stock.시가총액 = Convert.ToSingle(query.GetFieldData(outblock, "sigavalue", 0));
            stock.배당금 = Convert.ToSingle(query.GetFieldData(outblock, "cashsis", 0));
            stock.배당수익율 = Convert.ToSingle(query.GetFieldData(outblock, "cashrate", 0));
            stock.현재가 = Convert.ToInt32(query.GetFieldData(outblock, "price", 0));

            if (!"".Equals(shcode))
            {
                try
                {
                    stock.결산구분 = query.GetFieldData(outblock1, "gsgb", 0);
                    stock.per = Convert.ToSingle(query.GetFieldData(outblock1, "per", 0));
                    stock.eps = Convert.ToSingle(query.GetFieldData(outblock1, "eps", 0));
                    stock.pbr = Convert.ToSingle(query.GetFieldData(outblock1, "pbr", 0));
                    stock.roa = Convert.ToSingle(query.GetFieldData(outblock1, "roa", 0));
                    stock.roe = Convert.ToSingle(query.GetFieldData(outblock1, "roe", 0));
                    stock.ebitda = Convert.ToSingle(query.GetFieldData(outblock1, "ebitda", 0));
                    stock.evebitda = Convert.ToSingle(query.GetFieldData(outblock1, "evebitda", 0));
                    stock.sps = Convert.ToSingle(query.GetFieldData(outblock1, "sps", 0));
                    stock.cps = Convert.ToSingle(query.GetFieldData(outblock1, "cps", 0));
                    stock.bps = Convert.ToSingle(query.GetFieldData(outblock1, "bps", 0));
                    stock.peg = Convert.ToSingle(query.GetFieldData(outblock1, "peg", 0));
                    stock.t_per = Convert.ToSingle(query.GetFieldData(outblock1, "t_per", 0));
                    stock.t_eps = Convert.ToSingle(query.GetFieldData(outblock1, "t_eps", 0));
                    stock.t_peg = Convert.ToSingle(query.GetFieldData(outblock1, "t_peg", 0));
                    stock.최근분기년도 = Convert.ToSingle(query.GetFieldData(outblock1, "t_gsym", 0));
                }
                catch (System.FormatException e)
                {
                    //Console.WriteLine(e.Message);
                }
            }
            
            // 데이터 처리가 완료되면 
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
