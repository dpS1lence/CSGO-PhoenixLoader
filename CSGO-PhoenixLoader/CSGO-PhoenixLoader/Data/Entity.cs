using CSGO_PhoenixLoader.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using CSGO_PhoenixLoader.Data.Raw;
using System.Runtime.InteropServices;

namespace CSGO_PhoenixLoader.Data
{
    public class Entity :
        EntityBase
    {
        public int Index { get; }

        public bool Dormant { get; private set; } = true;

        public Offsets Offsets { get; set; }

        private IntPtr AddressStudioHdr { get; set; }
        
        public studiohdr_t StudioHdr { get; private set; }
        
        public mstudiohitboxset_t StudioHitBoxSet { get; private set; }
        
        public mstudiobbox_t[] StudioHitBoxes { get; }
        
        public mstudiobone_t[] StudioBones { get; }
        
        public Matrix3x4[] BonesMatrices { get; }
        
        public Vector3[] BonesPos { get; }
        
        public (int from, int to)[] Skeleton { get; }

        public int SkeletonCount { get; private set; }

        /// <summary />
        public Entity(int index, Offsets offsets) 
            : base(offsets)
        {
            Index = index;
            Offsets = offsets;
            StudioHitBoxes = new mstudiobbox_t[128];
            StudioBones = new mstudiobone_t[128];
            BonesMatrices = new Matrix3x4[128];
            BonesPos = new Vector3[128];
            Skeleton = new (int, int)[128];
        }

        public override bool IsAlive()
        {
            return base.IsAlive() && !Dormant;
        }
        protected override IntPtr ReadAddressBase(GameProcess gameProcess)
        {
            return gameProcess.ModuleClient.Read<IntPtr>(Offsets.Signatures.DwEntityList + Index * 0x10 /* size */);
        }

        public override bool Update(GameProcess gameProcess)
        {
            if (!base.Update(gameProcess))
            {
                return false;
            }

            Dormant = gameProcess.Process.Read<bool>(AddressBase + Offsets.Signatures.MBDormant);

            if (!IsAlive())
            {
                return true;
            }

            UpdateStudioHdr(gameProcess);
            UpdateStudioHitBoxes(gameProcess);
            UpdateStudioBones(gameProcess);
            UpdateBonesMatricesAndPos(gameProcess);
            UpdateSkeleton();

            return true;
        }

        /// <summary>
        /// Update <see cref="AddressStudioHdr"/> and <see cref="StudioHdr"/>.
        /// </summary>
        private void UpdateStudioHdr(GameProcess gameProcess)
        {
            var addressToAddressStudioHdr = gameProcess.Process.Read<IntPtr>(AddressBase + Offsets.Signatures.MPStudioHdr);
            AddressStudioHdr = gameProcess.Process.Read<IntPtr>(addressToAddressStudioHdr); // deref
            StudioHdr = gameProcess.Process.Read<studiohdr_t>(AddressStudioHdr);
        }

        /// <summary>
        /// Update <see cref="StudioHitBoxSet"/> and <see cref="StudioHitBoxes"/>.
        /// </summary>
        private void UpdateStudioHitBoxes(GameProcess gameProcess)
        {
            var addressHitBoxSet = AddressStudioHdr + StudioHdr.hitboxsetindex;
            StudioHitBoxSet = gameProcess.Process.Read<mstudiohitboxset_t>(addressHitBoxSet);

            // read
            for (var i = 0; i < StudioHitBoxSet.numhitboxes; i++)
            {
                var shit = gameProcess.Process.Read<mstudiobbox_t>
                    (addressHitBoxSet + StudioHitBoxSet.hitboxindex + i * Marshal.SizeOf<mstudiobbox_t>());
                StudioHitBoxes[i] = shit;
            }
        }

        /// <summary>
        /// Update <see cref="StudioBones"/>.
        /// </summary>
        private void UpdateStudioBones(GameProcess gameProcess)
        {
            for (var i = 0; i < StudioHdr.numbones; i++)
            {
                StudioBones[i] = gameProcess.Process.Read<mstudiobone_t>(AddressStudioHdr + StudioHdr.boneindex + i * Marshal.SizeOf<mstudiobone_t>());
            }
        }

        /// <summary>
        /// Update <see cref="BonesMatrices"/> and <see cref="BonesPos"/>.
        /// </summary>
        private void UpdateBonesMatricesAndPos(GameProcess gameProcess)
        {
            var addressBoneMatrix = gameProcess.Process.Read<IntPtr>(AddressBase + Offsets.Netvars.MDwBoneMatrix);
            for (var boneId = 0; boneId < BonesPos.Length; boneId++)
            {
                var matrix = gameProcess.Process.Read<Matrix3x4>(addressBoneMatrix + boneId * Marshal.SizeOf<Matrix3x4>());
                BonesMatrices[boneId] = matrix;
                BonesPos[boneId] = new Vector3(matrix.m30, matrix.m31, matrix.m32);
            }
        }

        /// <summary>
        /// Update <see cref="StudioBones"/>.
        /// </summary>
        private void UpdateSkeleton()
        {
            // get bones to draw
            var skeletonBoneId = 0;
            for (var i = 0; i < StudioHitBoxSet.numhitboxes; i++)
            {
                var hitbox = StudioHitBoxes[i];
                var bone = StudioBones[hitbox.bone];
                if (bone.parent >= 0 && bone.parent < StudioHdr.numbones)
                {
                    // has valid parent
                    Skeleton[skeletonBoneId] = (hitbox.bone, bone.parent);
                    skeletonBoneId++;
                }
            }
            SkeletonCount = skeletonBoneId;
        }

    }
}
