using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TalentLMS.Api
{
    public partial interface ITalentApi
    {
        [Post("/gettimeline")]
        Task<ApiResponse<List<TimelineFailures.CourseFailureEvent>>> GetTimelineFailures([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

    }

    namespace TimelineFailures
    {
        public record CourseFailureEvent(
            string Action,
            string Message,
            string Timestamp,
            string Unix_Timestamp,
            string User_Id,
            string User_Username,
            string User_Email,
            string User_Fullname,
            string Object_Id,
            string Object_Name,
            string Secondary_Object_Id,
            string Secondary_Object_Name,
            string Event_Counter
        );
    }
}

//Check with Marty about setting the key
//var response = await _talent_api.GetTimelineFailures(new Dictionary<string, string>
//                     {
//                        { "event_type", course_failure },
//                        { "user_id", "<userId>" }
//                     });
