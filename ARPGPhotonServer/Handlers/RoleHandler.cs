using System;
using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using ARPGPhotonServer.DB.Manager;
using LitJson;
using Photon.SocketServer;

namespace ARPGPhotonServer.Handlers
{
    internal class RoleHandler : HandlerBase
    {
        private readonly RoleManager _manager;

        public RoleHandler()
        {
            _manager = new RoleManager();
        }

        public override OperationResponse OnHandlerMeesage(OperationRequest request, ClientPeer peer)
        {
            var subCode = ParameterTool.GetParameter<SubCode>(request.Parameters, ParameterCode.SubCode, false);
            peer.WriteLog("Handling Request OperationCode:Role SubCode:" + subCode);
            var parameters = new Dictionary<byte, object>
            {
                {(byte) ParameterCode.SubCode, subCode}
            };
            var response = new OperationResponse
            {
                OperationCode = request.OperationCode,
                ReturnCode = (short) ReturnCode.Success
            };
            peer.WriteLog(subCode.ToString());
            switch (subCode)
            {
                case SubCode.GetRole:
                    //1.返回当前用户的角色列表
                    var roles = _manager.GetRoles(peer.User);
                    foreach (var role1 in roles)
                    {
                        role1.User = null;
                    }
                    var json = JsonMapper.ToJson(roles);
                    parameters.Add((byte) ParameterCode.RoleList, json);
                    break;
                case SubCode.AddRole:
                    //2.为当前用户添加一个角色
                    var role = ParameterTool.GetParameter<Role>(request.Parameters, ParameterCode.Role);
                    if (_manager.ContainRole(role.Name))
                    {
                        response.ReturnCode = (short) ReturnCode.Fail;
                        response.DebugMessage = "昵称已被占用";
                    }
                    else
                    {
                        role.User = peer.User;
                        response.ReturnCode = (short) (_manager.AddRole(role) ? ReturnCode.Success : ReturnCode.Fail);
                        role.User = null;
                        json = JsonMapper.ToJson(role);
                        parameters.Add((byte) ParameterCode.Role, json);
                    }
                    break;
                case SubCode.SelectRole:
                    //3.设定登录的角色
                    role = ParameterTool.GetParameter<Role>(request.Parameters, ParameterCode.Role);
                    peer.SetLoginRole(role);
                    ParameterTool.AddParameter(parameters, ParameterCode.Role, role);
                    role.User = peer.User;
                    break;
                case SubCode.UpdateRole:
                    //更新当前角色信息
                    role = ParameterTool.GetParameter<Role>(request.Parameters, ParameterCode.Role);
                    role.User = peer.User;
                    _manager.UpdateRole(role);
                    role.User = null;
                    ParameterTool.AddParameter(parameters, ParameterCode.Role, role);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            response.Parameters = parameters;
            return response;
        }

        protected override OperationCode OpCode
        {
            get { return OperationCode.Role; }
        }
    }
}