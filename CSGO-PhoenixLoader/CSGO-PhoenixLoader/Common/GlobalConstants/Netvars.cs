using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSGO_PhoenixLoader.Common.GlobalConstants
{
    public class Netvars
    {
        [JsonProperty("cs_gamerules_data")]
        [JsonPropertyName("cs_gamerules_data")]
        public int CsGamerulesData { get; set; }

        [JsonProperty("m_ArmorValue")]
        [JsonPropertyName("m_ArmorValue")]
        public int MArmorValue { get; set; }

        [JsonProperty("m_Collision")]
        [JsonPropertyName("m_Collision")]
        public int MCollision { get; set; }

        [JsonProperty("m_CollisionGroup")]
        [JsonPropertyName("m_CollisionGroup")]
        public int MCollisionGroup { get; set; }

        [JsonProperty("m_Local")]
        [JsonPropertyName("m_Local")]
        public int MLocal { get; set; }

        [JsonProperty("m_MoveType")]
        [JsonPropertyName("m_MoveType")]
        public int MMoveType { get; set; }

        [JsonProperty("m_OriginalOwnerXuidHigh")]
        [JsonPropertyName("m_OriginalOwnerXuidHigh")]
        public int MOriginalOwnerXuidHigh { get; set; }

        [JsonProperty("m_OriginalOwnerXuidLow")]
        [JsonPropertyName("m_OriginalOwnerXuidLow")]
        public int MOriginalOwnerXuidLow { get; set; }

        [JsonProperty("m_SurvivalGameRuleDecisionTypes")]
        [JsonPropertyName("m_SurvivalGameRuleDecisionTypes")]
        public int MSurvivalGameRuleDecisionTypes { get; set; }

        [JsonProperty("m_SurvivalRules")]
        [JsonPropertyName("m_SurvivalRules")]
        public int MSurvivalRules { get; set; }

        [JsonProperty("m_aimPunchAngle")]
        [JsonPropertyName("m_aimPunchAngle")]
        public int MAimPunchAngle { get; set; }

        [JsonProperty("m_aimPunchAngleVel")]
        [JsonPropertyName("m_aimPunchAngleVel")]
        public int MAimPunchAngleVel { get; set; }

        [JsonProperty("m_angEyeAnglesX")]
        [JsonPropertyName("m_angEyeAnglesX")]
        public int MAngEyeAnglesX { get; set; }

        [JsonProperty("m_angEyeAnglesY")]
        [JsonPropertyName("m_angEyeAnglesY")]
        public int MAngEyeAnglesY { get; set; }

        [JsonProperty("m_bBombDefused")]
        [JsonPropertyName("m_bBombDefused")]
        public int MBBombDefused { get; set; }

        [JsonProperty("m_bBombPlanted")]
        [JsonPropertyName("m_bBombPlanted")]
        public int MBBombPlanted { get; set; }

        [JsonProperty("m_bBombTicking")]
        [JsonPropertyName("m_bBombTicking")]
        public int MBBombTicking { get; set; }

        [JsonProperty("m_bFreezePeriod")]
        [JsonPropertyName("m_bFreezePeriod")]
        public int MBFreezePeriod { get; set; }

        [JsonProperty("m_bGunGameImmunity")]
        [JsonPropertyName("m_bGunGameImmunity")]
        public int MBGunGameImmunity { get; set; }

        [JsonProperty("m_bHasDefuser")]
        [JsonPropertyName("m_bHasDefuser")]
        public int MBHasDefuser { get; set; }

        [JsonProperty("m_bHasHelmet")]
        [JsonPropertyName("m_bHasHelmet")]
        public int MBHasHelmet { get; set; }

        [JsonProperty("m_bInReload")]
        [JsonPropertyName("m_bInReload")]
        public int MBInReload { get; set; }

        [JsonProperty("m_bIsDefusing")]
        [JsonPropertyName("m_bIsDefusing")]
        public int MBIsDefusing { get; set; }

        [JsonProperty("m_bIsQueuedMatchmaking")]
        [JsonPropertyName("m_bIsQueuedMatchmaking")]
        public int MBIsQueuedMatchmaking { get; set; }

        [JsonProperty("m_bIsScoped")]
        [JsonPropertyName("m_bIsScoped")]
        public int MBIsScoped { get; set; }

        [JsonProperty("m_bIsValveDS")]
        [JsonPropertyName("m_bIsValveDS")]
        public int MBIsValveDS { get; set; }

        [JsonProperty("m_bSpotted")]
        [JsonPropertyName("m_bSpotted")]
        public int MBSpotted { get; set; }

        [JsonProperty("m_bSpottedByMask")]
        [JsonPropertyName("m_bSpottedByMask")]
        public int MBSpottedByMask { get; set; }

        [JsonProperty("m_bStartedArming")]
        [JsonPropertyName("m_bStartedArming")]
        public int MBStartedArming { get; set; }

        [JsonProperty("m_bUseCustomAutoExposureMax")]
        [JsonPropertyName("m_bUseCustomAutoExposureMax")]
        public int MBUseCustomAutoExposureMax { get; set; }

        [JsonProperty("m_bUseCustomAutoExposureMin")]
        [JsonPropertyName("m_bUseCustomAutoExposureMin")]
        public int MBUseCustomAutoExposureMin { get; set; }

        [JsonProperty("m_bUseCustomBloomScale")]
        [JsonPropertyName("m_bUseCustomBloomScale")]
        public int MBUseCustomBloomScale { get; set; }

        [JsonProperty("m_clrRender")]
        [JsonPropertyName("m_clrRender")]
        public int MClrRender { get; set; }

        [JsonProperty("m_dwBoneMatrix")]
        [JsonPropertyName("m_dwBoneMatrix")]
        public int MDwBoneMatrix { get; set; }

        [JsonProperty("m_fAccuracyPenalty")]
        [JsonPropertyName("m_fAccuracyPenalty")]
        public int MFAccuracyPenalty { get; set; }

        [JsonProperty("m_fFlags")]
        [JsonPropertyName("m_fFlags")]
        public int MFFlags { get; set; }

        [JsonProperty("m_flC4Blow")]
        [JsonPropertyName("m_flC4Blow")]
        public int MFlC4Blow { get; set; }

        [JsonProperty("m_flCustomAutoExposureMax")]
        [JsonPropertyName("m_flCustomAutoExposureMax")]
        public int MFlCustomAutoExposureMax { get; set; }

        [JsonProperty("m_flCustomAutoExposureMin")]
        [JsonPropertyName("m_flCustomAutoExposureMin")]
        public int MFlCustomAutoExposureMin { get; set; }

        [JsonProperty("m_flCustomBloomScale")]
        [JsonPropertyName("m_flCustomBloomScale")]
        public int MFlCustomBloomScale { get; set; }

        [JsonProperty("m_flDefuseCountDown")]
        [JsonPropertyName("m_flDefuseCountDown")]
        public int MFlDefuseCountDown { get; set; }

        [JsonProperty("m_flDefuseLength")]
        [JsonPropertyName("m_flDefuseLength")]
        public int MFlDefuseLength { get; set; }

        [JsonProperty("m_flFallbackWear")]
        [JsonPropertyName("m_flFallbackWear")]
        public int MFlFallbackWear { get; set; }

        [JsonProperty("m_flFlashDuration")]
        [JsonPropertyName("m_flFlashDuration")]
        public int MFlFlashDuration { get; set; }

        [JsonProperty("m_flFlashMaxAlpha")]
        [JsonPropertyName("m_flFlashMaxAlpha")]
        public int MFlFlashMaxAlpha { get; set; }

        [JsonProperty("m_flLastBoneSetupTime")]
        [JsonPropertyName("m_flLastBoneSetupTime")]
        public int MFlLastBoneSetupTime { get; set; }

        [JsonProperty("m_flLowerBodyYawTarget")]
        [JsonPropertyName("m_flLowerBodyYawTarget")]
        public int MFlLowerBodyYawTarget { get; set; }

        [JsonProperty("m_flNextAttack")]
        [JsonPropertyName("m_flNextAttack")]
        public int MFlNextAttack { get; set; }

        [JsonProperty("m_flNextPrimaryAttack")]
        [JsonPropertyName("m_flNextPrimaryAttack")]
        public int MFlNextPrimaryAttack { get; set; }

        [JsonProperty("m_flSimulationTime")]
        [JsonPropertyName("m_flSimulationTime")]
        public int MFlSimulationTime { get; set; }

        [JsonProperty("m_flTimerLength")]
        [JsonPropertyName("m_flTimerLength")]
        public int MFlTimerLength { get; set; }

        [JsonProperty("m_hActiveWeapon")]
        [JsonPropertyName("m_hActiveWeapon")]
        public int MHActiveWeapon { get; set; }

        [JsonProperty("m_hBombDefuser")]
        [JsonPropertyName("m_hBombDefuser")]
        public int MHBombDefuser { get; set; }

        [JsonProperty("m_hMyWeapons")]
        [JsonPropertyName("m_hMyWeapons")]
        public int MHMyWeapons { get; set; }

        [JsonProperty("m_hObserverTarget")]
        [JsonPropertyName("m_hObserverTarget")]
        public int MHObserverTarget { get; set; }

        [JsonProperty("m_hOwner")]
        [JsonPropertyName("m_hOwner")]
        public int MHOwner { get; set; }

        [JsonProperty("m_hOwnerEntity")]
        [JsonPropertyName("m_hOwnerEntity")]
        public int MHOwnerEntity { get; set; }

        [JsonProperty("m_hViewModel")]
        [JsonPropertyName("m_hViewModel")]
        public int MHViewModel { get; set; }

        [JsonProperty("m_iAccountID")]
        [JsonPropertyName("m_iAccountID")]
        public int MIAccountID { get; set; }

        [JsonProperty("m_iClip1")]
        [JsonPropertyName("m_iClip1")]
        public int MIClip1 { get; set; }

        [JsonProperty("m_iCompetitiveRanking")]
        [JsonPropertyName("m_iCompetitiveRanking")]
        public int MICompetitiveRanking { get; set; }

        [JsonProperty("m_iCompetitiveWins")]
        [JsonPropertyName("m_iCompetitiveWins")]
        public int MICompetitiveWins { get; set; }

        [JsonProperty("m_iCrosshairId")]
        [JsonPropertyName("m_iCrosshairId")]
        public int MICrosshairId { get; set; }

        [JsonProperty("m_iDefaultFOV")]
        [JsonPropertyName("m_iDefaultFOV")]
        public int MIDefaultFOV { get; set; }

        [JsonProperty("m_iEntityQuality")]
        [JsonPropertyName("m_iEntityQuality")]
        public int MIEntityQuality { get; set; }

        [JsonProperty("m_iFOV")]
        [JsonPropertyName("m_iFOV")]
        public int MIFOV { get; set; }

        [JsonProperty("m_iFOVStart")]
        [JsonPropertyName("m_iFOVStart")]
        public int MIFOVStart { get; set; }

        [JsonProperty("m_iGlowIndex")]
        [JsonPropertyName("m_iGlowIndex")]
        public int MIGlowIndex { get; set; }

        [JsonProperty("m_iHealth")]
        [JsonPropertyName("m_iHealth")]
        public int MIHealth { get; set; }

        [JsonProperty("m_iItemDefinitionIndex")]
        [JsonPropertyName("m_iItemDefinitionIndex")]
        public int MIItemDefinitionIndex { get; set; }

        [JsonProperty("m_iItemIDHigh")]
        [JsonPropertyName("m_iItemIDHigh")]
        public int MIItemIDHigh { get; set; }

        [JsonProperty("m_iMostRecentModelBoneCounter")]
        [JsonPropertyName("m_iMostRecentModelBoneCounter")]
        public int MIMostRecentModelBoneCounter { get; set; }

        [JsonProperty("m_iObserverMode")]
        [JsonPropertyName("m_iObserverMode")]
        public int MIObserverMode { get; set; }

        [JsonProperty("m_iShotsFired")]
        [JsonPropertyName("m_iShotsFired")]
        public int MIShotsFired { get; set; }

        [JsonProperty("m_iState")]
        [JsonPropertyName("m_iState")]
        public int MIState { get; set; }

        [JsonProperty("m_iTeamNum")]
        [JsonPropertyName("m_iTeamNum")]
        public int MITeamNum { get; set; }

        [JsonProperty("m_lifeState")]
        [JsonPropertyName("m_lifeState")]
        public int MLifeState { get; set; }

        [JsonProperty("m_nBombSite")]
        [JsonPropertyName("m_nBombSite")]
        public int MNBombSite { get; set; }

        [JsonProperty("m_nFallbackPaintKit")]
        [JsonPropertyName("m_nFallbackPaintKit")]
        public int MNFallbackPaintKit { get; set; }

        [JsonProperty("m_nFallbackSeed")]
        [JsonPropertyName("m_nFallbackSeed")]
        public int MNFallbackSeed { get; set; }

        [JsonProperty("m_nFallbackStatTrak")]
        [JsonPropertyName("m_nFallbackStatTrak")]
        public int MNFallbackStatTrak { get; set; }

        [JsonProperty("m_nForceBone")]
        [JsonPropertyName("m_nForceBone")]
        public int MNForceBone { get; set; }

        [JsonProperty("m_nModelIndex")]
        [JsonPropertyName("m_nModelIndex")]
        public int MNModelIndex { get; set; }

        [JsonProperty("m_nTickBase")]
        [JsonPropertyName("m_nTickBase")]
        public int MNTickBase { get; set; }

        [JsonProperty("m_nViewModelIndex")]
        [JsonPropertyName("m_nViewModelIndex")]
        public int MNViewModelIndex { get; set; }

        [JsonProperty("m_rgflCoordinateFrame")]
        [JsonPropertyName("m_rgflCoordinateFrame")]
        public int MRgflCoordinateFrame { get; set; }

        [JsonProperty("m_szCustomName")]
        [JsonPropertyName("m_szCustomName")]
        public int MSzCustomName { get; set; }

        [JsonProperty("m_szLastPlaceName")]
        [JsonPropertyName("m_szLastPlaceName")]
        public int MSzLastPlaceName { get; set; }

        [JsonProperty("m_thirdPersonViewAngles")]
        [JsonPropertyName("m_thirdPersonViewAngles")]
        public int MThirdPersonViewAngles { get; set; }

        [JsonProperty("m_vecOrigin")]
        [JsonPropertyName("m_vecOrigin")]
        public int MVecOrigin { get; set; }

        [JsonProperty("m_vecVelocity")]
        [JsonPropertyName("m_vecVelocity")]
        public int MVecVelocity { get; set; }

        [JsonProperty("m_vecViewOffset")]
        [JsonPropertyName("m_vecViewOffset")]
        public int MVecViewOffset { get; set; }

        [JsonProperty("m_viewPunchAngle")]
        [JsonPropertyName("m_viewPunchAngle")]
        public int MViewPunchAngle { get; set; }

        [JsonProperty("m_zoomLevel")]
        [JsonPropertyName("m_zoomLevel")]
        public int MZoomLevel { get; set; }
    }
}
