using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyManagementSystem.Models
{
    public class ResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;
        
        /// <summary>
        /// 結果物件
        /// </summary>
        public object ReturnObject { get; set; }
        
        /// <summary>
        /// 取得物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetReturn<T>()
        {
            if (ReturnObject == null)
                return default(T);

            return (T)Convert.ChangeType(this.ReturnObject, typeof(T));
        }
        
        /// <summary>
        /// 例外
        /// </summary>
        public Exception Err { get; set; }
        
        /// <summary>
        /// 訊息代碼
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }
    }
}