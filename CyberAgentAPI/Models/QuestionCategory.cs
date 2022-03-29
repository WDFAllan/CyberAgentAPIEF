using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class QuestionCategory
    {
        public QuestionCategory()
        {
            InverseParent = new List<QuestionCategory>();
           
        }

        public int QuestionCategoryId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual QuestionCategory Parent { get; set; }
        public virtual ICollection<QuestionCategory> InverseParent { get; set; }

        //public List<Question> Questions { get; set; }

    }
}
