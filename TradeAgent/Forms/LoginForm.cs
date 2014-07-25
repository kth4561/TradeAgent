using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XA_DATASETLib;
using TradeAgent.Transactions.TR;

namespace TradeAgent
{
    public partial class LoginForm : Form
    {
        private SessionCtrl m_session = null;
        public LoginForm()
        {
            InitializeComponent();
        }

        #region sessionCtrl evnt handler 
        private void OnLogin(string szCode, string szMsg)
        {
           if ("0000".Equals(szCode))
           {
               TransactionCtrl trCtrl = new TransactionCtrl(new t8430_StockListTR());
               
               // A. BLOCK Occurs
               //IXAQuery query = new t8430_StockListTR().setResource("t8430");
               //query.SetFieldData("t8430InBlock", "gubun", 0, "0");
               //int result = query.Request(false);
               //if (result < 0)
               //{
               //    Console.WriteLine("t8430 request fail : " + query.GetLastError());
               //}
               //this.Close();
           }
           else
           {
               MessageBox.Show(szMsg);
           }
        }

        private void OnLogout()
        {
             Console.WriteLine("로그 아웃");
        }
        #endregion

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (!this.tbId.Text.Trim().Equals("") && !this.tbPw.Text.Trim().Equals(""))
            {
                m_session = new SessionCtrl();
                m_session.OnLogin += OnLogin;
                m_session.OnLogout += OnLogout;

                m_session.connect(cbServer.SelectedValue.ToString());
                m_session.login(this.tbId.Text, this.tbPw.Text, "");
            }
            else
            {
                MessageBox.Show("아이디와 패스워드를 입력해주세요");
            }
            
        }

        private void btExit_Click(object sender, EventArgs e)
        {

            if (m_session != null)
            {
                m_session.logout();
                m_session.disconnect();
            }
            Console.WriteLine("접속종료");
            this.Close();
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("key"));
            dt.Columns.Add(new DataColumn("value"));
            dt.Rows.Add("모의서버", SessionCtrl.SIMUL_SERVER_URL);
            dt.Rows.Add("실서버", SessionCtrl.REAR_SERVER_URL);
            cbServer.DataSource = dt;
            cbServer.DisplayMember = "key";
            cbServer.ValueMember = "value";
        }
    }
}
