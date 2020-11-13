using InterfacePlugIn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BmpPlugin
{
    [PlugInInfo(PlugInName = "Bmp file viewer", FileFormat = ".bmp")]
    public class CSharpModuleBmp : IAppFunctionality
    {
        public void IncludeIt()
        {
            MessageBox.Show("You have just included PlugIn!");
        }

        public void OpenFile(string fileName)
        {
            var form = new BmpViewerForm(); 
            form.Show();
            var bitmap = new Bitmap(fileName);
            form.BmpPictureBox.Image = bitmap;
        }
    }
}
