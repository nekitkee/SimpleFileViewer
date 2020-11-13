using System.Reflection;

namespace FileViewer
{
    class PlugIn
    {
        public Assembly IncludedAssembly { get; set; }
        public string Extension { get; set; }
    }
}
