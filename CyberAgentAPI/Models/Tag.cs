using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class Tag
    {
        public int TagId { get; set; }
        public int QuestionId { get; set; }
        public string TagName { get; set; }
        public string TagValue { get; set; }

        public virtual Question Question { get; set; }
    }
}
