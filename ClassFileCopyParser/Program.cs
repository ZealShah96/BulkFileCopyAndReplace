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

     
                var args2=helper.ReadJson();
            
            parentfolderpath = args2.path+args2.foldername;
            SearchPattern = args2.fileName + args2.filetype;
            whatYouWnattoReplace = args2.fileName;
            helper.logging("I will clone all files from this folder path and Which contains pattern "+ args2.fileName + " and has "+ args2.filetype + " extension", parentfolderpath);

            if (choice == "1")
            {
               filesAddbasedofpattern();
                
                Main();
            }
            else if (choice == "2")
            {
                filesDeletebasedofpattern();
                Main();
            }

            Console.Read();

        }

        #region files add based on pattern
        public bool filesAddbasedofpattern()
        {
            List<string> listOfpaths = new List<string>();
            Console.WriteLine("Please enter proper name of service you want to cloned.");
            replaceString = "ElectronicSignature";


            iterateInToFiles("1");
            //foreach (string pathoffile in Directory.GetFiles(fullPathString,"*"+ SearchPattern, SearchOption.AllDirectories).Select(Path.GetFullPath))
            //{
            //    var booltoproceed = checkpathallowed(pathoffile);
            //    if (booltoproceed)
            //    {
            //        logging("we are accessing this folder", pathoffile);
            //        WriteLinesInfile(replaceString, withwhatyouwhattoreplace, pathoffile);

            //    }
            //}
            //        makeSignatureInIocConfiguration(folderLocationString, withwhatyouwhattoreplace,serviceName);
            return true;
        }
        
        public  Boolean WriteLinesInfile(string path)
        {
            List<string> text = System.IO.File.ReadAllLines(path).ToList();
            var tempLines = new List<string>();
            try {
               foreach(string textLine in text)       
                {
                    tempLines.Add(textLine.Replace(whatYouWnattoReplace, replaceString));
                }
                
                System.IO.File.WriteAllLines(path.Replace(whatYouWnattoReplace, replaceString), tempLines);
               // logging("File Created ",path.Replace(replaceString, withwhatyouwhattoreplace));
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
            //var deleteService = ;

            //foreach (string s in Directory.GetFiles(@"C:\Projects\exxat-team\apps\\"+ServiceName+"-Service\\", "*" + deleteService + "*.cs", SearchOption.AllDirectories).Select(Path.GetFullPath))
            //{
            //    var booltoproceed = helper.checkpathallowed(s);
            //    if (booltoproceed)
            //    {
            //        File.Delete(s);
            //        Console.WriteLine(s + "\n deleted" + "\n");
            //    }

            //}
            replaceString = Console.ReadLine();
            iterateInToFiles("3");
            return listOfpaths;
        }
        #endregion

        #region pattern based iterate
        private void iterateInToFiles(string operationType)
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

        //private  void makeSignatureInIocConfiguration(string withwhatyouwhattoreplace, string serviceName)
        //{
        //    var scopeString=makeServiceString(withwhatyouwhattoreplace, serviceName);
        //}



        //private  string makeServiceString(string withwhatyouwhattoreplace,string serviceName)
        //{

        //    foreach (string s in Directory.GetFiles(@"C:\\Projects\\exxat-team\\apps\\"+serviceName+ "-Service\\", "*" + "IocContainer" + "*.cs", SearchOption.AllDirectories).Where(x=>x.Contains(".Business")).Select(Path.GetFullPath))
        //    //  .Where(x=>!x.Contains("Models")).Select(Path.GetFullPath))

        //    {
        //        var booltoproceed = checkpathallowed(s);
        //        if (booltoproceed)
        //        {
        //            List<string> text = System.IO.File.ReadAllLines(s).ToList();
        //            var endlinenumber = 0;
        //            for(var i=0;i<text.Count;i++)
        //            {
        //                // Console.WriteLine(textline);

        //                if (text[i].IndexOf(";") > 0)
        //                {
        //                    endlinenumber = i+1;
        //                }
        //            }

        //            text[endlinenumber] = text[endlinenumber] +"#region Zeal\n"+ scopeWillAdd.Replace(replaceTextInIOC, withwhatyouwhattoreplace) + scopeWillSingletonAdd.Replace(replaceTextInIOC, withwhatyouwhattoreplace)+"\n#endregion";
        //            System.IO.File.WriteAllLines(s, text);
        //            Console.WriteLine(endlinenumber);

        //        }

        //    }
        //    return scopeWillAdd;
        //}




    }
}
