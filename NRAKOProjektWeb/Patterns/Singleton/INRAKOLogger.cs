using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.Singleton
{
    public interface INRAKOLogger
    {
        void Log(string userId, string text);
    }
}
