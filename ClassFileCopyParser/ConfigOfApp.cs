using System;
using System.Collections.Generic;
using System.Text;

namespace ClassFileCopyParser
{
   public class ConfigOfApp
    {
        public string path { get; set; }
        public string foldername { get; set; }

        public string filetype { get; set; }

        public  string fileName { get; set; }

        public List<string> patternInTemplateShoulReplace { get; set; }

    }
}
