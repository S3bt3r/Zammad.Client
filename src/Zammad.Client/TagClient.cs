using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Core;
using Zammad.Client.Resources;
using Zammad.Client.Resources.Internal;
using Zammad.Client.Services;

namespace Zammad.Client
{
    public class TagClient : ZammadClient, ITagService
    {
        public TagClient(ZammadAccount account)
            : base(account)
        {

        }

        #region ITagService

        public async Task<IList<string>> GetTagListAsync(string objectName, int objectId)
        {
            var tagList = await GetAsync<StringTagList>("/api/v1/tags", $"object={objectName}&o_id={objectId}").ConfigureAwait(false);
            return tagList.Tags;
        }

        public Task<IList<Tag>> SearchTagAsync(string term)
        {
            return GetAsync<IList<Tag>>("/api/v1/tag_search", $"term={term}");
        }

        public Task<bool> AddTagAsync(string objectName, int objectId, string tagName)
        {
            return GetAsync<bool>("/api/v1/tags/add", $"object={objectName}&o_id={objectId}&item={tagName}");
        }

        public Task<bool> RemoveTagAsync(string objectName, int objectId, string tagName)
        {
            return GetAsync<bool>("/api/v1/tags/remove", $"object={objectName}&o_id={objectId}&item={tagName}");
        }

        public Task<IList<Tag>> GetTagListAdminAsync()
        {
            return GetAsync<IList<Tag>>("/api/v1/tag_list");
        }

        public Task<bool> CreateTagAdminAsync(Tag tag)
        {
            return PostAsync<bool>("/api/v1/tag_list", tag);
        }

        public Task<bool> RenameTagAdminAsync(Tag tag)
        {
            return PutAsync<bool>("/api/v1/tag_list", tag);
        }

        public Task<bool> DeleteTagAdminAsync(Tag tag)
        {
            return DeleteAsync<bool>("/api/v1/tag_list", tag);
        }

        #endregion
    }
}
