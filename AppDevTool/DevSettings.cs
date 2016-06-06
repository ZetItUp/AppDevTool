using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDevTool
{
    public static class DevSettings
    {
        public const string DEFAULT_COMPILER_ARGUMENTS = "/out:$OUTPUTPATH OutputFile.cs";
        private static string FILENAME = "Settings.txt";
        public static string CompilerPath = string.Empty;
        public static string OutputExeFilePath = @"$DESKTOP\MyApplication.exe";
        public static string CompilerArguments = string.Empty;

        public static string OutputPathToFullPath()
        {
            string path = OutputExeFilePath;

            path = path.Replace("$DESKTOP", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

            return path;
        }

        public static string GetCompilerArguments()
        {
            string args = CompilerArguments;
            if(args == string.Empty)
            {
                args = DEFAULT_COMPILER_ARGUMENTS;
            }

            args = args.Replace("$OUTPUTPATH", OutputPathToFullPath());

            return args;
        }

        public static void LoadSettings()
        {
            if(!File.Exists(FILENAME))
            {
                CompilerArguments = DEFAULT_COMPILER_ARGUMENTS;
                return;
            }

            using (StreamReader sr = new StreamReader(FILENAME))
            {
                int lineNum = 0;

                while(sr.Peek() > 0)
                {
                    switch(lineNum)
                    {
                        case 0:
                            CompilerPath = sr.ReadLine();
                            break;
                        case 1:
                            OutputExeFilePath = sr.ReadLine();
                            break;
                        case 2:
                            CompilerArguments = sr.ReadLine();
                            break;
                        default:
                            MessageBox.Show("Error Stuff...");
                            break;
                    }

                    lineNum++;
                }
            }
        }

        public static void SaveSettings()
        {
            using (StreamWriter sw = new StreamWriter(FILENAME))
            {
                sw.WriteLine(CompilerPath);
                sw.WriteLine(OutputExeFilePath);
                sw.WriteLine(CompilerArguments);
                sw.Flush();
            }
        }
    }
}
