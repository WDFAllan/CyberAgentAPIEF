using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class History
    {
        public int HistoryId { get; set; }
        public int SurveyQuestionId { get; set; }
        public string Answer { get; set; }

        public virtual SurveyQuestion SurveyQuestion { get; set; }
    }
}
