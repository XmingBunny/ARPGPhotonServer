using Photon.SocketServer;

namespace ARPGPhotonServer.Handlers
{
    abstract class HandlerBase
    {
        public abstract OperationResponse OnHandlerMeesage(OperationRequest request);
    }
}
