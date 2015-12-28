using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARPGCommon;
using ARPGCommon.Model;
using ARPGPhotonServer.DB.Manager;
using Photon.SocketServer;

namespace ARPGPhotonServer.Handlers
{
    internal class TaskDbHandler : HandlerBase
    {
        private readonly TaskDbManager _manager;

        public TaskDbHandler()
        {
            _manager = new TaskDbManager();
        }

        public override OperationResponse OnHandlerMeesage(OperationRequest request, ClientPeer peer)
        {
            var subCode = ParameterTool.GetParameter<SubCode>(request.Parameters, ParameterCode.SubCode, false);
            var parameters = new Dictionary<byte, object> {{(byte) ParameterCode.SubCode, subCode}};
            var response = new OperationResponse(request.OperationCode, parameters);
            switch (subCode)
            {
                case SubCode.AddTask:
                    var taskDb = ParameterTool.GetParameter<TaskDb>(request.Parameters, ParameterCode.TaskDb);
                    peer.LoginRole.User = peer.User;
                    taskDb.Role = peer.LoginRole;
                    _manager.AddTaskDb(taskDb);
                    //防止在转化json时出现bug
                    taskDb.Role = null;
                    ParameterTool.AddParameter(parameters, ParameterCode.TaskDb, taskDb);
                    break;
                case SubCode.GetTask:
                    peer.LoginRole.User = peer.User;
                    var taskDbs = _manager.GeTaskDbs(peer.LoginRole);
                    //防止在转化json时出现bug
                    foreach (var db in taskDbs)
                    {
                        db.Role = null;
                    }
                    ParameterTool.AddParameter(parameters, ParameterCode.TaskDb, taskDbs);
                    break;
                case SubCode.UpdateTask:
                    taskDb = ParameterTool.GetParameter<TaskDb>(request.Parameters, ParameterCode.TaskDb);
                    taskDb.Role = peer.LoginRole;
                    OperateTaskState(ref taskDb);
                    _manager.UpdateTaskDb(taskDb);
                    //防止在转化json时出现bug
                    taskDb.Role = null;
                    ParameterTool.AddParameter(parameters, ParameterCode.TaskDb, taskDb);
                    break;
            }
            return response;
        }

        protected override OperationCode OpCode
        {
            get { return OperationCode.Task; }
        }

        protected void OperateTaskState(ref TaskDb taskDb)
        {
            switch (taskDb.TaskState)
            {
                case (byte) TaskState.UnStarted:
                    taskDb.TaskState = (byte) TaskState.Accepted;
                    break;
                case (byte) TaskState.Accepted:
                    break;
                case (byte) TaskState.Achieved:
                    break;
                case (byte) TaskState.Accomplished:
                    break;
            }
        }
    }
}