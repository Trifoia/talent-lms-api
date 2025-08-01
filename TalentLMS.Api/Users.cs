﻿using Refit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using static TalentLMS.Api.Users.User;

namespace TalentLMS.Api
{
    // linke to docs: https://market.talentlms.com/pages/docs/TalentLMS-API-Documentation.pdf

    public partial interface ITalentApi
    {
        [Get("/users")]
        Task<ApiResponse<List<Users.AllUserRetreivalDto>>> Users();

        [Get("/users?id={userId}")]
        Task<ApiResponse<Users.User>> User(string userId);

        [Get("/users?email={userEmail}")]
        Task<ApiResponse<Users.User>> UserByEmail(string userEmail);

        //[Post("/usersignup")]
        //Task<ApiResponse<Users.BasicUser>> UserSignupOld([Body] Users.NewUserQA data);

        [Post("/usersignup")]
        Task<ApiResponse<Users.BasicUser>> UserSignup([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string,object> data);

        [Get("/addusertobranch?user_id={userId}&branch_id={branchId}")]
        Task<ApiResponse<UserBranch>> AddUserToBranch(string userId, string branchId);

        [Post("/edituser")]
        Task<ApiResponse<Users.BasicUser>> EditUser([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Get("/isuseronline/user_id:{userId}")]
        Task<ApiResponse<IsUserOnlineResponse>> IsUserOnline(string userId);
    }

    namespace Users
    {
        public record BasicUser(
            int Id,
            string Login,
            string First_Name,
            string Last_Name,
            string Email,
            string RestrictEmail,
            string UserType,
            string TimeZone,
            string Language,
            string Status,
            DateTime? DeactivationDate,
            string Level,
            string Points,
            DateTime CreatedOn,
            DateTime LastUpdated,
            string LastUpdatedTimestamp,
            string Avatar,
            string Bio,
            string LoginKey);

        public record AllUserRetreivalDto(
            string Id,
            string Login,
            string First_Name,
            string Last_Name,
            string Email,
            string RestrictEmail,
            string UserType,
            string TimeZone,
            string Language,
            string Status,
            DateTime? DeactivationDate,
            string Level,
            string Points,
            DateTime CreatedOn,
            string Last_Updated,
            string Last_Updated_Timestamp,
            string Avatar,
            string Bio,
            string LoginKey,
            string? Custom_Field_7);
        public record User(
            string Id,
            string Login,
            string First_Name,
            string Last_Name,
            string Email,
            string RestrictEmail,
            string UserType,
            string TimeZone,
            string Language,
            string Status,
            DateTime? DeactivationDate,
            string Level,
            string Points,
            DateTime CreatedOn,
            DateTime LastUpdated,
            string LastUpdatedTimestamp,
            string Avatar,
            string Bio,
            string LoginKey,
            List<User.Course> Courses,
            List<User.Branch> Branches,
            List<User.Group> Groups,
            List<User.Certification> Certifications,
            List<User.Badge> Badges)
        {
            public record Branch(string Id, string Name);

            public record Course(
                string Id,
                string Name,
                string Role,
                DateTime EnrolledOn,
                string EnrolledOnTimestamp,
                DateTime? CompletedOn,
                string CompletedOnTimestamp,
                string CompletionStatus,
                string CompletionPercentage,
                DateTime? ExpiredOn,
                string ExpiredOnTimestamp,
                string TotalTime);

            public record Group(
                string Id,
                string Name);

            public record Certification(
                string Course_Id,
                string Course_Name,
                string Unique_Id,
                string Issued_Date,
                long Issued_Date_Timestamp,
                string Expiration_Date,
                string Download_Url,
                string Public_Url);

            public record Badge(
                string Name,
                string Type,
                string Criteria,
                DateTime IssuedOn,
                string BadgeSetId);

            public record UserBranch(
               string user_id,
               string branch_id,
               string branch_name
                );

            public record IsUserOnlineResponse(
                bool Online
                );
        }
    }
}