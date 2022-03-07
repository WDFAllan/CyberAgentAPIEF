using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class SurveyQuestion
    {
        public SurveyQuestion()
        {
            Answers = new HashSet<Answers>();
        }

        public int SurveyQuestionId { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string Answer { get; set; }

        public virtual Question Question { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual ICollection<Answers> Answers { get; set; }
    }
}
