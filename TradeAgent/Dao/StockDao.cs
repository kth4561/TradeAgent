using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace TradeAgent
{
    public class StockDao
    {
        static string DB = @"Data Source=" + TradeAgent.Properties.Resources.DATABASE;

        public int insert(List<Stock> stocks) {
            using (SQLiteConnection conn = new SQLiteConnection(DB))
            {
                conn.Open();

                string sql = "INSERT OR REPLACE INTO TB_STOCK (shcode, hname, expcode, ETF여부, 상한가, 하한가, 전일가, 주문수량단위, 기준가, 구분, 불량종목여부) VALUES (:shcode, :hname, :expcode, :ETF여부, :상한가, :하한가, :전일가, :주문수량단위, :기준가, :구분, :불량종목여부)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("shcode", DbType.String);
                    cmd.Parameters.Add("hname", DbType.String);
                    cmd.Parameters.Add("expcode", DbType.String);
                    cmd.Parameters.Add("ETF여부", DbType.Boolean);
                    cmd.Parameters.Add("상한가", DbType.Int64);
                    cmd.Parameters.Add("하한가", DbType.Int64);
                    cmd.Parameters.Add("전일가", DbType.Int64);
                    cmd.Parameters.Add("주문수량단위", DbType.Int64);
                    cmd.Parameters.Add("기준가", DbType.Int64);
                    cmd.Parameters.Add("구분", DbType.Int32);
                    cmd.Parameters.Add("불량종목여부", DbType.Boolean);

                    using (SQLiteTransaction tx = conn.BeginTransaction())
                    {
                        cmd.Transaction = tx;

                        foreach (Stock stock in stocks)
                        {
                            cmd.Parameters["shcode"].Value = stock.shcode;
                            cmd.Parameters["hname"].Value = stock.hname;
                            cmd.Parameters["expcode"].Value = stock.expcode;
                            cmd.Parameters["ETF여부"].Value = stock.ETF여부;
                            cmd.Parameters["상한가"].Value = stock.상한가;
                            cmd.Parameters["하한가"].Value = stock.하한가;
                            cmd.Parameters["전일가"].Value = stock.전일가;
                            cmd.Parameters["주문수량단위"].Value = stock.주문수량단위;
                            cmd.Parameters["기준가"].Value = stock.기준가;
                            cmd.Parameters["구분"].Value = stock.구분;
                            cmd.Parameters["불량종목여부"].Value = stock.불량종목여부;
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
