using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using ARPGCommon;
using ARPGPhotonServer.Handlers;
using ExitGames.Logging;

namespace ARPGPhotonServer
{
    class ClientPeer:PeerBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 创建时通过
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="peer"></param>
        public ClientPeer(IRpcProtocol protocol, IPhotonPeer peer)
            : base(protocol, peer)
        {

        }

        protected override void OnDisconnect(PhotonHostRuntimeInterfaces.DisconnectReason reasonCode, string reasonDetail)
        {
            throw new NotImplementedException();
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HandlerBase handler;
            ArpgApplication.Instance.Handlers.TryGetValue(operationRequest.OperationCode, out handler);

            if (handler != null)
            {
                //1.处理
                OperationResponse response = handler.OnHandlerMeesage(operationRequest);

                //2.发送响应
                SendOperationResponse(response, sendParameters);
            }
            else
            {
                //2.异常,日志记录
                Log.Debug("Can not find OperationCode:" + operationRequest.OperationCode);
            }
        }
    }
}
