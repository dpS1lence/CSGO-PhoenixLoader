using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Data;
using CSGO_PhoenixLoader.Helpers;
using CSGO_PhoenixLoader.System;
using Timer = System.Threading.Timer;

namespace CSGO_PhoenixLoader.Hacks
{
    public class AntiFlash : ThreadedComponent
    {
        public AntiFlash(GameProcess process)
        {
            GameProcess = process;
        }
        
        private GameProcess GameProcess { get; set; }

        private void Enable()
        {
            var baseAddress = GameProcess.Process.MainModule.BaseAddress.ToInt64();
            var clientState = new IntPtr(baseAddress + 0x588D9C);
            var clanTagOffset = 0x70;

            var clanTags = new string[]
            {
                "",
                "P",
                "Ph",
                "Pho",
                "Phoe",
                "Phoen",
                "Phoeni",
                "Phoenix",
                "Phoenix ",
                "Phoenix L",
                "Phoenix Lo",
                "Phoenix Loa",
                "Phoenix Load",
                "Phoenix Loade",
                "Phoenix Loader",
                "Phoenix Loade",
                "Phoenix Load",
                "Phoenix Loa",
                "Phoenix Lo",
                "Phoenix L",
                "Phoenix ",
                "Phoenix",
                "Phoeni",
                "Phoen",
                "Phoe",
                "Pho",
                "Ph",
                "P",
                ""
            };

            var currentTagIndex = 0;

            while(true)
            {
                // Get the current clan tag
                var currentTag = clanTags[currentTagIndex];

                // Update the clan tag in memory
                var bytes = Encoding.ASCII.GetBytes(currentTag + "\0");
                var clanTagAddress = clientState + clanTagOffset;
                GameProcess.Process.Write<byte[]>(GameProcess.Process.Handle + (int) clanTagAddress, bytes);

                // Increment the index or reset it to 0
                currentTagIndex = (currentTagIndex + 1) % clanTags.Length;
            }
        }

        protected override void FrameAction()
        {
            var clantag = new Thread(Enable);
            clantag.Start();
        }
    }
}
