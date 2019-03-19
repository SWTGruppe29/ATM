using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class Logger : ILogger
    {
        /// <summary>
        /// Logs the time of the occurence and the involved tags.
        /// Saves file in ATM\ATMUnitTest\bin\Debug
        /// </summary>
        /// <param name="occurenceTime">DateTime parameter of time of occurence</param>
        /// <param name="involvedTags">List of the involved tracks by tag name</param>
        public void LogMessage(DateTime occurenceTime, List<string> involvedTags)
        {
            //Find the path of the project
            string ProjectDirectory = System.AppContext.BaseDirectory;

            //Combine the path and .txt file 
            string fullPath = Path.Combine(ProjectDirectory, "SeparationLog.txt");

            

            //Pass the filepath and filename to the StreamWriter Constructor
            using (StreamWriter writeText = new StreamWriter(fullPath, append: true))
            {
                writeText.WriteLine($"Time of occurence:  " +
                                    $"{occurenceTime.Year}/" +
                                    $"{occurenceTime.Month}/" +
                                    $"{occurenceTime.Day} " +
                                    $"{occurenceTime.Hour}:" +
                                    $"{occurenceTime.Minute}:" +
                                    $"{occurenceTime.Second}." +
                                    $"{occurenceTime.Millisecond}");

                foreach (var tag in involvedTags)
                {
                    writeText.WriteLine($"Track involved - Tag: {tag}");
                }
                writeText.WriteLine();
            }
        }
    }
}
