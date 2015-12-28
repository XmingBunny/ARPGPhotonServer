using ARPGCommon;
using ARPGCommon.Model;
using ARPGPhotonServer.DB.Manager;
using LitJson;
using Photon.SocketServer;

namespace ARPGPhotonServer.Handlers
{
    internal class RegisterHandler : HandlerBase
    {
        private readonly UserManager _manager;

        public RegisterHandler()
        {
            _manager = new UserManager();
        }

        public override OperationResponse OnHandlerMeesage(OperationRequest request, ClientPeer peer)
        {
            object json;
            request.Parameters.TryGetValue((byte) ParameterCode.UserCheckInfo, out json);
            var user = JsonMapper.ToObject<User>(json.ToString());
            var response = new OperationResponse {OperationCode = request.OperationCode};

            //查询数据库
            var userDb = _manager.GetUserByUsername(user.Username);
            if (userDb == null)
            {
                if (_manager.RegisterUser(user))
                {
                    response.ReturnCode = (short) ReturnCode.Success;
                    peer.SetUser(user);
                }
                else
                {
                    response.ReturnCode = (short) ReturnCode.Eception;
                    response.DebugMessage = "异常";
                }
            }
            else
            {
                response.ReturnCode = (short) ReturnCode.Fail;
                response.DebugMessage = "用户名已存在!";
            }
            return response;
        }

        protected override OperationCode OpCode
        {
            get { return OperationCode.Register; }
        }
    }
}