using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARPGCommon;
using ARPGPhotonServer.DB.Manager;
using LitJson;
using Photon.SocketServer;

namespace ARPGPhotonServer.Handlers
{
    internal class ServerPropertyHanlder : HandlerBase
    {
        private ServerPropertyManager _manager;

        public ServerPropertyHanlder()
        {
            this._manager = new ServerPropertyManager();
        }

        public override OperationResponse OnHandlerMeesage(OperationRequest request)
        {
            var list = _manager.GetserverpropertyList();
            var json = JsonMapper.ToJson(list);
            var parameters = new Dictionary<byte, object> {{(byte) ParameterCode.ServerList, json}};

            var response = new OperationResponse(request.OperationCode, parameters)
            {
                ReturnCode = (short) ReturnCode.Success
            };
            return response;
        }
    }
}