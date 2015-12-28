using System;
using System.Collections.Generic;
using ExitGames.Logging.Log4Net;
using Photon.SocketServer;
using System.IO;
using System.Reflection;
using ARPGCommon;
using ARPGPhotonServer.Handlers;
using ExitGames.Logging;
using log4net;
using log4net.Config;
using LogManager = ExitGames.Logging.LogManager;


namespace ARPGPhotonServer
{
    internal class ArpgApplication : ApplicationBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public static ArpgApplication Instance { get; private set; }

        public readonly Dictionary<byte, HandlerBase> Handlers = new Dictionary<byte, HandlerBase>();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ClientPeer(initRequest.Protocol, initRequest.PhotonPeer);
        }

        /// <summary>
        /// 服务启动的时候调用
        /// </summary>
        protected override void Setup()
        {
            //1.从Photon官方示例中copy过来的代码用来输出日志
            LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            //应用的根目录中的log文件
            GlobalContext.Properties["LogFileName"] = "ARPG" + this.ApplicationName; //生成的日志的名字
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));

            //2.初始化了单例模式
            Instance = this;

            //3.输出启动成功日志信息
            Log.Debug("Application Setup Complete");

            //临时注册个登录处理器
            RegisterHandlers();
        }

        /// <summary>
        /// 把Handler交给Operation进行管理
        /// </summary>
        private void RegisterHandlers()
        {
            //Handlers.Add((byte) OperationCode.Login, new LoginHandler());
            //Handlers.Add((byte) OperationCode.GetServer, new ServerPropertyHandler());
            //Handlers.Add((byte) OperationCode.Register, new RegisterHandler());
            //Handlers.Add((byte) OperationCode.Role, new RoleHandler());
            var types = Assembly.GetAssembly(typeof (HandlerBase)).GetTypes();
            foreach (var type in types)
            {
                if (type.Name.EndsWith("Handler"))
                    Activator.CreateInstance(type);
            }
        }

        protected override void TearDown()
        {
            Log.Debug("Application TearDown");
            //throw new NotImplementedException();
        }
    }
}