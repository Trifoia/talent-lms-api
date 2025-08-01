using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Refit;
using TalentLMS.Api.Courses;

namespace TalentLMS.Api
{
    public partial interface ITalentApi
    {
        [Get("/getuserstatusincourse?user_id={userId}&course_id={courseId}")]
        Task<ApiResponse<UserCourseStatus>> UserStatus(string userId, string courseId);

        [Get("/courses?id={courseId}")]
        Task<ApiResponse<Course>> Course(string courseId);

        [Get("/courses")]
        Task<ApiResponse<IEnumerable<Course>>> Courses();

        [Post("/addusertocourse")]
        Task<ApiResponse<List<UserCourse>>> AddUserToCourse([Body] UserCourse data);

        [Get("/addusertobranch?user_id={user_id}&branch_id={branch_id}")]
        Task<ApiResponse<List<UserBranch>>> AddUserTobranch(string user_id, string branch_id);

        [Get("/removeuserfromcourse/user_id:{user_id},course_id:{course_id}")]
        Task<ApiResponse<dynamic>> RemoveUserFromCourse(string user_id, string course_id);

        [Post("/resetuserprogress")]
        Task<ApiResponse<dynamic>> ResetUserProgress([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);



    }

    namespace Courses
    {

        public record UserCourse(
            string user_id,
            string course_id,
            string role
            );

        public record Course(
            string Id,
            string Name,
            string Code,
            string CategoryId,
            string Description,
            string Price,
            string Status,
            DateTime CreationDate,
            DateTime LastUpdateOn,
            string CreatorId,
            string HideFromCatalog,
            string TimeLimit,
            string Level,
            string Shared,
            string SharedUrl,
            string Avatar,
            string BigAvatar,
            string Certification,
            string CertificationDuration,
            List<Course.User> Users,
            List<Course.Unit> Units)
        {
            public record User(
                string Id,
                string Name,
                string Role,
                DateTime EnrolledOn,
                string EnrolledOnTimestamp,
                [property: JsonPropertyName("completed_on")] string CompletedOn,
                [property: JsonPropertyName("completed_on_timestamp")] string CompletedOnTimestamp,
                [property: JsonPropertyName("completion_percentage")] string CompletionPercentage,
                DateTime? ExpiredOn,
                string ExpiredOnTimestamp,
                string TotalTime)
            {
                public bool IsLearner => Role == "learner";
            }

            public record Unit(
                string Id,
                string Type,
                string Name,
                string DelayTime,
                string AggregatedDelayTime,
                string FormattedAggregatedDelayTime,
                string Url)
            {
                public bool IsSection => Type == "Section";
            }
        }

        public record UserBranch(
               string user_id,
               string branch_id,
               string branch_name
               );

        public record UserCourseStatus(
            string Role,
            DateTime EnrolledOn,
            string EnrolledOnTimestamp,
            string CompletionStatus,
            string CompletionPercentage,
            DateTime? CompletedOn,
            string CompletedOnTimestamp,
            DateTime? ExpiredOn,
            string ExpiredOnTimestamp,
            string TotalTime,
            List<UserCourseStatus.Unit> Units)
        {
            public record Unit(
                string Id,
                string Name,
                string Type,
                string CompletionStatus,
                DateTime? CompletedOn,
                string CompletedOnTimestamp,
                string Score,
                string TotalTime);

        }
    }
}