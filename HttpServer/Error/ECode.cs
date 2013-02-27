using HttpServer.Resources;

namespace HttpServer.Util
{
    /// <summary>
    /// エラーコード定義クラス
    /// </summary>
    class ECode
    {
        /// <summary>
        /// EL001 : 別プロセス使用時の例外
        /// </summary>
        public const string USE_OTHER_PROCESS = "EL001";

        /// <summary>
        /// EM001 : アウトオブメモリーの例外
        /// </summary>
        public const string OUT_OF_MEMORY = "EM001";

        /// <summary>
        /// EP401 : 401ページ
        /// </summary>
        public const string UNAUTHORIZED_PAGE = "EP401";

        /// <summary>
        /// EP403 : 403ページ
        /// </summary>
        public const string FORBIDDEN_PAGE = "EP403";

        /// <summary>
        /// EP404 : 404ページ
        /// </summary>
        public const string NOT_FOUND_PAGE = "EP404";

        /// <summary>
        /// EP500 : 500ページ
        /// </summary>
        public const string INTERNAL_SERVER_ERROR = "EP500";

        /// <summary>
        /// EF001 : ディレクトリが存在しない例外
        /// </summary>
        public const string NOT_FOUND_REQUIRED_DIRECTORY = "EF001";

        /// <summary>
        /// EF002 : ファイルが存在しない例外
        /// </summary>
        public const string NOT_FOUND_REQUIRED_FILE = "EF002";


        /// <summary>
        /// eCode に対応するリソースの値を返す
        /// </summary>
        /// <param name="eCode"></param>
        /// <returns></returns>
        public static string GetMessage(string eCode)
        {
            return ErrorMessages.ResourceManager.GetString(eCode);
        }

    }
}
