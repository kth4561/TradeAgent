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
using System.Collections;

namespace TradeAgent
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            Global.session = new SessionCtrl();
            Global.session.OnLogin += OnLogin;
            Global.session.OnLogout += OnLogout;
            Global.session.OnDisconnect += OnDisconnect;
        }

        #region sessionCtrl event handler 
        private void OnLogin(string szCode, string szMsg)
        {
           if ("0000".Equals(szCode))
           {
               this.Close();
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

        /// <summary>
        /// 서버에서 강제로 연결을 종료시켰을때 발생
        /// </summary>
        private void OnDisconnect()
        {
            Global.session.disconnect();
        }
        #endregion

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (!this.tbId.Text.Trim().Equals("") && !this.tbPw.Text.Trim().Equals(""))
            {
                Global.session.connect(cbServer.SelectedValue.ToString());
                Global.session.login(this.tbId.Text, this.tbPw.Text, "");
            }
            else
            {
                MessageBox.Show("아이디와 패스워드를 입력해주세요");
            }
            
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (Global.session != null)
            {
                Global.session.logout();
                Global.session.disconnect();
            }
            Console.WriteLine("접속종료");
            this.Hide();
            this.Close();

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
