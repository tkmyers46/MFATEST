using System;
using System.IO;

namespace TestCommon
{
    public static class FileUtility
    {
        public static string[] lines { get; set; }
        public static string path { get; set; }

        /// <summary>
        /// Accepts a child path to the system folder and returns the lines
        /// in the file
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string[] ReadLinesInFile(string filepath)
        {
            return lines = File.ReadAllLines(GetPathSystemFolder(filepath));
        }

        /// <summary>
        /// Accepts child path to a system folder and adds the line to that file
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="line"></param>
        public static void AddLineToFile(string filepath, string line)
        {
            File.AppendAllText(GetPathSystemFolder(filepath), line);
        }

        /// <summary>
        /// Accepts child path to a file in a system folder and writes all lines
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="lines"></param>
        public static void WriteLinesInFile(string filepath, string[] lines)
        {
            File.WriteAllLines(GetPathSystemFolder(filepath), lines);
        }

        /// <summary>
        /// Accepts a subdirectory path and returns path to system folder
        /// </summary>
        /// <param name="subdirectory"></param>
        /// <returns></returns>
        public static string GetPathSystemFolder(string subdirectory)
        {
            return path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), subdirectory);            
        }

    }
}
