using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ClassFileCopyParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please click what you want to do 1.Add Service 2.Delete service");
            var choice = Console.ReadLine();
            Console.WriteLine("Please Enter Service Name like(e.g Student,Person,Site)");
            var serviceName= Console.ReadLine(); 
            if (choice == "1")
            {
                findAllPathWhichContainName(serviceName);

                string[] args1 = new string[10];
                Main(args1);
            }
            else if (choice == "2")
            {
                findAllPathWhichContainNameandDelete(serviceName);
                string[] args1 = new string[10];
                Main(args1);
            }
            Console.Read();

        }
        
        public static Boolean WriteLinesInfile(string replaceString,string withwhatyouwhattoreplace,string path)
        {
            List<string> text = System.IO.File.ReadAllLines(path).ToList();
            var tempLines = new List<string>();
            try {
               foreach(string textLine in text)
                {
                    tempLines.Add(textLine.Replace(replaceString, withwhatyouwhattoreplace));
                }
                
                System.IO.File.WriteAllLines(path.Replace(replaceString, withwhatyouwhattoreplace), tempLines);
                Console.Write("File Created "+ path.Replace(replaceString, withwhatyouwhattoreplace) + "\n");
            }
            catch{
                return false;
            }
            return true;
        }


        public static List<string> findAllPathWhichContainName(string serviceName)
        {
            List<string> listOfpaths = new List<string>();

            Console.WriteLine("Please enter proper name from that service ypou want to clone.");
            var replaceString = Console.ReadLine();
            Console.WriteLine("Please enter proper name of service ypou want to cloned.");
            var withwhatyouwhattoreplace = Console.ReadLine();
  
            
           
            foreach (string s in Directory.GetFiles(@"C:\\Projects\\exxat-team\\apps\\"+serviceName+"-Service\\", "*" + replaceString + "*.cs", SearchOption.AllDirectories).Select(Path.GetFullPath))
            {
                var booltoproceed = checkpathallowed(s);
                if (booltoproceed)
                {
                     WriteLinesInfile(replaceString, withwhatyouwhattoreplace, s);
                }
            }
            makeSignatureInIocConfiguration(withwhatyouwhattoreplace,serviceName);
            return listOfpaths;
        }

        private static void makeSignatureInIocConfiguration(string withwhatyouwhattoreplace, string serviceName)
        {
            var scopeString=makeServiceString(withwhatyouwhattoreplace, serviceName);
        }

        private static string makeServiceString(string withwhatyouwhattoreplace,string serviceName)
        {
            List<string> WhatIsScope = new List<string>();
            WhatIsScope.Add("AddScoped");
            string scopeWillAdd = "services.AddScoped<IElectronicSignatureService, ElectronicSignatureService>();";
            string scopeWillSingletonAdd = "   services.AddSingleton<IElectronicSignatureRepository>(x =>{var listOfElectronicSignature = Builder<ElectronicSignature>.CreateListOfSize(5).Build().ToList(); return new "+"ElectronicSignatureInMemoryRepository(listOfElectronicSignature);"
            +"}); ";

            string replaceTextInIOC = "ElectronicSignature";

            foreach (string s in Directory.GetFiles(@"C:\\Projects\\exxat-team\\apps\\"+serviceName+ "-Service\\", "*" + "IocContainer" + "*.cs", SearchOption.AllDirectories).Where(x=>x.Contains(".Business")).Select(Path.GetFullPath))
            //  .Where(x=>!x.Contains("Models")).Select(Path.GetFullPath))

            {
                var booltoproceed = checkpathallowed(s);
                if (booltoproceed)
                {
                    List<string> text = System.IO.File.ReadAllLines(s).ToList();
                    var endlinenumber = 0;
                    for(var i=0;i<text.Count;i++)
                    {
                        // Console.WriteLine(textline);

                        if (text[i].IndexOf(";") > 0)
                        {
                            endlinenumber = i+1;
                        }
                    }

                    text[endlinenumber] = text[endlinenumber] +"#region Zeal\n"+ scopeWillAdd.Replace(replaceTextInIOC, withwhatyouwhattoreplace) + scopeWillSingletonAdd.Replace(replaceTextInIOC, withwhatyouwhattoreplace)+"\n#endregion";
                    System.IO.File.WriteAllLines(s, text);
                    Console.WriteLine(endlinenumber);

                }

            }
            return scopeWillAdd;
        }

        public static List<string> findAllPathWhichContainNameandDelete(String ServiceName)
        {
            List<string> listOfpaths = new List<string>();
            Console.WriteLine("Please enter proper name from service you want to delete.");
            var deleteService = Console.ReadLine();
           
            foreach (string s in Directory.GetFiles(@"C:\Projects\exxat-team\apps\\"+ServiceName+"-Service\\", "*" + deleteService + "*.cs", SearchOption.AllDirectories).Select(Path.GetFullPath))
            {
                var booltoproceed = checkpathallowed(s);
                if (booltoproceed)
                {
                    File.Delete(s);
                    Console.WriteLine(s + "\n deleted" + "\n");
                }

            }
            return listOfpaths;
        }
        
        private static bool checkpathallowed(string s)
        {
            List<string> excludePathIdetifier = new List<string>();
            excludePathIdetifier.Add(".BL.Test");
           // excludePathIdetifier.Add(".Data.Test");
           // excludePathIdetifier.Add(".Data");
            excludePathIdetifier.Add(".Entity");
            foreach(var i in excludePathIdetifier)
            {
                if (s.Contains(i))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
