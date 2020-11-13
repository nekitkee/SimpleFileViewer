using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FileViewer
{
    [Serializable, XmlRoot(Namespace = "http://MyCompany.com")]
    public class Setting
    {
        [XmlAttribute]
        public string Extension { get; set; }
        [XmlAttribute]
        public string AssamblyName { get; set; }
        [XmlAttribute]
        public string AssamblyPath { get; set; }

        public static string SettingFileName = "pluginsList.xml"; 
        public Setting() { }

        public static List<Setting> ReadFromFile()
        {
            if (File.Exists(SettingFileName))
            {
                var xmlFormat = new XmlSerializer(typeof(List<Setting>));
                using var fStream = new FileStream(SettingFileName,
                FileMode.Open, FileAccess.Read, FileShare.None);
                return (List<Setting>)xmlFormat.Deserialize(fStream);
            }
            else 
            { 
                return null; 
            }
        }

        public static void AddToFile(object SettingList)
        {
            var xmlFormat = new XmlSerializer(typeof(List<Setting>)); 
            using var fStream = new FileStream(SettingFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            xmlFormat.Serialize(fStream, SettingList);
        }
    }

}
