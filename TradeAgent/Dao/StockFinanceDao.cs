using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace TradeAgent
{
    public class StockFinanceDao
    {
        static string DB = @"Data Source=" + TradeAgent.Properties.Resources.DATABASE;

        //public int insert(List<Stock> stocks)
        public void insert(Stock stock)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DB))
            {
                conn.Open();

                string sql = "INSERT OR REPLACE INTO TB_STOCKFINANCE (shcode, 업종구분명, 최근결산년월, 주식수, 외국인비율, 자본금, 시가총액, 배당금, 배당수익율, 현재가, 결산구분, per, eps, pbr, roa, roe, ebitda, evebitda, sps, cps, bps, peg, t_per, t_eps, t_peg, 최근분기년도) VALUES (:shcode, :업종구분명, :최근결산년월, :주식수, :외국인비율, :자본금, :시가총액, :배당금, :배당수익율, :현재가, :결산구분, :per, :eps, :pbr, :roa, :roe, :ebitda, :evebitda, :sps, :cps, :bps, :peg, :t_per, :t_eps, :t_peg, :최근분기년도)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("shcode", DbType.String);
                    cmd.Parameters.Add("업종구분명", DbType.String);
                    cmd.Parameters.Add("최근결산년월", DbType.String);
                    cmd.Parameters.Add("주식수", DbType.Int64);
                    cmd.Parameters.Add("외국인비율", DbType.Double);
                    cmd.Parameters.Add("자본금", DbType.Double);
                    cmd.Parameters.Add("시가총액", DbType.Double);
                    cmd.Parameters.Add("배당금", DbType.Double);
                    cmd.Parameters.Add("배당수익율", DbType.Double);
                    cmd.Parameters.Add("현재가", DbType.Int64);
                    cmd.Parameters.Add("결산구분", DbType.String);
                    cmd.Parameters.Add("per", DbType.Double);
                    cmd.Parameters.Add("eps", DbType.Double);
                    cmd.Parameters.Add("pbr", DbType.Double);
                    cmd.Parameters.Add("roa", DbType.Double);
                    cmd.Parameters.Add("roe", DbType.Double);
                    cmd.Parameters.Add("ebitda", DbType.Double);
                    cmd.Parameters.Add("evebitda", DbType.Double);
                    cmd.Parameters.Add("sps", DbType.Double);
                    cmd.Parameters.Add("cps", DbType.Double);
                    cmd.Parameters.Add("bps", DbType.Double);
                    cmd.Parameters.Add("peg", DbType.Double);
                    cmd.Parameters.Add("t_per", DbType.Double);
                    cmd.Parameters.Add("t_eps", DbType.Double);
                    cmd.Parameters.Add("t_peg", DbType.Double);
                    cmd.Parameters.Add("최근분기년도", DbType.Double);
                    
                    using (SQLiteTransaction tx = conn.BeginTransaction())
                    {
                        cmd.Transaction = tx;

                        //foreach (Stock stock in stocks)
                        //{
                            cmd.Parameters["shcode"].Value = stock.shcode;
                            cmd.Parameters["업종구분명"].Value = stock.finance.업종구분명;
                            cmd.Parameters["최근결산년월"].Value = stock.finance.최근결산년월;
                            cmd.Parameters["주식수"].Value = stock.finance.주식수;
                            cmd.Parameters["외국인비율"].Value = stock.finance.외국인비율;
                            cmd.Parameters["자본금"].Value = stock.finance.자본금;
                            cmd.Parameters["시가총액"].Value = stock.finance.시가총액;
                            cmd.Parameters["배당금"].Value = stock.finance.배당금;
                            cmd.Parameters["배당수익율"].Value = stock.finance.배당수익율;
                            cmd.Parameters["현재가"].Value = stock.finance.현재가;
                            cmd.Parameters["결산구분"].Value = stock.finance.결산구분;
                            cmd.Parameters["per"].Value = stock.finance.per;
                            cmd.Parameters["eps"].Value = stock.finance.eps;
                            cmd.Parameters["pbr"].Value = stock.finance.pbr;
                            cmd.Parameters["roa"].Value = stock.finance.roa;
                            cmd.Parameters["roe"].Value = stock.finance.roe;
                            cmd.Parameters["ebitda"].Value = stock.finance.ebitda;
                            cmd.Parameters["evebitda"].Value = stock.finance.evebitda;
                            cmd.Parameters["sps"].Value = stock.finance.sps;
                            cmd.Parameters["cps"].Value = stock.finance.cps;
                            cmd.Parameters["bps"].Value = stock.finance.bps;
                            cmd.Parameters["peg"].Value = stock.finance.peg;
                            cmd.Parameters["t_per"].Value = stock.finance.t_per;
                            cmd.Parameters["t_eps"].Value = stock.finance.t_eps;
                            cmd.Parameters["t_peg"].Value = stock.finance.t_peg;
                            cmd.Parameters["최근분기년도"].Value = stock.finance.최근분기년도;
                            
                            cmd.ExecuteNonQuery();
                        //}
                        tx.Commit();
                    }
                }
                conn.Close();
            }

        }

    }
}
