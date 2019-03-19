using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public class SeparationLogEventArgs : EventArgs
    {
        public List<string> ConflictList { get; set; }
    }

    public interface ILogger
    {
        void LogMessage(List<string> involvedTags);
        void SeparationLogDataHandler(object sender, SeparationLogEventArgs e);
        void SeparationLogDataHandler(object sender, EventArgs e);
    }
}
