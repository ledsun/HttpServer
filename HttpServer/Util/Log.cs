using System.Text;

namespace HttpServer.Util
{
    /// <summary>
    /// ログ情報クラス
    /// 
    /// ・ログの情報を保持
    /// ・ログの情報をカンマ区切りで並べて返す
    /// ・「null」を許容
    /// ・外部からのアクセス可
    /// </summary>
    class Log
    {
        /// <summary>
        /// ログ出力情報のフォーマット
        /// 
        /// {0}, {1}, {2}, {3}, {4}, {5}
        /// 
        ///  - {0}：リクエストのURL
        ///  - {1}：メソッド
        ///  - {2}：リクエストのContentType
        ///  - {3}：リクエストの
        ///  - {4}：レスポンスのURL
        ///  - {5}：レスポンスのステータスコード
        ///  - {6}：レスポンスのステータスの値
        ///  - {7}：メッセージ
        ///  - {8}：エラーコード
        /// </summary>
        private const string FORMAT_LOG_INFO = "{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}";

        /// <summary>
        /// リクエスト AcceptType の出力フォーマット
        /// 
        /// [{0}]
        /// 
        ///  - {0}：AcceptTypes[x]
        /// </summary>
        private const string FORMAT_ACCEPT_TYPE = "[{0}]";


        /// <summary>
        /// リクエストURL
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// メソッド名
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// リクエストに含まれている本体データの MIME タイプ
        /// </summary>
        public string RequestContentType { get; set; }

        /// <summary>
        /// クライアントが受け入れる MIME タイプ
        /// </summary>
        public string[] RequestAcceptType { get; set; }

        /// <summary>
        /// レスポンスURL
        /// </summary>
        public string ResponseUrl { get; set; }

        /// <summary>
        /// レスポンスのステータスコード
        /// </summary>
        public int ResponseStatusCode { get; set; }

        /// <summary>
        /// レスポンスのステータスコードに対応する値
        /// </summary>
        public string ResponseStatusDescription { get; set; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// エラーコード
        /// </summary>
        public string ErrorCode { get; set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Log() 
        {
            Initialization();
        }


        /// <summary>
        /// 初期化
        /// </summary>
        private void Initialization()
        {
            RequestUrl = "";
            MethodName = "";
            RequestContentType = "";
            RequestAcceptType = new string[] { "" };
            ResponseUrl = "";
            ResponseStatusCode = 0;
            ResponseStatusDescription = "";
            Message = "";
            ErrorCode = "";
        }


        /// <summary>
        /// ログ情報をカンマ区切りの文字列で返す
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Validate();

            return SerializeLog();
        }


        /// <summary>
        /// バリデーション
        /// </summary>
        /// <param name="request"></param>
        /// <param name="methodName"></param>
        protected void Validate()
        {
            AssertException.Assert(!string.IsNullOrEmpty(MethodName), "MethodName は必ず存在すること");
            AssertException.Assert(!string.IsNullOrEmpty(Message), "Message は必ず存在すること");
        }


        /// <summary>
        /// ログ情報を順番に並べて返す
        /// </summary>
        private string SerializeLog()
        {
            StringBuilder acceptType = new StringBuilder();
            if (RequestAcceptType != null)
            {
                foreach (string accept in RequestAcceptType)
                {
                    acceptType.Append(string.Format(Log.FORMAT_ACCEPT_TYPE, accept));
                }
            }

            string statusCode = ResponseStatusCode == 0 ? "" : ResponseStatusCode.ToString();

            return string.Format(FORMAT_LOG_INFO, RequestUrl, MethodName, RequestContentType, acceptType.ToString(), ResponseUrl, statusCode, ResponseStatusDescription, Message, ErrorCode);
        }


        /// <summary>
        /// ログのクリア
        /// </summary>
        public void Clear()
        {
            this.Message = "";
            this.ErrorCode = "";
        }

    }


    /// <summary>
    /// スレッド処理のログ：名前付け
    /// </summary>
    class ThreadLog : Log
    { }


    /// <summary>
    /// スレッドのプロセスログ：名前付け
    /// </summary>
    class ThreadProcessLog : Log
    { }

}
