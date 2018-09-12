using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassFileCopyParser
{
    /// <summary>
    /// this class is for helping some process which is important in main program excecution 
    /// </summary>
    public class helperClass
    {
        public  void logging(string messagetoPrint, string output)
        {
            Console.WriteLine(String.Format("-->{0}:-{1}", messagetoPrint, output));
        }
        
        public  bool checkpathallowed(string s)
        {
            List<string> excludePathIdetifier = new List<string>();
            excludePathIdetifier.Add(".BL.Test");
             excludePathIdetifier.Add(".Data.Test");
             excludePathIdetifier.Add(".Data");
            excludePathIdetifier.Add(".Entity");
            foreach (var i in excludePathIdetifier)
            {
                if (s.Contains(i))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// it will update file not gone create or add new file
        /// </summary>
        /// <returns></returns>
        public string gettemplateFromConfigForUpdate()
        {
        
            var listofjsonconfig=ReadJson();
        
           string replaceTextInIOC = "ElectronicSignature";     

            return replaceTextInIOC;

        }
        
        public List<ConfigOfApp> ReadJson()
        {
            var currentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())); ;
            DirectoryInfo di = new DirectoryInfo(currentDirectory);
            Console.WriteLine("try to find config file in below folder" + di.Parent.FullName);
            List<ConfigOfApp> data = new List<ConfigOfApp>();
            using (StreamReader r = new StreamReader(di.Parent.FullName + "/config.json"))
            {
                string json = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<ConfigOfApp>>(json);
            }
            return data;

        }
        
    }
}
