using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace TalentLMS.Api
{
    public partial interface ITalentApi
    {
        [Get("/groups")]
        Task<ApiResponse<List<Groups.GetBasicGroup>>> Groups();

        [Get("/groups?id={groupId}")]
        Task<ApiResponse<Groups.Group>> Group(string groupId);

        [Post("/creategroup")]
        Task<ApiResponse<Groups.CreateBasicGroup>> CreateGroup([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Get("/addcoursetogroup/course_id:{courseId},group_id:{groupId}")]
        Task<ApiResponse<Groups.AddCourseToGroupResponse>> AddCourseToGroup(string courseId, string groupId);

        [Get("/addusertogroup/user_id:{userId},group_key:{groupKey}")]
        Task<ApiResponse<Groups.AddUserToGroupResponse>> AddUserToGroup(string userId, string groupKey);


    }

    namespace Groups
    {

        public record AddCourseToGroupResponse(
        string CourseId,
        string GroupId,
        string GroupName);

        public record AddUserToGroupResponse(
            string UserId,
            string GroupId,
            string GroupName);

        public record AddCourseToGroupRequest(
        string CourseId,
        string GroupId);

        public record CreateBasicGroup(
            int Id,
            string Name,
            string? Description,
            string? Key,
            string? Price,
            string OwnerId,
            string? BelongsToBranch,
            string? MaxRedemptions,
            string? RedemptionsSoFar);

        public record GetBasicGroup(
            string Id,
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

        public static class GroupExtensions
        {
            public static GetBasicGroup ConvertToBasicGroup(CreateBasicGroup createBasicGroup)
            {
                return new GetBasicGroup(
                    Id: createBasicGroup.Id.ToString(),
                    Name: createBasicGroup.Name,
                    Description: createBasicGroup.Description,
                    Key: createBasicGroup.Key,
                    Price: createBasicGroup.Price,
                    OwnerId: createBasicGroup.OwnerId,
                    BelongsToBranch: createBasicGroup.BelongsToBranch,
                    MaxRedemptions: createBasicGroup.MaxRedemptions,
                    RedemptionsSoFar: createBasicGroup.RedemptionsSoFar
                );
            }
        }
    }
}

