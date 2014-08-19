using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using TradeAgent.Model;
using TradeAgent.Transactions.TR;
using System.Threading;

namespace TradeAgent.Forms
{
    public partial class MainForm : Form
    {
        SessionCtrl session;        // 섹션
        List<Stock> stocks;         // 종목 정보
        Dictionary<string, Stock> etf = new Dictionary<string, Stock>();    // etf
        List<string> badStocks=new List<string>();     // 나쁜 종목
        
        int index = 0;              // 검색 인덱스 (임시)

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(SessionCtrl session) : this()
        {
            this.session = session;
            
            rbConsole.WriteLine("[불량종목] ", Color.GreenYellow);
            requestBadStock(1);
            
        }

        void requestBadStock(int chk)
        {
            t1404_BadStockListTR tr = new t1404_BadStockListTR();
            tr.OnReceiveComplete += OnReceiveBadStock;
            Hashtable ht = new Hashtable();
            ht.Add("gubun", 0);
            ht.Add("jongchk", chk);
            rbConsole.Write(" - " + t1404_BadStockListTR.getType(chk) + " : ");
            tr.request(ht);
        }

        void OnReceiveBadStock(List<string> stocks, int chk)
        {
            rbConsole.Write(stocks.Count.ToString(), Color.Red);
            rbConsole.WriteLine(" 건");
            
            badStocks.AddRange(stocks);
            if(chk < 4) {
                Thread.Sleep(1000);
                requestBadStock(++chk);
            }
            else
            {
                requestMoreBadStock(1);
            }
        }

        void requestMoreBadStock(int chk)
        {
            t1405_MoreBadStockListTR tr = new t1405_MoreBadStockListTR();
            tr.OnReceiveComplete += OnReceiveMoreBadStock;
            Hashtable ht = new Hashtable();
            ht.Add("gubun", 0);
            ht.Add("jongchk", chk);
            rbConsole.Write(" - " + t1405_MoreBadStockListTR.getType(chk) + " : ");
            tr.request(ht);
        }

        void OnReceiveMoreBadStock(List<string> stocks, int chk)
        {
            rbConsole.Write(stocks.Count.ToString(), Color.Red);
            rbConsole.WriteLine(" 건");

            badStocks.AddRange(stocks);
            if (chk < 8)
            {
                Thread.Sleep(1000);
                requestMoreBadStock(++chk);
            }
            else
            {
                requestStocks();
            }
        }

        void requestStocks()
        {
            rbConsole.WriteLine("[종목조회] ", Color.GreenYellow);
            t8430_StockListTR tr = new t8430_StockListTR();
            tr.OnReceiveComplete += OnReceiveStock;
            Hashtable ht = new Hashtable();
            ht.Add("gubun", 0);
            tr.request(ht);
            
        }

        void OnReceiveStock(List<Stock> stocks)
        {
            // 기본적으로 추출할 종목 정보 정리 완료
            new Dao.StockDao().insert(stocks);
            
            Stock stock;
            for (int i = stocks.Count - 1; i > 0; i--)
            {
                stock = stocks[i];
                if (stock.etfgubun)
                {
                    etf.Add(stock.hname, stock);
                    stocks.RemoveAt(i);
                }
                if (badStocks.Contains(stock.hname))
                {
                    stocks.RemoveAt(i);
                }
            }
            this.stocks = stocks;
            

            
            rbConsole.Write(" - 불량종목 : 총 ");
            rbConsole.Write(badStocks.Count.ToString(), Color.Red);
            rbConsole.WriteLine(" 건 제외");

            rbConsole.Write(" - ETF : 총 ");
            rbConsole.Write(etf.Count.ToString(), Color.Red);
            rbConsole.WriteLine(" 건");

            rbConsole.Write(" - KOSPI/KOSDAQ : 총 ");
            rbConsole.Write(this.stocks.Count.ToString(), Color.Red);
            rbConsole.WriteLine(" 건");
            rbConsole.WriteLine("[재무정보조회] ", Color.GreenYellow);
            
            


            getStockFinance(stocks[index]);
        }

        void getStockFinance(Stock stock)
        {   
            rbConsole.Write(" " + index + ") '" + stock.hname + "' 조회 중...");
            t3320_FinanceTR tr = new t3320_FinanceTR();
            tr.OnReceiveComplete += OnReceiveStockFinance;
            Hashtable ht = new Hashtable();
            ht.Add("gicode", stock.shcode);
            tr.request(ht);
        }

        void OnReceiveStockFinance(StockFinance data)
        {
            rbConsole.WriteLine(" 완료", Color.Violet);
            if (index < 10)
            //if (stocks.Count - 1 > index)
            {
                Thread.Sleep(1000);
                Stock stock = stocks[++index];
                stock.finance = data;
                getStockFinance(stock);
            }
            else
            {
                rbConsole.Write("[재무]",  Color.GreenYellow);
                rbConsole.Write(" 조회");
                rbConsole.WriteLine(" 완료",  Color.Violet);
            }
        }
    }
}
