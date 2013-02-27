using System;
using System.Net;

namespace HttpServer.Util
{
    /// <summary>
    /// HttpServerに関する例外クラス
    /// </summary>
    abstract class HttpServerException : Exception
    {
        /// <summary>
        /// エラーコード
        /// </summary>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// HTTP のステータスコード
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// HTTP のステータスコードの説明
        /// </summary>
        public string StatusDescription { get; private set; }


        /// <summary>
        /// コンストラクタ（システムにおける例外のみの場合）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected HttpServerException(string eCode, params object[] args) 
        {
            this.ErrorCode = eCode;
            this.ErrorMessage = string.Format(ECode.GetMessage(eCode), args);

            this.StatusCode = (int)HttpStatusCode.OK;
            this.StatusDescription = SCode.GetDescription((int)HttpStatusCode.OK);
        }


        /// <summary>
        /// コンストラクタ（HTTPで定義されたステータスコードに該当する例外の場合）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected HttpServerException(string eCode, int sCode, params object[] args)
            : this(eCode, args)
        {
            this.StatusCode = sCode;
            this.StatusDescription = SCode.GetDescription(sCode);
        }

    }


    /// <summary>
    /// プロセスに関する例外クラス
    /// </summary>
    class ListenerException : HttpServerException
    {
        /// <summary>
        /// 他のプロセスが使用中であることを表わす例外を生成
        /// </summary>
        /// <returns></returns>
        public static ListenerException CreateHttpListenerException(string msg)
        {
            return new ListenerException(ECode.USE_OTHER_PROCESS, msg);
        }

        public ListenerException(string eCode, params object[] args)
            : base(eCode, args) 
        { }
    }


    /// <summary>
    /// メモリに関する例外クラス
    /// </summary>
    class MemoryException : HttpServerException
    {
        /// <summary>
        /// 使用可能なメモリが不足していることを表す例外を生成
        /// </summary>
        /// <returns></returns>
        public static MemoryException CreateOutOfMemoryException()
        {
            return new MemoryException(ECode.OUT_OF_MEMORY);
        }

        public MemoryException(string eCode, params object[] args)
            : base(eCode, args) 
        { }
    }


    /// <summary>
    /// アクセスに関する例外クラス
    /// </summary>
    class AccessException : HttpServerException
    {
        /// <summary>
        /// 権限がないことなどを表す例外を生成
        /// </summary>
        /// <returns></returns>
        public static AccessException CreateUnauthorizedAccessException()
        {
            return new AccessException(ECode.UNAUTHORIZED_PAGE, (int)HttpStatusCode.Unauthorized);
        }

        public AccessException(string eCode, int sCode, params object[] args)
            : base(eCode, sCode, args)
        { }
    }


    /// <summary>
    /// 存在の有無に関する例外クラス
    /// </summary>
    class NotFoundException : HttpServerException
    {
        /// <summary>
        /// ページが見つからないことを表す例外を生成
        /// </summary>
        /// <returns></returns>
        public static NotFoundException CreatePageNotFound()
        {
            throw new NotFoundException(ECode.NOT_FOUND_PAGE, (int)HttpStatusCode.NotFound);
        }

        /// <summary>
        /// 必須ディレクトリが存在しないことを表す例外を生成
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static NotFoundException CreateRequiredDirectoryException(string dir)
        {
            throw new NotFoundException(ECode.NOT_FOUND_REQUIRED_DIRECTORY, dir);
        }

        /// <summary>
        /// 必須ファイルが存在しないことを表す例外を生成
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static NotFoundException CreateRequiredFileException(string file)
        {
            throw new NotFoundException(ECode.NOT_FOUND_REQUIRED_FILE, file);
        }


        public NotFoundException(string eCode, int sCode, params object[] args)
            : base(eCode, sCode, args)
        { }

        public NotFoundException(string eCode, params object[] args)
            : base(eCode, args)
        { }
    }


    /// <summary>
    /// 不明なエラーに関するクラス
    /// </summary>
    class InternalServerException : HttpServerException
    {
        public InternalServerException(string msg)
            : base(ECode.INTERNAL_SERVER_ERROR, (int)HttpStatusCode.InternalServerError, msg)
        { }
    }

}
