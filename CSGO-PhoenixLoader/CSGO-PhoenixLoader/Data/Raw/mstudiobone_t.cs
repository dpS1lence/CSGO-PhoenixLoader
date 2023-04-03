﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;

namespace CSGO_PhoenixLoader.Data.Raw
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct mstudiobone_t
    {
        public int sznameindex;
        public int parent; // parent bone
        public fixed int bonecontroller[6];     // bone controller index, -1 == none
        public Vector3 pos;
        public Quaternion quat;
        public Vector3 rot;
        public Vector3 posscale;
        public Vector3 rotscale;
        public Matrix3x4 poseToBone;
        public Quaternion qAlignment;
        public int flags;
        public int proctype;
        public int procindex;                   // procedural rule
        public int physicsbone;                 // index into physically simulated bone
        public int surfacepropidx;              // index into string tablefor property name
        public int contents;                    // See BSPFlags.h for the contents flags
        public fixed int unused[8];             // remove as appropriat
    }
}
