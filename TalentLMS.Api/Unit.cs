﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TalentLMS.Api.Units;

namespace TalentLMS.Api
{
    public partial interface ITalentApi
    {
        [Get("/getuserprogressinunits?unit_id={unitId}")]
        Task<ApiResponse<IEnumerable<UserUnitProgress>>> UserProgress(string unitId);

        [Get("/getuserprogressinunits?unit_id={unitId}&user_id={userId}")]
        Task<ApiResponse<UserUnitProgress>> UserProgress(string unitId, string userId);

        [Get("/gettestanswers?test_id={testId}&user_id={userId}")]
        Task<ApiResponse<TestResult>> TestAnswers(string testId, string userId);

        [Get("/getsurveyanswers?survey_id={surveyId}&user_id={userId}")]
        Task<ApiResponse<SurveyResult>> SurveyAnswers(string surveyId, string userId);


        [Get("/getiltsessions?ilt_id={iltId}")]
        Task<ApiResponse<List<IltSession>>> IltSessions(string iltId);
    }

    namespace Units
    {
        public record UserUnitProgress(
            string UserId,
            string Status,
            int Score);

        public record TestResult(
            string TestId,
            string TestName,
            string UserId,
            string UserName,
            string Score,
            string CompletionStatus,
            DateTime CompletedOn,
            int CompletedOnTimestamp,
            string TotalTime,
            List<TestResult.TestQuestion> Questions)
        {
            public record TestQuestion(
                string Id,
                string Text,
                string Type,
                string Weight,
                string Correct,
                IDictionary<string, string> Answers,
                IDictionary<string, string> CorrectAnswers,
                IDictionary<string, string> UserAnswers);
        }

        public record SurveyResult(
            string SurveyId,
            string SurveyName,
            string UserId,
            string UserName,
            string CompletionStatus,
            DateTime CompletedOn,
            string CompletedOnTimestamp,
            string TotalTime,
            List<SurveyResult.SurveyQuestion> Questions)
        {
            public record SurveyQuestion(
                string Id,
                string Text,
                string Type,
                IDictionary<string, string> Answers,
                IDictionary<string, string> UserAnswers);
        }

        public record IltSession(
            string Id,
            string Name,
            string MultiName,
            string LinkedTo,
            string Type,
            string OwnerId,
            string InstructorId,
            string Description,
            string Location,
            string StartTimestamp,
            DateTime StartDate,
            string Capacity,
            string DurationMinutes);
    }
}