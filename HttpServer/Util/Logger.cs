using System;
using System.IO;
using log4net;
using log4net.Config;

namespace HttpServer.Util
{
    public class Logger
    {
        private const string LOGGER_NAME = "ILog";

        private ILog _iLog;
        private static Logger __instance = new Logger();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Logger()
        {
            XmlConfigurator.Configure(new FileInfo(Config.LogConfigFilePath));
            _iLog = LogManager.GetLogger(LOGGER_NAME);
        }


        /// <summary>
        /// Loggerを取得
        /// </summary>
        private static ILog ILog
        {
            get { return __instance._iLog; }
        }


        /// <summary>
        /// ログを出力：通常ログ
        /// </summary>
        /// <param name="log"></param>
        internal static void PutInfo(Log log, string msg)
        {
            log.Message = msg;
            log.ErrorCode = "";
            ILog.Info(log.ToString());

            Console.WriteLine(log.Message);
        }


        /// <summary>
        /// ログを出力：エラーログ
        /// </summary>
        /// <param name="errorLog"></param>
        internal static void PutError(Log log)
        {
            ILog.Error(log.ToString());
            Console.WriteLine(log.Message);
        }


        /// <summary>
        /// ログを出力：警告ログ
        /// </summary>
        /// <param name="errorLog"></param>
        internal static void PutWarning(Log log)
        {
            ILog.Warn(log.ToString());
            Console.WriteLine(log.Message);
        }
    }
}
