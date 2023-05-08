using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemSprawozdan.Shared.Dto
{
    public class ReportTopicDto
    {
        public string Subject { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public int SubjectGroupId { get; set; }
        public string Major { get; set; }
    }
}
