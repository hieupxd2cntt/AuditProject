using System.Threading.Tasks;

namespace WebAppCoreNew.Utils
{
    public interface IRazorViewRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
