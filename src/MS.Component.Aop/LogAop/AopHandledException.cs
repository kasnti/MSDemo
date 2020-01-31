using System;

namespace MS.Component.Aop
{
    /// <summary>
    /// 使用自定义的Exception，用于在aop中已经处理过的异常，在其他地方不用重复记录日志
    /// </summary>
    public class AopHandledException : ApplicationException
    {
        public string ErrorMessage { get; private set; }
        public Exception InnerHandledException { get; private set; }
        //无参数构造函数
        public AopHandledException()
        {

        }
        //带一个字符串参数的构造函数，作用：当程序员用Exception类获取异常信息而非 MyException时把自定义异常信息传递过去
        public AopHandledException(string msg) : base(msg)
        {
            this.ErrorMessage = msg;
        }
        //带有一个字符串参数和一个内部异常信息参数的构造函数
        public AopHandledException(string msg, Exception innerException) : base(msg)
        {
            this.InnerHandledException = innerException;
            this.ErrorMessage = msg;
        }
        public string GetError()
        {
            return ErrorMessage;
        }
    }
}