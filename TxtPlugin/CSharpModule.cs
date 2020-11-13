using InterfacePlugIn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TxtPlugin
{
    [PlugInInfo(PlugInName = "Text file viewer", FileFormat = ".txt")]
    public class CSharpModule : IAppFunctionality
    {
        public void IncludeIt()
        {
            MessageBox.Show("You have just included PlugIn!");
        }

        public void OpenFile(string fileName)
        {
            var form = new TextViewerForm(); form.Show();
            FileInfo file = new FileInfo(fileName);
            using (StreamReader streader = file.OpenText())
            {
                string input = null;
                while ((input = streader.ReadLine()) != null)
                {
                    form.txtTextFile.Text += input;
                }
            }

        }
    }

}
