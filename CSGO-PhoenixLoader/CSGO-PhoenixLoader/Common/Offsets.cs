using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_PhoenixLoader.Common
{
    public class Offsets
    {
        public const int dwLocalPlayer = 0xDEA964;
        public const int m_VecOrigin = 0x138;
        public const int m_VecViewOffset = 0x108;
        public const int m_lifeState = 0x25F;
        public const int m_iHealth = 0x100;
        public const int m_iTeamNum = 0xF4;
        public const int dwEntityList = 0x4E01024;
        public const int m_bDormant = 0xED;
        public const int m_iGlowIndex = 0x10488;
        public const int dwGlowObjectManager = 0x535BAD0;
        public const int m_clrRender = 0x70;
    }
}
