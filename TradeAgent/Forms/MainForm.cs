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
        int index = 0;              // 검색 인덱스

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(SessionCtrl session) : this()
        {
            this.session = session;
            requestStocks();
        }

        void requestStocks()
        {
            rbConsole.Write("[종목조회] ", Color.GreenYellow);
            t8430_StockListTR tr = new t8430_StockListTR();
            tr.OnReceiveComplete += OnReceiveStock;
            Hashtable ht = new Hashtable();
            ht.Add("gubun", 0);
            tr.request(ht);
            rbConsole.WriteLine("조회 중...");
            
        }

        void OnReceiveStock(List<Stock> data) {
            rbConsole.Write("[종목조회] ", Color.GreenYellow);
            rbConsole.Write("총 ");
            rbConsole.Write(data.Count.ToString(), Color.Red);
            rbConsole.Write(" 건 ");
            rbConsole.WriteLine(" 완료", Color.Violet);
            stocks = data;
            getStockFinance(stocks[index]);
        }

        void getStockFinance(Stock stock)
        {
            rbConsole.Write("[재무-" + stock.hname + "] ", Color.GreenYellow);
            t3320_FinanceTR tr = new t3320_FinanceTR(); 
            tr.OnReceiveComplete += OnReceiveStockFinance;
            Hashtable ht = new Hashtable();
            ht.Add("gicode", stock.shcode);
            tr.request(ht);
            rbConsole.Write("조회 중...");
        }

        void OnReceiveStockFinance(StockFinance data)
        {
            rbConsole.WriteLine(" 완료", Color.Violet);
            if (stocks.Count-1 > index)
            {
                Thread.Sleep(1000);
                Stock stock = stocks[++index];
                stock.finance = data;
                getStockFinance(stock);
            }
            else
            {
                rbConsole.Write("전부다 완료", Color.Red);
            }
        }
    }
}
