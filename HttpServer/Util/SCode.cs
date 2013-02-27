using HttpServer.Resources;

namespace HttpServer.Util
{
    /// <summary>
    /// HTTP の StatusCode に関するクラス
    /// </summary>
    class SCode
    {
        /// <summary>
        /// HttpStatusCode_{0}
        /// 
        ///  - {0}：HttpStatusCode
        /// </summary>
        private const string HTTP_STATUS_CODE_PREFIX = "HttpStatusCode_{0}";

        /// <summary>
        /// HttpStatusCode に対応するリソースの値を取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDescription(int key)
        {
            return ErrorStatusDescription.ResourceManager.GetString(string.Format(HTTP_STATUS_CODE_PREFIX, key));
        }
    }
}
