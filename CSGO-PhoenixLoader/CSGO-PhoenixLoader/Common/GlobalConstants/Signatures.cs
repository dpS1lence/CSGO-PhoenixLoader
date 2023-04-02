using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSGO_PhoenixLoader.Common.GlobalConstants
{
    public class Signatures
    {
        [JsonProperty("anim_overlays")]
        [JsonPropertyName("anim_overlays")]
        public int AnimOverlays { get; set; }

        [JsonProperty("clientstate_choked_commands")]
        [JsonPropertyName("clientstate_choked_commands")]
        public int ClientstateChokedCommands { get; set; }

        [JsonProperty("clientstate_delta_ticks")]
        [JsonPropertyName("clientstate_delta_ticks")]
        public int ClientstateDeltaTicks { get; set; }

        [JsonProperty("clientstate_last_outgoing_command")]
        [JsonPropertyName("clientstate_last_outgoing_command")]
        public int ClientstateLastOutgoingCommand { get; set; }

        [JsonProperty("clientstate_net_channel")]
        [JsonPropertyName("clientstate_net_channel")]
        public int ClientstateNetChannel { get; set; }

        [JsonProperty("convar_name_hash_table")]
        [JsonPropertyName("convar_name_hash_table")]
        public int ConvarNameHashTable { get; set; }

        [JsonProperty("dwClientState")]
        [JsonPropertyName("dwClientState")]
        public int DwClientState { get; set; }

        [JsonProperty("dwClientState_GetLocalPlayer")]
        [JsonPropertyName("dwClientState_GetLocalPlayer")]
        public int DwClientStateGetLocalPlayer { get; set; }

        [JsonProperty("dwClientState_IsHLTV")]
        [JsonPropertyName("dwClientState_IsHLTV")]
        public int DwClientStateIsHLTV { get; set; }

        [JsonProperty("dwClientState_Map")]
        [JsonPropertyName("dwClientState_Map")]
        public int DwClientStateMap { get; set; }

        [JsonProperty("dwClientState_MapDirectory")]
        [JsonPropertyName("dwClientState_MapDirectory")]
        public int DwClientStateMapDirectory { get; set; }

        [JsonProperty("dwClientState_MaxPlayer")]
        [JsonPropertyName("dwClientState_MaxPlayer")]
        public int DwClientStateMaxPlayer { get; set; }

        [JsonProperty("dwClientState_PlayerInfo")]
        [JsonPropertyName("dwClientState_PlayerInfo")]
        public int DwClientStatePlayerInfo { get; set; }

        [JsonProperty("dwClientState_State")]
        [JsonPropertyName("dwClientState_State")]
        public int DwClientStateState { get; set; }

        [JsonProperty("dwClientState_ViewAngles")]
        [JsonPropertyName("dwClientState_ViewAngles")]
        public int DwClientStateViewAngles { get; set; }

        [JsonProperty("dwEntityList")]
        [JsonPropertyName("dwEntityList")]
        public int DwEntityList { get; set; }

        [JsonProperty("dwForceAttack")]
        [JsonPropertyName("dwForceAttack")]
        public int DwForceAttack { get; set; }

        [JsonProperty("dwForceAttack2")]
        [JsonPropertyName("dwForceAttack2")]
        public int DwForceAttack2 { get; set; }

        [JsonProperty("dwForceBackward")]
        [JsonPropertyName("dwForceBackward")]
        public int DwForceBackward { get; set; }

        [JsonProperty("dwForceForward")]
        [JsonPropertyName("dwForceForward")]
        public int DwForceForward { get; set; }

        [JsonProperty("dwForceJump")]
        [JsonPropertyName("dwForceJump")]
        public int DwForceJump { get; set; }

        [JsonProperty("dwForceLeft")]
        [JsonPropertyName("dwForceLeft")]
        public int DwForceLeft { get; set; }

        [JsonProperty("dwForceRight")]
        [JsonPropertyName("dwForceRight")]
        public int DwForceRight { get; set; }

        [JsonProperty("dwGameDir")]
        [JsonPropertyName("dwGameDir")]
        public int DwGameDir { get; set; }

        [JsonProperty("dwGameRulesProxy")]
        [JsonPropertyName("dwGameRulesProxy")]
        public int DwGameRulesProxy { get; set; }

        [JsonProperty("dwGetAllClasses")]
        [JsonPropertyName("dwGetAllClasses")]
        public int DwGetAllClasses { get; set; }

        [JsonProperty("dwGlobalVars")]
        [JsonPropertyName("dwGlobalVars")]
        public int DwGlobalVars { get; set; }

        [JsonProperty("dwGlowObjectManager")]
        [JsonPropertyName("dwGlowObjectManager")]
        public int DwGlowObjectManager { get; set; }

        [JsonProperty("dwInput")]
        [JsonPropertyName("dwInput")]
        public int DwInput { get; set; }

        [JsonProperty("dwInterfaceLinkList")]
        [JsonPropertyName("dwInterfaceLinkList")]
        public int DwInterfaceLinkList { get; set; }

        [JsonProperty("dwLocalPlayer")]
        [JsonPropertyName("dwLocalPlayer")]
        public int DwLocalPlayer { get; set; }

        [JsonProperty("dwMouseEnable")]
        [JsonPropertyName("dwMouseEnable")]
        public int DwMouseEnable { get; set; }

        [JsonProperty("dwMouseEnablePtr")]
        [JsonPropertyName("dwMouseEnablePtr")]
        public int DwMouseEnablePtr { get; set; }

        [JsonProperty("dwPlayerResource")]
        [JsonPropertyName("dwPlayerResource")]
        public int DwPlayerResource { get; set; }

        [JsonProperty("dwRadarBase")]
        [JsonPropertyName("dwRadarBase")]
        public int DwRadarBase { get; set; }

        [JsonProperty("dwSensitivity")]
        [JsonPropertyName("dwSensitivity")]
        public int DwSensitivity { get; set; }

        [JsonProperty("dwSensitivityPtr")]
        [JsonPropertyName("dwSensitivityPtr")]
        public int DwSensitivityPtr { get; set; }

        [JsonProperty("dwSetClanTag")]
        [JsonPropertyName("dwSetClanTag")]
        public int DwSetClanTag { get; set; }

        [JsonProperty("dwViewMatrix")]
        [JsonPropertyName("dwViewMatrix")]
        public int DwViewMatrix { get; set; }

        [JsonProperty("dwWeaponTable")]
        [JsonPropertyName("dwWeaponTable")]
        public int DwWeaponTable { get; set; }

        [JsonProperty("dwWeaponTableIndex")]
        [JsonPropertyName("dwWeaponTableIndex")]
        public int DwWeaponTableIndex { get; set; }

        [JsonProperty("dwYawPtr")]
        [JsonPropertyName("dwYawPtr")]
        public int DwYawPtr { get; set; }

        [JsonProperty("dwZoomSensitivityRatioPtr")]
        [JsonPropertyName("dwZoomSensitivityRatioPtr")]
        public int DwZoomSensitivityRatioPtr { get; set; }

        [JsonProperty("dwbSendPackets")]
        [JsonPropertyName("dwbSendPackets")]
        public int DwbSendPackets { get; set; }

        [JsonProperty("dwppDirect3DDevice9")]
        [JsonPropertyName("dwppDirect3DDevice9")]
        public int DwppDirect3DDevice9 { get; set; }

        [JsonProperty("find_hud_element")]
        [JsonPropertyName("find_hud_element")]
        public int FindHudElement { get; set; }

        [JsonProperty("force_update_spectator_glow")]
        [JsonPropertyName("force_update_spectator_glow")]
        public int ForceUpdateSpectatorGlow { get; set; }

        [JsonProperty("interface_engine_cvar")]
        [JsonPropertyName("interface_engine_cvar")]
        public int InterfaceEngineCvar { get; set; }

        [JsonProperty("is_c4_owner")]
        [JsonPropertyName("is_c4_owner")]
        public int IsC4Owner { get; set; }

        [JsonProperty("m_bDormant")]
        [JsonPropertyName("m_bDormant")]
        public int MBDormant { get; set; }

        [JsonProperty("m_bIsLocalPlayer")]
        [JsonPropertyName("m_bIsLocalPlayer")]
        public int MBIsLocalPlayer { get; set; }

        [JsonProperty("m_flSpawnTime")]
        [JsonPropertyName("m_flSpawnTime")]
        public int MFlSpawnTime { get; set; }

        [JsonProperty("m_pStudioHdr")]
        [JsonPropertyName("m_pStudioHdr")]
        public int MPStudioHdr { get; set; }

        [JsonProperty("m_pitchClassPtr")]
        [JsonPropertyName("m_pitchClassPtr")]
        public int MPitchClassPtr { get; set; }

        [JsonProperty("m_yawClassPtr")]
        [JsonPropertyName("m_yawClassPtr")]
        public int MYawClassPtr { get; set; }

        [JsonProperty("model_ambient_min")]
        [JsonPropertyName("model_ambient_min")]
        public int ModelAmbientMin { get; set; }

        [JsonProperty("set_abs_angles")]
        [JsonPropertyName("set_abs_angles")]
        public int SetAbsAngles { get; set; }

        [JsonProperty("set_abs_origin")]
        [JsonPropertyName("set_abs_origin")]
        public int SetAbsOrigin { get; set; }
    }
}
