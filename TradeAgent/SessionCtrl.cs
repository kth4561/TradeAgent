using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using XA_SESSIONLib;

namespace TradeAgent
{
    class SessionCtrl : _IXASessionEvents
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IXASession m_Session;
        protected UCOMIConnectionPoint m_icp;
        protected UCOMIConnectionPointContainer m_icpc;

        public SessionCtrl()
        {
            m_Session = getSession();
        }

        ~SessionCtrl()
        {
            m_Session.Logout();
            m_Session.DisconnectServer();
        }

        /// <summary>
        /// 세션 객체를 만들고 반환한다.
        /// </summary>
        /// <returns>생성된 세션 객체</returns>
        private XASession getSession(){
            int m_dwCookie=0;

            XASession session = new XASession();
            m_icpc = (UCOMIConnectionPointContainer)session;
            Guid IID_SessionEvents = typeof(_IXASessionEvents).GUID;
            m_icpc.FindConnectionPoint(ref IID_SessionEvents, out m_icp);
            m_icp.Advise(this, out m_dwCookie);

            log.Debug("세션 생성시 Advise의 호출 결과 dwCookie는 " + m_dwCookie);
            return session;
        }

        /// <summary>
        /// m_Session에서 에러가 날 수도 있다.
        /// 에러 메시지를 가공해주는 기능을 담당.
        /// </summary>
        /// <returns></returns>
        public string getLastErrorMessage(int number = 0)
        {
            string errmsg = "";
            switch (m_Session.GetLastError())
            {
                case -1: errmsg = "XINGAPI_ERROR_SOCKET_CREATE_FAIL (-1) 소켓생성 실패"; break;
                case -2: errmsg = "XINGAPI_ERROR_CONNECT_FAIL (-2) 서버연결 실패"; break;
                case -3: errmsg = "XINGAPI_ERROR_WRONG_ADDRESS (-3) 서버주소가 잘못되었음"; break;
                case -4: errmsg = "XINGAPI_ERROR_CONNECT_TIMEOUT (-4) 연결시간 초과"; break;
                case -5: errmsg = "XINGAPI_ERROR_ALREADY_CONNECT (-5) 이미 서버에 연결중이거나 연결시도중"; break;
                default: errmsg = "알 수 없는 에러. (" + m_Session.GetErrorMessage(number) + ")"; break;
            }
            return errmsg;
        }

        #region _IXASessionEvents
        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Login(string szCode, string szMsg)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
