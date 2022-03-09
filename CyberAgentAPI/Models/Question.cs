using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class Question
    {
        public Question()
        {
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Domain { get; set; }
        public string AuditType { get; set; }
        public bool Small { get; set; }
        public bool Medium { get; set; }
        public bool Big { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string KeyWord { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
