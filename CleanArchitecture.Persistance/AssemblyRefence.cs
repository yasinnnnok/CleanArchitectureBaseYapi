using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance
{
    public static class AssemblyRefence
    {
        public static readonly Assembly Assembly = typeof(Assembly).Assembly;
    }
}
