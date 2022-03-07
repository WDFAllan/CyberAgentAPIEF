using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class Answers
    {
        public int AnswerId { get; set; }
        public int SurveyQuestionId { get; set; }
        public string Answer { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        //public virtual SurveyQuestion SurveyQuestion { get; set; }
        public virtual User User { get; set; }
    }
}
