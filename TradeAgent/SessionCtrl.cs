using System;
using System.Runtime.InteropServices.ComTypes;
using System.ComponentModel;
using System.Windows.Forms;
using XA_SESSIONLib;
using System.Threading;

using TradeAgent.Transactions.TR;
using XA_DATASETLib;

namespace TradeAgent
{
    class SessionCtrl : _IXASessionEvents
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IXASession m_Session;
        public const string REAR_SERVER_URL = "hts.etrade.co.kr";
        public const string SIMUL_SERVER_URL = "demo.etrade.co.kr";

        #region event 전달을 위한 delegate
        public delegate void LoginHandler(string szCode, string szMsg);
        public event LoginHandler OnLogin;
        public delegate void LogoutHandler();
        public event LogoutHandler OnLogout;
        public delegate void OnDisconnectHandler();
        public event OnDisconnectHandler OnDisconnect;
        #endregion

        public SessionCtrl()
        {
            m_Session = getSession();
        }

        ~SessionCtrl()
        {
            disconnect();
        }

        public void disconnect()
        {
            m_Session.Logout();
            if (m_Session.IsConnected())
            {
                m_Session.DisconnectServer();
            }
        }

        /// <summary>
        /// 세션 객체를 만들고 반환한다.
        /// </summary>
        /// <returns>생성된 세션 객체</returns>
        private XASession getSession(){
            int dwCookie=0;
            IConnectionPoint icp;
            IConnectionPointContainer icpc;

            XASession session = new XASession();
            icpc = (IConnectionPointContainer)session;
            Guid IID_SessionEvents = typeof(_IXASessionEvents).GUID;
            icpc.FindConnectionPoint(ref IID_SessionEvents, out icp);
            icp.Advise(this, out dwCookie);

            Console.WriteLine("세션 생성시 Advise의 호출 결과 dwCookie는 " + dwCookie);
            return session;
        }

        /// <summary>
        /// Disconnect 이벤트를 수신했을 때 다시 로그인을 시도한다. 10분마다 그런다. (언제 다시 열리는지 몰랑!)
        /// </summary>
        public void connect(string serverURL)
        {
            Console.WriteLine("서버 접속 시작 : " + serverURL);
            // 세션 얻기 시도는 최대 10분간 1초에 한 번씩 요청한다. 
            DateTime time = DateTime.Now;

            for (int i = 0; i < 600; i++)
            {
                m_Session.ConnectServer(serverURL, 20001);
                if (!m_Session.IsConnected())
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("서버 접속 실패.....중..." + m_Session.GetErrorMessage(m_Session.GetLastError()));     // 연결 실패시 메시지 남기기
                }
                else
                {
                    break;
                }
            }

            TimeSpan diff = DateTime.Now - time;
            Console.WriteLine("서버에 접속하기까지 소요된 시간은 [" + diff.TotalSeconds + "]초 입니다.");
        
        }
        public bool logout()
        {
            return m_Session.Logout();
        }

        public bool login(string id, string pw, string certPw)
        {
            bool result = false;
            if (m_Session.IsConnected())
            {
                // 서버에만 연결되었으니 로그인 시도. 
                result = m_Session.Login(id, pw, certPw, (int)(SessionCtrl.REAR_SERVER_URL.Equals(id) ? XA_SESSIONLib.XA_SERVER_TYPE.XA_REAL_SERVER : XA_SESSIONLib.XA_SERVER_TYPE.XA_SIMUL_SERVER), true);
                
                if (!result)
                {
                    OnLogin.Invoke("FAIL", "로그인에 실패했어요! 계정 정보를 다시 한 번 확인해주세요.");
                }
            }
            return result;
        }

        /// <summary>
        /// m_Session에서 에러가 날 수도 있다.
        /// 에러 메시지를 가공해주는 기능을 담당.
        /// </summary>
        /// <returns></returns>
        //private string getLastErrorMessage(int number = 0)
        //{
        //    string errmsg = "";
        //    switch (m_Session.GetLastError())
        //    {
        //        case -1: errmsg = "XINGAPI_ERROR_SOCKET_CREATE_FAIL (-1) 소켓생성 실패"; break;
        //        case -2: errmsg = "XINGAPI_ERROR_CONNECT_FAIL (-2) 서버연결 실패"; break;
        //        case -3: errmsg = "XINGAPI_ERROR_WRONG_ADDRESS (-3) 서버주소가 잘못되었음"; break;
        //        case -4: errmsg = "XINGAPI_ERROR_CONNECT_TIMEOUT (-4) 연결시간 초과"; break;
        //        case -5: errmsg = "XINGAPI_ERROR_ALREADY_CONNECT (-5) 이미 서버에 연결중이거나 연결시도중"; break;
        //        default: errmsg = "알 수 없는 에러. (" + m_Session.GetErrorMessage(number) + ")"; break;
        //    }
        //    return errmsg;
        //}

        #region _IXASessionEvents
        /// <summary>
        /// 서버에서 유지보수 등의 이유로 강제로 연결을 종료시킬 때 발생하는 이벤트
        /// 서버에 재접속을 시도한다.
        /// </summary>
        void _IXASessionEvents.Disconnect()
        {
            //Console.WriteLine("onDisconnect 서버에서 통신을 끊어버림! 세션을 새로 만듭니다.");
            
            //// 우선 세션을 새로 만든다
            //m_Session.DisconnectServer();
            //m_Session = getSession();
            //Console.WriteLine("세션을 새로 만들었습니다. 서버 접속 및 로그인을 시도합니다.");
            ////@todo 다시 접속하게 만든다.
            OnDisconnect.Invoke();
        }

        void _IXASessionEvents.Login(string szCode, string szMsg)
        {
            OnLogin.Invoke(szCode, szMsg);
        }
        // deprecated
        void _IXASessionEvents.Logout()
        {
            OnLogout.Invoke();
        }
        #endregion
    }
}
