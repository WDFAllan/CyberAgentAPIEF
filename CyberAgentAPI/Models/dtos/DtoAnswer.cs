using System;

namespace CyberAgentAPI.Models.dtos
{
    public class DtoAnswer
    {
        public int SurveyQuestionId { get; set; }
        public string Answer1 { get; set; }
        public int UserId { get; set; }
    }
}
