using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ClassFileCopyParser
{
    class Program
    {
        public string parentfolderpath = "";
        public string SearchPattern = "";
        public string replaceString = "";
        public string whatYouWnattoReplace = "";
        helperClass helper = new helperClass();
        public void Main()
        {
            Console.WriteLine("Please click what you want to do 1.Add Service 2.Delete service");
            var choice = Console.ReadLine();
            var args=helper.ReadJson();
            foreach(var args2 in args) {
                parentfolderpath = args2.path + args2.foldername;
                SearchPattern = args2.fileName + args2.filetype;
                whatYouWnattoReplace = args2.fileName;
                helper.logging("I will clone all files from this folder path and Which contains pattern " + args2.fileName + " and has " + args2.filetype + " extension", parentfolderpath);

                if (choice == "1")
                {
                    foreach (var item in args2.patternInTemplateShoulReplace)
                    {
                        //args2.patternInTemplateShoulReplace
                        filesAddbasedofpattern(item);
                    }

                   
                }
                else if (choice == "2")
                {
                    filesDeletebasedofpattern();
                  //  Main();
                }
                else if (choice == "3")
                {
                    filesToUpdate();
                }
            }
            Main();
            Console.Read();

        }

        #region files add based on pattern
        public bool filesAddbasedofpattern(string replaceString)
        {
            List<string> listOfpaths = new List<string>();
            Console.WriteLine("Please enter proper name of service you want to cloned.");



            iterateInToFiles("1", replaceString);
        
            return true;
        }
        
        public  Boolean WriteLinesInfile(string path)
        {
            List<string> text = System.IO.File.ReadAllLines(path).ToList();
            var tempLines = new List<string>();
            try {
               foreach(string textLine in text)       
                {
                    tempLines.Add(textLine.Replace(whatYouWnattoReplace, replaceString)
                        .Replace(whatYouWnattoReplace.ToLower(),replaceString.ToLower())
                        .Replace(whatYouWnattoReplace.Replace(whatYouWnattoReplace.Substring(0,1), whatYouWnattoReplace.Substring(0, 1).ToLower()),
                        (replaceString.Replace(replaceString.Substring(0, 1), replaceString.Substring(0, 1).ToLower()))
                        ));
                }
                
                System.IO.File.WriteAllLines(path.Replace(whatYouWnattoReplace, replaceString)
                    .Replace(whatYouWnattoReplace.ToLower(), replaceString.ToLower())
                        .Replace(whatYouWnattoReplace.Replace(whatYouWnattoReplace.Substring(0, 1), whatYouWnattoReplace.Substring(0, 1).ToLower()),
                        (replaceString.Replace(replaceString.Substring(0, 1), replaceString.Substring(0, 1).ToLower()))
                        )


                    , tempLines);
              
                helper.logging("we cloned a above file and clone file's path is", path.Replace(whatYouWnattoReplace, replaceString));
            }
            catch{
                return false;
            }
            return true;
        }

        #endregion
        
        #region delete files based on pattern
        public List<string> filesDeletebasedofpattern()
        {
            List<string> listOfpaths = new List<string>();
            Console.WriteLine("Please enter proper name from service you want to delete.");
         
            replaceString = Console.ReadLine();
            iterateInToFiles("3","");
            return listOfpaths;
        }
        #endregion

        #region pattern based iterate
        private void iterateInToFiles(string operationType,string replacestringtopass)
        {
            foreach (string pathoffile in Directory.GetFiles(parentfolderpath, "*" + SearchPattern, SearchOption.AllDirectories).Select(Path.GetFullPath))
            {
                var booltoproceed = helper.checkpathallowed(pathoffile);
                if (booltoproceed)
                {
                  
                    switch (operationType)
                    {
                        case TypeOfOperation.Add:
                            helper.logging("we are accessing this folder", pathoffile);
                            replaceString = replacestringtopass;
                            WriteLinesInfile(pathoffile);
                            break;
                        case TypeOfOperation.Update:
                            helper.logging("we are accessing this folder", pathoffile);
                            break;
                        case TypeOfOperation.Delete:
                            helper.logging("we are going to delete this file", pathoffile.Replace(whatYouWnattoReplace, replaceString));
                            Console.WriteLine("Please approve delete by typing Y for Yes and N for No.");
                            var approve = Console.ReadLine();
                            if (approve.ToLower()=="y") { 
                            File.Delete(pathoffile.Replace(whatYouWnattoReplace, replaceString));
                                Console.WriteLine(pathoffile + "\n deleted" + "\n");
                            }
                            else
                            {
                                Console.WriteLine(pathoffile + "did not deleted" + "\n");
                            }
                           
                            break;

                    }
                    

                }
            }
        }

        #endregion
        
       public void filesToUpdate()
        {

        }


    }
}
