using ERPPlugin.Dto;

namespace ERPPlugin
{
    public interface IERPPlugin
    {
        Task<List<Title>> GetTitle();
        Task<List<User>> GetUser();
    }
}
