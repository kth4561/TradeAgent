using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradeAgent
{
    public partial class LoginForm : Form
    {
        private SessionCtrl m_session;
        public LoginForm()
        {
            
            InitializeComponent();
            m_session = new SessionCtrl();

            //session.connect("demo.etrade.co.kr");
            m_session.connect(SessionCtrl.REAR_SERVER_URL);
            m_session.login("sculove", "mhyj0425", "");
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Console.WriteLine("로그아웃 : " + m_session.logout());

            m_session.disconnect();
            Console.WriteLine("접속종료");
        }
    }
}
