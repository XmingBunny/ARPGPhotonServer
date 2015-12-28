using ARPGCommon;
using Photon.SocketServer;

namespace ARPGPhotonServer.Handlers
{
    internal abstract class HandlerBase
    {
        protected HandlerBase()
        {
            ArpgApplication.Instance.Handlers.Add((byte) OpCode, this);
        }

        public abstract OperationResponse OnHandlerMeesage(OperationRequest request, ClientPeer peer);
        protected abstract OperationCode OpCode { get; }
    }
}