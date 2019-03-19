using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM.Classes;

namespace ATM.Interfaces
{
    public class ConsoleSeparationEventArgs
    {
        public List<Track> tracks { get; set; }
        public List<string> conflictList { get; set; }
    }
    public interface IConsolePrinter
    {
        void Print(List<Track> tracks, List<string> conflictTags);
        void ConsoleSeparationDataHandler(object sender, ConsoleSeparationEventArgs args);
    }
}
