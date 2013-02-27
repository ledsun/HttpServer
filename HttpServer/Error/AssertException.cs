using System;
using System.Diagnostics;

namespace HttpServer.Util
{
    /// <summary>
    /// 表明クラス
    /// </summary>
    class AssertException
    {
        public static void Assert(bool condition, string msg)
        {
            if (!condition)
            {
                throw new Exception(msg);
            }
        }

        /// <summary>
        /// null であること
        /// </summary>
        /// <param name="val"></param>
        public static void IsNull(object obj)
        {
            Debug.Assert(obj == null, "this value is null.");
        }

        /// <summary>
        /// null でないこと
        /// </summary>
        /// <param name="val"></param>
        public static void IsNotNull(object obj)
        {
            Debug.Assert(obj != null, "this value is not null.");
        }

        /// <summary>
        /// 到達することがないはず
        /// </summary>
        public static void NotFalled()
        {
            Assert(false, "this line is not falled");
        }
    }
}