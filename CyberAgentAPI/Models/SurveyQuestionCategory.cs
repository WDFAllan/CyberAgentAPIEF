using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class SurveyQuestionCategory
    {
        public SurveyQuestionCategory()
        {
            InverseParent = new HashSet<SurveyQuestionCategory>();
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        public int SurveyQuestionCategoryId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual SurveyQuestionCategory Parent { get; set; }
        public virtual ICollection<SurveyQuestionCategory> InverseParent { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
