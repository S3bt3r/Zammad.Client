using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Resources;

namespace Zammad.Client.Services
{
    public interface ITagService
    {
        Task<IList<Tag>> GetTagListAsync(string objectName, int objectId);
        Task<IList<Tag>> SearchTagAsync(string term);
        Task<bool> AddTagAsync(string objectName, int objectId, string tagName);
        Task<bool> RemoveTagAsync(string objectName, int objectId, string tagName);
        Task<IList<Tag>> GetTagListAdminAsync();
        Task<bool> CreateTagAdminAsync(Tag tag);
        Task<bool> RenameTagAdminAsync(Tag tag);
        Task<bool> DeleteTagAdminAsync(Tag tag);
    }
}
