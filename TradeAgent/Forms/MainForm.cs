using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using TradeAgent;
using TradeAgent.Model;
using TradeAgent.Transactions.TR;

namespace TradeAgent.Forms
{
    public partial class MainForm : Form
    {
        SessionCtrl session;        // 섹션
        List<Stock> stocks;         // 종목 정보

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
            rbConsole.WriteLine(" 건 조회완료");


            rbConsole.WriteLine(data[0].hname + "----" + data[0].shcode);
            //rbConsole.Write("[재무조회] ", Color.GreenYellow);
            //t3320_FinanceTR tr = new t3320_FinanceTR();
            //tr.OnReceiveComplete += OnReceiveStockFinance;
            //Hashtable ht = new Hashtable();
            //ht.Add("gubun", 0);
            //tr.request(ht);
            //rbConsole.WriteLine("조회 중...");

        }

        void OnReceiveStockFinance(List<Stock> data)
        {
            //rbConsole.Write("[재무조회] ", Color.GreenYellow);
            //rbConsole.Write("총 ");
            //rbConsole.Write(data.Count.ToString(), Color.Red);
            //rbConsole.WriteLine(" 건 조회완료");

        }

    }
}
