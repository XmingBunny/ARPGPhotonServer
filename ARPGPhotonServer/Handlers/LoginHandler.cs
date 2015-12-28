using ARPGCommon;
using ARPGCommon.Model;
using ARPGPhotonServer.DB.Manager;
using ARPGPhotonServer.Tools;
using LitJson;
using Photon.SocketServer;

namespace ARPGPhotonServer.Handlers
{
    internal class LoginHandler : HandlerBase
    {
        private readonly UserManager _manager;

        public LoginHandler()
        {
            _manager = new UserManager();
        }

        public override OperationResponse OnHandlerMeesage(OperationRequest request, ClientPeer peer)
        {
            //1.获得客户端发送的帐号和明文密码
            object json;
            request.Parameters.TryGetValue((byte) ParameterCode.UserCheckInfo, out json);
            var user = JsonMapper.ToObject<User>(json.ToString());
            var userDb = _manager.GetUserByUsername(user.Username);
            var s = userDb != null
                ? string.Format("user.Username:{0},user.Password:{1} userDb.Username:{2},userDb.Password:{3}",
                    user.Username, user.Password, userDb.Username, userDb.Password)
                : "未找到用户:" + user.Username;
            peer.WriteLog(s);

            //2.比较,然后创建响应
            var response = new OperationResponse {OperationCode = request.OperationCode};
            if (userDb != null && userDb.Password == MD5Tool.GetMD5(user.Password))
            {
                response.ReturnCode = (short) ReturnCode.Success;
                peer.SetUser(userDb);
            }
            else
            {
                response.ReturnCode = (short) ReturnCode.Fail;
                response.DebugMessage = "用户名或密码错误!";
            }

            return response;
        }

        protected override OperationCode OpCode
        {
            get { return OperationCode.Login; }
        }
    }
}