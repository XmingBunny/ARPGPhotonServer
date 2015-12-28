using ARPGCommon;
using ARPGCommon.Model;
using ARPGPhotonServer.Handlers;
using ExitGames.Logging;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace ARPGPhotonServer
{
    internal class ClientPeer : PeerBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     创建时通过
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="peer"></param>
        public ClientPeer(IRpcProtocol protocol, IPhotonPeer peer)
            : base(protocol, peer)
        {
            WriteLog("connected.");
        }

        public User User { get; private set; }
        public Role LoginRole { get; private set; }

        public void SetUser(User user)
        {
            User = user;
        }

        public void SetLoginRole(Role role)
        {
            LoginRole = role;
        }

        protected override void OnDisconnect(DisconnectReason reasonCode,
            string reasonDetail)
        {
            WriteLog("disconnected.");
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            WriteLog("Receive RequestCode:" + (OperationCode) operationRequest.OperationCode);
            HandlerBase handler;
            ArpgApplication.Instance.Handlers.TryGetValue(operationRequest.OperationCode, out handler);

            if (handler != null)
            {
                //1.处理
                var response = handler.OnHandlerMeesage(operationRequest, this);

                //2.发送响应
                SendOperationResponse(response, sendParameters);

                //3.日志记录
                object subCode = null;
                WriteLog(string.Format("Rsponse operationCode:{0} success",
                    (OperationCode) operationRequest.OperationCode));
            }
            else
            {
                //1.异常,日志记录
                WriteLog(string.Format("Can not find OperationCode:{0}",
                    (OperationCode) operationRequest.OperationCode));
            }
        }

        public void WriteLog(string message)
        {
            Log.Debug(ConnectionId + ":" + message);
        }
    }
}