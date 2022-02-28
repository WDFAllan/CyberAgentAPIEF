using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class Survey
    {
        public Survey()
        {
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        public int SurveyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
