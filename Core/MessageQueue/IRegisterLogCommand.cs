namespace MessageQueue
{
    public interface IRegisterLogCommand
    {
        string ErrCode { get; }
        string ErrDesc { get; }
    }
}
