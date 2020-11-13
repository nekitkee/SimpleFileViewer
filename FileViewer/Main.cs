using InterfacePlugIn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FileViewer
{
    public partial class Main : Form
    {
        private List<PlugIn> pluginList = new List<PlugIn>(); 
        private List<Setting> settings;

        public Main()
        {
            InitializeComponent(); 
            LoadAssamblies();

        }

        private void LoadAssamblies()
        {
            settings = Setting.ReadFromFile();
            if (settings == null)
            {
                settings = new List<Setting>();
            }
            else
            {
                foreach (var asmSetting in settings)
                {
                    Assembly assem = Assembly.LoadFrom(asmSetting.AssamblyPath);
                    PlugIn plugin = new PlugIn
                    {
                        IncludedAssembly = assem,
                        Extension = asmSetting.Extension
                    };
                    pluginList.Add(plugin);
                }
            }
        }


        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowPluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            foreach (var format in settings)
                s += format.Extension + ";\n"; 
            MessageBox.Show(s);
        }

        private void AddPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Allow user to select an assembly to load. 
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!dlg.FileName.Contains("Plugin.dll")) 
                    MessageBox.Show("It is NOT Plug-In!");
                else if (!LoadExternalModule(dlg.FileName)) 
                    MessageBox.Show("Nothing implements InterfacePlugIn!");
            }

        }

        void OpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog(); 
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (PlugIn format in pluginList)
                {
                    if (dlg.FileName.Contains(format.Extension))
                    {
                        MessageBox.Show("Мы можем открыть это файл");
                        var theClassTypes = from t in format.IncludedAssembly.GetTypes()
                                            where t.IsClass && (t.GetInterface("IAppFunctionality") != null)
                                            select t;

                        foreach (Type t in theClassTypes)
                        {
                            var itfApp = (IAppFunctionality)(format.IncludedAssembly.CreateInstance(t.FullName, true));
                            itfApp.OpenFile(dlg.FileName);
                        }

                        return;
                    }
                }
                MessageBox.Show("Мы НЕ можем открыть это файл");
            }
        }

        private bool LoadExternalModule(string path)
        {
            bool foundPlugIn = false; 
            Assembly thePlugInAsm = null; 
            var setting = new Setting(); 
            PlugIn plugin = new PlugIn(); 
            try
            {
                // Dynamically load the selected assembly. 
                thePlugInAsm = Assembly.LoadFrom(path);
                setting.AssamblyName = thePlugInAsm.GetName().ToString(); 
                setting.AssamblyPath = path;
                plugin.IncludedAssembly = thePlugInAsm;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
                return foundPlugIn;
            }

            // Get all IAppFunctionality compatible classes in assembly.
            var theClassTypes = from t in thePlugInAsm.GetTypes()  
                                where t.IsClass && (t.GetInterface("IAppFunctionality") != null) 
                                select t;

            // Now, create the object and call IncludeIt() method.
            foreach(Type t in theClassTypes)
            {
                foundPlugIn = true;
                // Use late binding to create the type. 
                var itfApp = (IAppFunctionality)thePlugInAsm.CreateInstance(t.FullName, true); 
                itfApp.IncludeIt();

                // Show extension  info. 
                setting.Extension = DisplayFileFormat(t); 
                plugin.Extension = setting.Extension; 
                bool isAdded = false;
                foreach (PlugIn pl in pluginList)
                {
                    if (pl.IncludedAssembly.Equals(plugin.IncludedAssembly)) 
                        isAdded = true;
                }
                if (!isAdded)
                {
                    pluginList.Add(plugin); 
                    settings.Add(setting); 
                    Setting.AddToFile(settings);
                }
            }
            return foundPlugIn;
        }

        private string DisplayFileFormat(Type t)
        {
            // Get [File Format] data.
            var fileFormat = from ci in t.GetCustomAttributes(false)
                             where
                             (ci.GetType() == typeof(PlugInInfoAttribute))
                             select ci;

            // Show data. 
            string format = "";
            foreach (PlugInInfoAttribute c in fileFormat)
            {
                MessageBox.Show(string.Format("The are {0} file format viewer", c.FileFormat), c.PlugInName);
                format = c.FileFormat.ToString();

            }
            return format;
        }
    }
}
