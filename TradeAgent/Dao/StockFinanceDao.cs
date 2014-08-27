using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using TradeAgent.Model;

namespace TradeAgent.Dao
{
    public class StockFinanceDao
    {
        static string DB = @"Data Source=C:\Users\naver\Projects\TradeAgent\TradeAgent\TradeAgent.db";

        //public int insert(List<Stock> stocks)
        public void insert(Stock stock)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DB))
            {
                conn.Open();

                string sql = "INSERT OR REPLACE INTO TB_STOCKFINANCE (shcode, upgubunnm, gsym, gstock, foreignratio, capital, sigavalue, cashsis, cashrate, price, gsgb, per, eps, pbr, roa, roe, ebitda, evebitda, sps, cps, bps, peg, t_per, t_eps, t_peg, t_gsym) VALUES (:shcode, :upgubunnm, :gsym, :gstock, :foreignratio, :capital, :sigavalue, :cashsis, :cashrate, :price, :gsgb, :per, :eps, :pbr, :roa, :roe, :ebitda, :evebitda, :sps, :cps, :bps, :peg, :t_per, :t_eps, :t_peg, :t_gsym)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("shcode", DbType.String);
                    cmd.Parameters.Add("upgubunnm", DbType.String);
                    cmd.Parameters.Add("gsym", DbType.String);
                    cmd.Parameters.Add("gstock", DbType.Int64);
                    cmd.Parameters.Add("foreignratio", DbType.Double);
                    cmd.Parameters.Add("capital", DbType.Double);
                    cmd.Parameters.Add("sigavalue", DbType.Double);
                    cmd.Parameters.Add("cashsis", DbType.Double);
                    cmd.Parameters.Add("cashrate", DbType.Double);
                    cmd.Parameters.Add("price", DbType.Int64);
                    cmd.Parameters.Add("gsgb", DbType.String);
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
                    cmd.Parameters.Add("t_gsym", DbType.Double);
                    
                    using (SQLiteTransaction tx = conn.BeginTransaction())
                    {
                        cmd.Transaction = tx;

                        //foreach (Stock stock in stocks)
                        //{
                            cmd.Parameters["shcode"].Value = stock.shcode;
                            cmd.Parameters["upgubunnm"].Value = stock.finance.upgubunnm;
                            cmd.Parameters["gsym"].Value = stock.finance.gsym;
                            cmd.Parameters["gstock"].Value = stock.finance.gstock;
                            cmd.Parameters["foreignratio"].Value = stock.finance.foreignratio;
                            cmd.Parameters["capital"].Value = stock.finance.capital;
                            cmd.Parameters["sigavalue"].Value = stock.finance.sigavalue;
                            cmd.Parameters["cashsis"].Value = stock.finance.cashsis;
                            cmd.Parameters["cashrate"].Value = stock.finance.cashrate;
                            cmd.Parameters["price"].Value = stock.finance.price;
                            cmd.Parameters["gsgb"].Value = stock.finance.gsgb;
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
                            cmd.Parameters["t_gsym"].Value = stock.finance.t_gsym;
                            
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
