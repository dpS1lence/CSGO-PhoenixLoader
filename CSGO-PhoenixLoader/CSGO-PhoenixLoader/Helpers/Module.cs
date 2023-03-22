using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_PhoenixLoader.Helpers
{
    public class Module : IDisposable
    {
        public Process Process { get; set; }

        public ProcessModule ProcessModule { get; set; }

        public Module(Process process, ProcessModule processModule)
        {
            Process = process;
            ProcessModule = processModule;
        }

        public void Dispose()
        {
            Process = default;

            ProcessModule.Dispose();
            ProcessModule = default;
        }
    }
}
