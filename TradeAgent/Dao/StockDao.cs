﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using TradeAgent.Model;

namespace TradeAgent.Dao
{
    public class StockDao
    {
        static string DB = @"Data Source=C:\Users\naver\Projects\TradeAgent\TradeAgent\TradeAgent.db";

        public int insert(List<Stock> stocks) {
            using (SQLiteConnection conn = new SQLiteConnection(DB))
            {
                conn.Open();

                string sql = "INSERT OR REPLACE INTO TB_STOCK (shcode, hname, expcode, etfgubun, uplmtprice, dnlmtprice, jnilclose, memedan, recprice, gubun, isbad) VALUES (:shcode, :hname, :expcode, :etfgubun, :uplmtprice, :dnlmtprice, :jnilclose, :memedan, :recprice, :gubun, :isbad)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("shcode", DbType.String);
                    cmd.Parameters.Add("hname", DbType.String);
                    cmd.Parameters.Add("expcode", DbType.String);
                    cmd.Parameters.Add("etfgubun", DbType.Boolean);
                    cmd.Parameters.Add("uplmtprice", DbType.Int64);
                    cmd.Parameters.Add("dnlmtprice", DbType.Int64);
                    cmd.Parameters.Add("jnilclose", DbType.Int64);
                    cmd.Parameters.Add("memedan", DbType.Int64);
                    cmd.Parameters.Add("recprice", DbType.Int64);
                    cmd.Parameters.Add("gubun", DbType.Int32);
                    cmd.Parameters.Add("isbad", DbType.Boolean);

                    using (SQLiteTransaction tx = conn.BeginTransaction())
                    {
                        cmd.Transaction = tx;

                        foreach (Stock stock in stocks)
                        {
                            cmd.Parameters["shcode"].Value = stock.shcode;
                            cmd.Parameters["hname"].Value = stock.hname;
                            cmd.Parameters["expcode"].Value = stock.expcode;
                            cmd.Parameters["etfgubun"].Value = stock.etfgubun;
                            cmd.Parameters["uplmtprice"].Value = stock.uplmtprice;
                            cmd.Parameters["dnlmtprice"].Value = stock.dnlmtprice;
                            cmd.Parameters["jnilclose"].Value = stock.jnilclose;
                            cmd.Parameters["memedan"].Value = stock.memedan;
                            cmd.Parameters["recprice"].Value = stock.recprice;
                            cmd.Parameters["gubun"].Value = stock.gubun;
                            cmd.Parameters["isbad"].Value = stock.isBad;
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                }
                conn.Close();
            }

            return stocks.Count;
        }
        
    }
}