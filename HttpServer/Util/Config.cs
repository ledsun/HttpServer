using System.Configuration;

namespace HttpServer.Util
{
    /// <summary>
    /// 設定ファイル情報クラス
    /// </summary>
    class Config
    {
        private const string DOCUMENT_ROOT_PATH = "DOCUMENT_ROOT_PATH";
        private const string PREFIX = "PREFIX";
        private const string PORT = "PORT";
        private const string LOG_CONFIG_FILE_PATH = "LOG_CONFIG_FILE_PATH";

        private const string LOG_FILES_DIR = "LOG_FILES_DIR";
        private const string ERROR_FILES_DIR = "ERROR_FILES_DIR";


        /// <summary>
        /// ドキュメントルートのパス
        /// </summary>
        public static string DocumentRootPath { get { return GetValue(DOCUMENT_ROOT_PATH); } }

        /// <summary>
        /// 受け付けるURL
        /// </summary>
        public static string Prefix { get { return GetValue(PREFIX); } }

        /// <summary>
        /// ポート
        /// </summary>
        public static string Port { get { return GetValue(PORT); } }

        /// <summary>
        /// log4net の設定ファイルパス
        /// </summary>
        public static string LogConfigFilePath { get { return GetValue(LOG_CONFIG_FILE_PATH); } }

        // OBSORETED
        //private const string THREAD_TIMEOUT = "THREAD_TIMEOUT";
        ///// <summary>
        ///// スレッドのタイムアウト時間
        ///// </summary>
        //public static int ThreadTimeout { get { return int.Parse(GetValue(THREAD_TIMEOUT)); } }

        /// <summary>
        /// ログファイル格納先ディレクトリ
        /// </summary>
        public static string LogFilesDir { get { return GetValue(LOG_FILES_DIR); } }

        /// <summary>
        /// エラーファイル格納先ディレクトリ
        /// </summary>
        public static string ErrorFilesDir { get { return GetValue(ERROR_FILES_DIR); } }


        /// <summary>
        /// App.Config から Key に対応する値を取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetValue(string key)
        {
            string val = ConfigurationSettings.AppSettings[key];
            AssertException.IsNotNull(val);
            return val;
        }

    }
}
