using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeAgent.Model
{
    public class Stock
    {
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

        ////////////////
        /// t3320
        ////////////////

        //t3320OutBlock
        //업종구분명,upgubunnm,upgubunnm,char,20;
        //시장구분,sijangcd,sijangcd,char,1;
        //시장구분명,marketnm,marketnm,char,10;
        //한글기업명,company,company,char,100;
        //본사주소,baddress,baddress,char,100;
        //본사전화번호,btelno,btelno,char,20;
        //최근결산년도,gsyyyy,gsyyyy,char,4;
        //결산월,gsmm,gsmm,char,2;
        //최근결산년월,gsym,gsym,char,6;
        //주당액면가,lstprice,lstprice,long,12;
        //주식수,gstock,gstock,long,12;
        //Homepage,homeurl,homeurl,char,50;
        //그룹명,grdnm,grdnm,char,30;
        //외국인,foreignratio,foreignratio,float,6.2;
        //주담전화,irtel,irtel,char,30;
        //자본금,capital,capital,float,12.0;
        //시가총액,sigavalue,sigavalue,float,12.0;
        //배당금,cashsis,cashsis,float,12.0;
        //배당수익율,cashrate,cashrate,float,13.2;
        //현재가,price,price,long,8;
        //전일종가,jnilclose,jnilclose,long,8; ---
        public string upgubunnm;
        public int gsyyyy;
        public string gsym;
        public long gstock;
        public float foreignratio;
        public float sigavalue;
        public float cashsis;
        public float cashrate;
        public float price;

        //t3320OutBlock1,기업재무정보,output;
        //기업코드,gicode,gicode,char,7;    --
        //결산년월,gsym,gsym,char,6;        --
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

	}
}
