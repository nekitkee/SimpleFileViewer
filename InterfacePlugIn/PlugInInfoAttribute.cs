using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacePlugIn
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PlugInInfoAttribute : Attribute
    {
        public string PlugInName { get; set; }
        public string FileFormat { get; set; }
    }
}
