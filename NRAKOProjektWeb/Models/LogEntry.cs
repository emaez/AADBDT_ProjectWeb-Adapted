using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public NRAKOUser User { get; set; }
        public string Text { get; set; }
        public DateTime CretedAt { get; set; }
    }
}
