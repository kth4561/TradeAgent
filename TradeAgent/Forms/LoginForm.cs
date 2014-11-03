using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XA_DATASETLib;
using System.Collections;

namespace TradeAgent
{
    public partial class LoginForm : Form
    {
        SessionCtrl session;

        public SessionCtrl getSession() {
            return session;
        }

        public LoginForm()
        {
            InitializeComponent();
            session = new SessionCtrl();
            session.OnLogin += OnLogin;
            session.OnLogout += OnLogout;
            session.OnDisconnect += OnDisconnect;
        }

        #region sessionCtrl event handler 
        private void OnLogin(string szCode, string szMsg)
        {
           if ("0000".Equals(szCode))
           {
               this.DialogResult = DialogResult.OK;
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
            session.disconnect();
        }
        #endregion

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (!this.tbId.Text.Trim().Equals("") && !this.tbPw.Text.Trim().Equals(""))
            {
                session.connect(cbServer.SelectedValue.ToString());
                session.login(this.tbId.Text, this.tbPw.Text, "");
            }
            else
            {
                MessageBox.Show(TradeAgent.Properties.Resources.MSG_0001);
            }
            
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (session != null)
            {
                session.logout();
                session.disconnect();
            }
            Console.WriteLine("접속종료");
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("key"));
            dt.Columns.Add(new DataColumn("value"));
            dt.Rows.Add(TradeAgent.Properties.Resources.REAL_SERVER_NAME, SessionCtrl.REAR_SERVER_URL);
            dt.Rows.Add(TradeAgent.Properties.Resources.SIMUL_SERVER_NAME, SessionCtrl.SIMUL_SERVER_URL);
            cbServer.DataSource = dt;
            cbServer.DisplayMember = "key";
            cbServer.ValueMember = "value";
        }
    }
}
