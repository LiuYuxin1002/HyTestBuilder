namespace HyTestIEInterface
{
    /// <summary>
    /// 操作后返回结果，出错时error!=0，并且有msg.
    /// </summary>
    public class OperationResult
    {
        private int error;//should be transfer to Errorcode.
        private string msg;

        public OperationResult() { }
        public OperationResult(int error, string msg)
        {
            this.error = error;
            this.msg = msg;
        }
    }

    public enum ErrorCode
    {

    }
}
