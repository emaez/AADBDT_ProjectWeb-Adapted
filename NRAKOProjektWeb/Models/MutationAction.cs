using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Models
{
    public class MutationAction
    {
        public String Action { get; set; }
        public Dictionary<string,string> Params { get; set; }
    }
}
