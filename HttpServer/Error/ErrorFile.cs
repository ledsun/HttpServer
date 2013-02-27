using HttpServer.Resources;

namespace HttpServer.Util
{
    class ErrorFile
    {
        /// <summary>
        /// HTML拡張子
        /// </summary>
        private const string HTML_EXTENSION = ".html";

        /// <summary>
        /// エラーファイルパスのフォーマット
        /// </summary>
        private const string FORMAT_ERROR_FILE_PATH = "{0}{1}{2}";


        /// <summary>
        /// エラーファイルの配置先ディレクトリ
        /// </summary>
        private string ErrorFilesPath
        {
            get { return Config.DocumentRootPath + "/ErrorFiles"; }
        }


        /// <summary>
        /// エラーコードに対応するエラーファイルのパスを取得
        /// </summary>
        /// <param name="eCode"></param>
        /// <returns></returns>
        public string GetErrorFilePath(string eCode)
        {
            return string.Format(FORMAT_ERROR_FILE_PATH, this.ErrorFilesPath, GetValue(eCode), HTML_EXTENSION);
        }


        /// <summary>
        /// エラーコードに対応するエラーファイル名(拡張子なし)を取得
        /// </summary>
        /// <param name="eCode"></param>
        /// <returns></returns>
        private string GetValue(string eCode)
        {
            return ErrorFilePaths.ResourceManager.GetString(eCode);
        }

    }
}
