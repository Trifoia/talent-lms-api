using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace TalentLMS.Api
{
    public partial interface ITalentApi
    {
        [Get("/groups")]
        Task<ApiResponse<List<Groups.BasicGroup>>> Groups();

        [Get("/groups?id={groupId}")]
        Task<ApiResponse<Groups.Group>> Group(string groupId);

        [Post("/creategroup")]
        Task<ApiResponse<Groups.BasicGroup>> CreateGroup([Body(BodySerializationMethod.UrlEncoded)]Dictionary<string, string> data);
    }

    namespace Groups
    {
        public record BasicGroup(
            int Id,
            string Name,
            string? Description,
            string? Key,
            string? Price,
            string OwnerId,
            string? BelongsToBranch,
            string? MaxRedemptions,
            string? RedemptionsSoFar);

        public record CreateGroupRequest(
            string Name,
            string? Description = null,
            string? Key = null,
            string? Price = null,
            string? CreatorId = null,
            string? MaxRedemptions = null);

        public record Group(
            string Id,
            string Name,
            string Description,
            string Key,
            string Price,
            string OwnerId,
            string? BelongsToBranch,
            string? MaxRedemptions,
            string? RedemptionsSofar,
            List<Group.User> Users,
            List<Group.Course> Courses)
        {
            public record User(
                string Id,
                string Name);

            public record Course(
                string Id,
                string Name);
        }
    }
}

