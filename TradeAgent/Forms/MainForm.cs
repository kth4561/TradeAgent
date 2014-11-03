using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using TradeAgent.Transactions;
using System.Threading;

namespace TradeAgent
{
    public partial class MainForm : Form
    {
        SessionCtrl session;        // 섹션
        List<Stock> stocks;         // 종목 정보
        //Dictionary<string, Stock> etf = new Dictionary<string, Stock>();    // etf
        //List<string> badStocks=new List<string>();     // 나쁜 종목
        
        int gTmpindex = 0;              // 검색 인덱스 (임시)

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(SessionCtrl session) : this()
        {
            this.session = session;
        }

        // 불량 종목 조회 (총 4 종류)
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

        void OnReceiveBadStock(List<Stock> stocks, int chk)
        {
            rbConsole.Write(stocks.Count.ToString(), Color.Red);
            rbConsole.WriteLine(" 건");

            updateBadStock(stocks);
            if(chk < 4) {   //"관리종목","불성실공시종목","투자유의종목","투자환기종목"
                Thread.Sleep(1000);
                requestBadStock(++chk);
            }
            else
            {
                requestMoreBadStock(1);
            }
        }

        // 불량 종목 조회 (총 8 종류)
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

        void OnReceiveMoreBadStock(List<Stock> stocks, int chk)
        {
            rbConsole.Write(stocks.Count.ToString(), Color.Red);
            rbConsole.WriteLine(" 건");

            updateBadStock(stocks);
            if (chk < 8) // "투자경고종목","매매정지종목","정리매매종목", "투자주의종목","투자위험종목","위험예고종목","단기과열지정종목","단기과열지졍예고종목"
            {
                Thread.Sleep(1000);
                requestMoreBadStock(++chk);
            }
            else
            {
                // 종목 정보 DB 갱신
                rbConsole.Write("[DB반영] ", Color.GreenYellow);
                rbConsole.Write("종목 정보 : 총 ");
                new StockDao().insert(this.stocks);
                rbConsole.Write(this.stocks.Count.ToString(), Color.Red);
                rbConsole.WriteLine(" 건 반영완료");


                getStockFinance(this.stocks[gTmpindex]);
            }
        }

        // 전체 종목 조회
        void requestStocks()
        {
            rbConsole.WriteLine("[종목조회] ", Color.GreenYellow);
            t8430_주식종목조회TR tr = new t8430_주식종목조회TR();
            tr.OnReceiveComplete += OnReceiveStock;
            Hashtable ht = new Hashtable();
            ht.Add("gubun", 0);
            tr.request(ht);
            
        }

        void OnReceiveStock(List<Stock> stocks)
        {
            // 기본적으로 추출할 종목 정보 정리 완료
            this.stocks = stocks;
            
            // 불량 종목 조회
            requestBadStock(1);

            //Stock stock;
            //for (int i = stocks.Count - 1; i > 0; i--)
            //{
            //    stock = stocks[i];
            //    if (stock.etfgubun)
            //    {
            //        etf.Add(stock.hname, stock);
            //        stocks.RemoveAt(i);
            //    }
            //    if (badStocks.Contains(stock.shcode))
            //    {
            //        stocks.RemoveAt(i);
            //    }
            //}
            
            

            
            //rbConsole.Write(" - 불량종목 : 총 ");
            //rbConsole.Write(badStocks.Count.ToString(), Color.Red);
            //rbConsole.WriteLine(" 건 제외");

            //rbConsole.Write(" - ETF : 총 ");
            //rbConsole.Write(etf.Count.ToString(), Color.Red);
            //rbConsole.WriteLine(" 건");

            //rbConsole.Write(" - KOSPI/KOSDAQ : 총 ");
            //rbConsole.Write(this.stocks.Count.ToString(), Color.Red);
            //rbConsole.WriteLine(" 건");
            //rbConsole.WriteLine("[재무정보조회] ", Color.GreenYellow);




        }

        // 종목의 재무정보 단건 조회
        void getStockFinance(Stock stock)
        {
            if (stock.우선주여부 || stock.불량종목여부 || stock.ETF여부)
            {
                if (stock.우선주여부)
                {
                    rbConsole.Write(" " + gTmpindex + ") '" + stock.hname + "' 우선주는", Color.Gray);
                }
                else if (stock.불량종목여부)
                {
                    rbConsole.Write(" " + gTmpindex + ") '" + stock.hname + "' 불량종목은", Color.Gray);
                }
                else if (stock.ETF여부)
                {
                    rbConsole.Write(" " + gTmpindex + ") '" + stock.hname + "' ETF는", Color.Gray);
                }
                rbConsole.WriteLine(" 재무 정보 조회에서 제외", Color.Gray);
                getStockFinance(this.stocks[++gTmpindex]);
            }
            else
            {
                rbConsole.Write(" " + gTmpindex + ") '" + stock.hname + "(" + stock.shcode + ")' 조회 중...");
                t3320_재무요약TR tr = new t3320_재무요약TR();
                tr.OnReceiveComplete += OnReceiveStockFinance;
                Hashtable ht = new Hashtable();
                ht.Add("gicode", stock.shcode);
                tr.request(ht);
            }
        }

        void OnReceiveStockFinance(StockFinance stockfnc)
        {
            Stock stock = this.stocks[gTmpindex];
            stock.finance = stockfnc;
            // 종목 정보 DB 갱신
            new StockFinanceDao().insert(stock);
            rbConsole.WriteLine(" 완료", Color.Violet);
           
            //if (gTmpindex < 10)
            if (stocks.Count - 1 > gTmpindex)
            {
                Thread.Sleep(1000);
                getStockFinance(this.stocks[++gTmpindex]);
            }
            else
            {
                rbConsole.Write("[재무]", Color.GreenYellow);
                rbConsole.Write(" 조회");
                rbConsole.WriteLine(" 완료", Color.Violet);
            }
        }

        private void btSync_Click(object sender, System.EventArgs e)
        {
            //rbConsole.WriteLine("[불량종목] ", Color.GreenYellow);
            //requestBadStock(1);
            gTmpindex = 0;
            requestStocks();
        }

        // 나쁜 종목 표시하는 함수
        private void updateBadStock(List<Stock> stocks) {
            int index = -1;
            foreach (Stock stock in stocks)
            {
                index = this.stocks.IndexOf(stock);
                if (index != -1)
                {
                    this.stocks[index].불량종목여부 = true;
                }
            }
        }

        private void rbConsole_TextChanged(object sender, System.EventArgs e)
        {
            rbConsole.ScrollToCaret();
        }
    }
}
