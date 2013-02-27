using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using HttpServer.Resources;
using HttpServer.Util;

namespace HttpServer
{
    class Program
    {
        /// <summary>
        /// メイン処理
        /// </summary>
        public static void Main()
        {
            try
            {
                ExistsConfirmRequiredThing();

                Run();
            }
            catch (HttpListenerException ex)
            {
                Notice(ListenerException.CreateHttpListenerException(ex.Message));
            }
            catch (Exception ex)
            {
                Notice(ex);
            }
        }


        /// <summary>
        /// 必要なモノの存在確認
        /// </summary>
        private static void ExistsConfirmRequiredThing()
        {
            List<string> requiredDirectory = new List<string>(new string[] { 
                Config.LogFilesDir,
                Config.ErrorFilesDir,
            });

            foreach (string dir in requiredDirectory)
            {
                if (!Directory.Exists(dir))
                {
                    throw NotFoundException.CreateRequiredDirectoryException(dir);
                }
            }

            List<string> requiredFiles = new List<string>(new string[] {
                Config.LogConfigFilePath,
                new ErrorFile().GetErrorFilePath(ECode.UNAUTHORIZED_PAGE),
                new ErrorFile().GetErrorFilePath(ECode.FORBIDDEN_PAGE),
                new ErrorFile().GetErrorFilePath(ECode.NOT_FOUND_PAGE),
                new ErrorFile().GetErrorFilePath(ECode.INTERNAL_SERVER_ERROR),
                new ErrorFile().GetErrorFilePath(ECode.OUT_OF_MEMORY),
            });

            foreach (string file in requiredFiles)
            {
                if (!File.Exists(file))
                {
                    throw NotFoundException.CreateRequiredFileException(file);
                }
            }
        }


        /// <summary>
        /// 処理実行
        /// 
        /// ※Thread終了の有無は気にせず、Thread中の処理で全て完結させる
        /// 
        ///  1. HttpListenerクラスをインスタンス化
        ///  2. 応答するプレフィックスの登録
        ///  3. Startメソッドを呼び出して処理を開始
        ///  4. GetContextメソッドを呼び出してリクエストを待つ
        ///  5. リクエストが来たら、そのURLに応じたファイルを出力
        /// </summary>
        private static void Run()
        {
            ThreadLog log = new ThreadLog();
            log.MethodName = MethodBase.GetCurrentMethod().Name;

            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(string.Format(Config.Prefix, Config.Port));
                listener.Start();

                Logger.PutInfo(log, LogMessage.HttpServerStart);

                while (true)
                {
                    Logger.PutInfo(log, LogMessage.AcceptRequest);

                    HttpListenerContext context = listener.GetContext();

                    ThreadProcess process = new ThreadProcess(context);
                    Thread thread = new Thread(process.DoRun);

                    thread.Start();
                }
            }
        }


        /// <summary>
        /// エラー通知
        /// </summary>
        /// <param name="ex"></param>
        private static void Notice(Exception ex)
        {
            Log log = new Log();
            log.MethodName = MethodBase.GetCurrentMethod().Name;

            if (ex is HttpServerException)
            {
                HttpServerException httpEx = ex as HttpServerException;
                log.Message     = httpEx.ErrorMessage;
                log.ErrorCode   = httpEx.ErrorCode;
            }
            else
            {
                log.Message = String.Format(ECode.GetMessage(ECode.INTERNAL_SERVER_ERROR), ex.Message + ex.StackTrace);
                log.ErrorCode = ECode.INTERNAL_SERVER_ERROR;
            }

            Logger.PutError(log);

            Console.WriteLine(LogMessage.FinishedForError);
            Console.ReadLine();
        }

    }
}
