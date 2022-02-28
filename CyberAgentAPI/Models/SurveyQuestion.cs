using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class SurveyQuestion
    {
        public SurveyQuestion()
        {
            Histories = new HashSet<History>();
        }

        public int SurveyQuestionId { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string Answer { get; set; }

        public virtual Question Question { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual ICollection<History> Histories { get; set; }
    }
}
