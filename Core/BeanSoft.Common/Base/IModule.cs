namespace Core.Base
{
    public interface IModule
    {
        object this[string fieldID] { get; set; }
    }
}
