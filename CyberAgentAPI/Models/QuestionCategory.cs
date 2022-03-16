using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class QuestionCategory
    {
        

        public int QuestionCategoryId { get; set; }
        public string Name { get; set; }
        public int? Parent { get; set; }

    }
}
