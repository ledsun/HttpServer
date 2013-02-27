using System;
using System.IO;
using System.Net;
using System.Reflection;
using HttpServer.Resources;
using HttpServer.Util;

namespace HttpServer
{
    public class ThreadProcess
    {
        /// <summary>
        /// OutputStream.Writeの第２引数 offset のデフォルト値
        /// </summary>
        private const int DEFAULT_OFFSET_INDEX = 0;

        /// <summary>
        /// エラー発生回数の制限値
        /// </summary>
        private const int ERROR_COUNT_LIMIT = 1;


        /// <summary>
        /// リスナーから取得したコンテキスト
        /// </summary>
        internal HttpListenerContext Context { get; private set; }

        /// <summary>
        /// エラー保持
        /// </summary>
        internal Exception KeepError { get; private set; }

        /// <summary>
        /// エラー発生回数
        /// </summary>
        internal int ErrorCount { get; private set; }

        /// <summary>
        /// エラーを含んでいるか否か
        /// </summary>
        private bool IsIncludeError { get { return KeepError != null; } }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        public ThreadProcess(HttpListenerContext context)
        {
            AssertException.IsNotNull(context);

            this.Context = context;
            this.KeepError = null;
            this.ErrorCount = 0;
        }


        /// <summary>
        /// スレッドの処理内容
        /// </summary>
        public void DoRun()
        {
            ThreadProcessLog log = new ThreadProcessLog();
            log.MethodName = MethodBase.GetCurrentMethod().Name;

            Logger.PutInfo(log, LogMessage.ThreadProcessStart);

            HttpListenerRequest request = this.Context.Request;
            HttpListenerResponse response = this.Context.Response;

            try
            {
                SetLogOfRequest(ref log, request);

                Logger.PutInfo(log, string.Format(LogMessage.GetRequest, log.RequestUrl));

                ReturnResponse(ref log, GetResponseFilePath(request), response);
            }
            catch (UnauthorizedAccessException)
            {
                ReturnErrorFile(ref log, response, AccessException.CreateUnauthorizedAccessException());
            }
            catch (OutOfMemoryException)
            {
                ReturnErrorFile(ref log, response, MemoryException.CreateOutOfMemoryException());
            }
            catch (HttpListenerException ex)
            {
                //ReturnErrorFile(ref log, response, ListenerException.CreateNetWorkNotFound());

                // HttpListenerException が発生した場合、再度専用のエラーページを出力しようとしても
                // 同じエラー(ネットワークの接続が不正、IO受信拒否など)が発生し、無限ループとなるため
                // ログに出力し、例外を握りつぶす

                log.ErrorCode = ex.ErrorCode.ToString();
                log.Message = ex.Message + " : " + ex.StackTrace;
                Logger.PutError(log);
            }
            catch (Exception ex)
            {
                ReturnErrorFile(ref log, response, ex);
            }
            finally
            {
                response.Close();
                Logger.PutInfo(log, LogMessage.ResponseClose);
            }

            Logger.PutInfo(log, LogMessage.ThreadProcessEnd);
        }


        /// <summary>
        /// ログ情報にリクエストの値を設定
        /// </summary>
        /// <param name="log"></param>
        /// <param name="request"></param>
        private static void SetLogOfRequest(ref ThreadProcessLog log, HttpListenerRequest request)
        {
            log.RequestUrl          = request.Url.OriginalString;
            log.RequestAcceptType   = request.AcceptTypes;
            log.RequestContentType  = request.ContentType;
        }


        /// <summary>
        /// リクエストのパスを取得
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetResponseFilePath(HttpListenerRequest request)
        {
            string requestFilePath = Config.DocumentRootPath + request.RawUrl.Replace("/", "\\");

            if (File.Exists(requestFilePath))
            {
                return requestFilePath;
            }

            throw NotFoundException.CreatePageNotFound();
        }


        /// <summary>
        /// エラーが発生した場合、適切なページを表示(ストリームに出力)
        /// </summary>
        /// <param name="log"></param>
        /// <param name="response"></param>
        /// <param name="ex"></param>
        private void ReturnErrorFile(ref ThreadProcessLog log, HttpListenerResponse response, Exception ex)
        {
            // エラー発生数をカウント
            this.ErrorCount += ERROR_COUNT_LIMIT;

            HttpServerException httpEx = ex is HttpServerException ? 
                ex as HttpServerException : new InternalServerException(ex.Message + " :: " + ex.StackTrace);

            log.ErrorCode   = httpEx.ErrorCode;
            log.Message     = httpEx.ErrorMessage;

            response.StatusCode = httpEx.StatusCode;
            response.StatusDescription = httpEx.StatusDescription;

            // エラーを保持
            this.KeepError = httpEx;

            ReturnResponse(ref log, new ErrorFile().GetErrorFilePath(httpEx.ErrorCode), response);
        }


        /// <summary>
        /// レスポンスを返す
        /// 
        ///  - 読み込み
        ///  - 書き出し
        /// </summary>
        /// <param name="log"></param>
        /// <param name="path"></param>
        /// <param name="response"></param>
        private void ReturnResponse(ref ThreadProcessLog log, string path, HttpListenerResponse response)
        {
            log.MethodName = MethodBase.GetCurrentMethod().Name;

            SetLogOfResponse(ref log, path, response);

            // エラーがループ(制限回数以上エラー発生)している場合、エラーを出力し例外を握りつぶす
            if (this.ErrorCount > ERROR_COUNT_LIMIT)
            {
                Logger.PutError(log);
                return;
            }

            // エラー(1回目)が発生した場合、エラーを出力
            if (this.IsIncludeError)
            {
                Logger.PutError(log);
                log.Clear();
            }

            Logger.PutInfo(log, string.Format(LogMessage.SetResponse, path));

            Logger.PutInfo(log, LogMessage.ReadResponseDataStart);
            byte[] content = File.ReadAllBytes(path);
            Logger.PutInfo(log, LogMessage.ReadResponseDataEnd);

            Logger.PutInfo(log, LogMessage.WriteResponseDataStart);
            response.OutputStream.Write(content, DEFAULT_OFFSET_INDEX, content.Length);
            Logger.PutInfo(log, LogMessage.WriteResponseDataEnd);
        }


        /// <summary>
        /// ログ情報にレスポンスの値を設定
        /// </summary>
        /// <param name="log"></param>
        /// <param name="path"></param>
        /// <param name="response"></param>
        private static void SetLogOfResponse(ref ThreadProcessLog log, string path, HttpListenerResponse response)
        {
            log.ResponseUrl = path;
            log.ResponseStatusCode = response.StatusCode;
            log.ResponseStatusDescription = response.StatusDescription;
        }

    }
}
