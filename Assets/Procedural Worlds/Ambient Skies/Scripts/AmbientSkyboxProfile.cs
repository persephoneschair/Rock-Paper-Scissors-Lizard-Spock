﻿//Copyright © 2019 Procedural Worlds Pty Limited. All Rights Reserved.
using UnityEngine;
#if UNITY_2019_1_OR_NEWER
using UnityEngine.Rendering;
#elif UNITY_2018_3_OR_NEWER
using UnityEngine.Experimental.Rendering;
#endif
#if HDPipeline && !UNITY_2019_3_OR_NEWER
using UnityEngine.Experimental.Rendering.HDPipeline;
#elif HDPipeline && UNITY_2019_3_OR_NEWER
using UnityEngine.Rendering.HighDefinition;
#endif

namespace AmbientSkies
{
    /// <summary>
    /// Settings for an ambient sky system
    /// </summary>
    [System.Serializable]
    public class AmbientSkyboxProfile
    {
        #region Skybox Default Settings
        //Skybox settings
        public string name;
        public string assetName;
        public int profileIndex;
        public float lightRotation;
        public bool isPWProfile;
        public string postProcessingAssetName;
        public Cubemap creationHDRIAsset;
        public int creationMatchPostProcessing;

        //Default skybox settings
        [Header("Main Settings")]
        public bool defaultShowSettings = false;
        public AmbientSkiesConsts.AmbientMode defaultAmbientMode = AmbientSkiesConsts.AmbientMode.Skybox;
        public AmbientSkiesConsts.VolumeFogType defaultFogType = AmbientSkiesConsts.VolumeFogType.Volumetric;
        [Header("Skybox Settings")]
        public Cubemap defaultHDRISkybox;
        public Color defaultSkyboxTint = Color.gray;
        public Cubemap defaultCustomSkybox;
        public bool defaultIsProceduralSkybox;
        public float defaultSkyboxExposure = 1f;
        public float defaultSkyboxRotation;
        public float defaultSkyboxPitch;
        public float defaultSkyMultiplier = 1f;
        [Header("Sun Settings")]
        public float defaultShadowStrength = 1f;
        public float defaultIndirectLightMultiplier = 1f;
        public Color defaultSunColor = Color.white;
        public Vector3 defaultSunRotation = new Vector3(135f, 0f, 0f);
        public float defaultSunIntensity = 1f;
        public float defaultLWRPSunIntensity = 1f;
        public float defaultHDRPSunIntensity = 1f;
        //Tempature
        public float defaultTemperature = 6500f;
        public bool defaultUseTempature = false;
        [Header("Built-In/LW Fog Settings")]
        public Color defaultFogColor = Color.gray;
        public float defaultFogDistance = 2048f;
        public float defaultFogDensity = 0.01f;
        public float defaultNearFogDistance = 10f;
        [Header("Horizon Settings")]
        public bool defaultScaleHorizonObjectWithFog = true;
        public bool defaultHorizonSkyEnabled = true;
        public float defaultHorizonScattering = 1f;
        public float defaultHorizonFogDensity = 1f;
        public float defaultHorizonFalloff = 0.5f;
        public float defaultHorizonBlend = 1f;
        public Vector3 defaultHorizonSize = new Vector3(2048f, 2500f, 2048);       
        public bool defaultFollowPlayer = true;
        public Vector3 defaultHorizonPosition;
        public float defaultHorizonUpdateTime = 10f;
        [Header("Built-In/LW Shadow Settings")]
        public float defaultShadowDistance = 150;
        public AmbientSkiesConsts.HDShadowQuality defaultShadowQuality = AmbientSkiesConsts.HDShadowQuality.Resolution2048;
        public AmbientSkiesConsts.ShadowCascade defaultCascadeCount = AmbientSkiesConsts.ShadowCascade.CascadeCount4;
        public ShadowmaskMode defaultShadowmaskMode = ShadowmaskMode.DistanceShadowmask;
        public LightShadows defaultShadowType = LightShadows.Soft;
        public ShadowResolution defaultShadowResolution = ShadowResolution.High;
        public ShadowProjection defaultShadowProjection = ShadowProjection.CloseFit;
        [Header("Ambient Settings")]
        public float defaultSkyboxGroundIntensity = 1f;
        public Color32 defaultSkyColor = new Color32(188, 187, 218, 255);
        public Color32 defaultEquatorColor = new Color32(108, 115, 148, 255);
        public Color32 defaultGroundColor = new Color32(135, 130, 98, 255);
        public Color32 defaultLWRPSkyColor = new Color32(188, 187, 218, 255);
        public Color32 defaultLWRPEquatorColor = new Color32(108, 115, 148, 255);
        public Color32 defaultLWRPGroundColor = new Color32(135, 130, 98, 255);
        public bool defaultEnableSunDisk = true;

        //HDRP
        //Default HD Pipeline Volume Settings
        [Header("HD Environment Volume Settings")]
        public bool defaultIsGlobal = true;
        public float defaultBlendDistance = 0f;
        public float defaultWeight = 1f;
        public int defaultPriority = 0;
        [Header("HD Skybox Settings")]
        public float defaultHDRIExposure = 0f;
        public float defaultHDRIMultiplier = 1f;
        public AmbientSkiesConsts.EnvironementSkyUpdateMode defaultHDRIUpdateMode = AmbientSkiesConsts.EnvironementSkyUpdateMode.OnChanged;
        public float defaultHDRIRotation = 0f;
        [Header("HD Shadow Settings")]
        public float defaultMaxShadowDistance = 700f;
        public float defaultCascadeSplit1 = 0.05f;
        public float defaultCascadeSplit2 = 0.15f;
        public float defaultCascadeSplit3 = 0.3f;
        public bool defaultUseContactShadows = true;
        public float defaultContactShadowsLength = 0.15f;
        public float defaultContactShadowsDistanceScaleFactor = 0.5f;
        public float defaultContactShadowsMaxDistance = 50f;
        public float defaultContactShadowsFadeDistance = 5f;
        public AmbientSkiesConsts.HDRPPostProcessingQuality defaultContactShadowsQuality = AmbientSkiesConsts.HDRPPostProcessingQuality.Medium;
        public int defaultContactShadowsSampleCount = 8;
        public float defaultContactShadowsOpacity = 1f;
        public bool defaultUseMicroShadowing = true;
        [Range(0f, 1f)]
        public float defaultMicroShadowOpacity = 1f;
        [Header("HD Volumetric Fog Settings")]
        public float defaultHDRPFogDistance = 500f;
        public Color32 defaultVolumetricSingleScatteringAlbedo = new Color32(151, 168, 185, 255);
        public float defaultVolumetricBaseFogDistance = 40f;
        public float defaultVolumetricBaseFogHeight = -2f;
        public float defaultVolumetricMeanHeight = 20f;
        public float defaultVolumetricGlobalAnisotropy = 0.4f;
        public float defaultVolumetricGlobalLightProbeDimmer = 0.5f;
        public float defaultVolumetricMaxFogDistance = 1500f;
        public bool defaultVolumetricEnableDistanceFog = true;
        public Color32 defaultVolumetricConstFogColor = new Color32(128, 128, 128, 0);
        public float defaultVolumetricMipFogNear = 0f;
        public float defaultVolumetricMipFogFar = 1000f;
        public float defaultVolumetricMipFogMaxMip = 0.5f;
        public bool defaultVolumetricFilter = true;
        public float defaultVolumetricDistanceRange = 127f;
        public float defaultVolumetricSliceDistributionUniformity = 0f;
        [Header("HD Exponential Fog Settings")]
        public float defaultExponentialFogDensity = 1f;
        public float defaultExponentialBaseHeight = 200f;
        public float defaultExponentialHeightAttenuation = 0.2f;
        public float defaultExponentialMaxFogDistance = 5000f;
        public float defaultExponentialMipFogNear = 0f;
        public float defaultExponentialMipFogFar = 1000f;
        public float defaultExponentialMipFogMaxMip = 0.5f;
        [Header("HD Linear Fog Settings")]
        public float defaultLinearFogDensity = 1f;
        public float defaultLinearHeightStart = 0f;
        public float defaultLinearHeightEnd = 10f;
        public float defaultLinearMaxFogDistance = 5000f;
        public float defaultLinearMipFogNear = 0f;
        public float defaultLinearMipFogFar = 1000f;
        public float defaultLinearMipFogMaxMip = 0.5f;
        public float defaultIndirectDiffuseIntensity = 1.3f;
        public float defaultIndirectSpecularIntensity = 1f;
        public AmbientSkiesConsts.HDRPAmbientMode defaultHdrpAmbientMode = AmbientSkiesConsts.HDRPAmbientMode.Dynamic;
        [Header("HD Screen Space Reflection Settings")]
        public bool defaultEnableScreenSpaceReflections = true;
        public float defaultScreenEdgeFadeDistance = 0.1f;
        public AmbientSkiesConsts.HDRPPostProcessingQuality defaultScreenSpaceReflectionsQuality = AmbientSkiesConsts.HDRPPostProcessingQuality.Medium;
        public int defaultMaxNumberOfRaySteps = 32;
        public float defaultObjectThickness = 0.01f;
        public float defaultMinSmoothness = 0.9f;
        public float defaultSmoothnessFadeStart = 0.9f;
        public bool defaultReflectSky = true;
        [Header("HD Screen Space Refraction Settings")]
        public bool defaultEnableScreenSpaceRefractions = true;
        public float defaultScreenWeightDistance = 0.1f;
        public bool defaultUseBakingSky = true;
        [Header("HD Density Volume Settings")]
        public bool defaultUseFogDensityVolume = true;
        public Color32 defaultSingleScatteringAlbedo = new Color32(255, 255, 255, 255);
        public float defaultDensityVolumeFogDistance = 200f;
        public Texture3D defaultFogDensityMaskTexture;
        public Vector3 defaultDensityMaskTiling = new Vector3(10f, 0f, 0.2f);
        public Vector3 defaultDensityScale = new Vector3(500f, 500f, 500f);
        [Space(15)]

#if HDPipeline && UNITY_2018_3_OR_NEWER
        public FogColorMode defaultFogColorMode = FogColorMode.SkyColor;
        public SkyIntensityMode defaultHDRISkyIntensityMode = SkyIntensityMode.Exposure;
        public VolumeProfile defaultVolumeProfile;
        public FogColorMode defaultVolumetricFogColorMode = FogColorMode.SkyColor;
#endif
        #endregion

        #region Skybox Current Variables
        //Current skybox settings
        public bool showSettings = false;
        public Color skyboxTint = Color.gray;
        public Cubemap customSkybox;

        //Custom Procedural
        public Color customSkyboxTint = Color.white;
        public float customSkyboxExposure = 0.8f;
        public float customSkyboxRotation = 0f;
        public float customSkyboxPitch = 133f;
        public float customProceduralSunSize = 0.25f;
        public float customProceduralSunSizeConvergence = 10f;
        public float customProceduralAtmosphereThickness = 0.9f;
        public Color customProceduralGroundColor = Color.black;
        public bool customEnableSunDisk = true;
        
        [Range(0f, 3f)]
        public float skyboxExposure = 1f;
        [Range(0f, 3f)]
        public float skyboxGroundIntensity = 1f;
        [Range(0f, 360f)]
        public float skyboxRotation;
        public float skyboxPitch;
        public float skyMultiplier = 1f;

        public float shadowStrength = 1f;
        public float indirectLightMultiplier = 1f;
        public Color sunColor = Color.white;
        public float shadowDistance = 150;
        public Vector3 sunRotation = new Vector3(135f, 0f, 0f);
        public bool isProceduralSkybox;
        public float sunIntensity = 1f;
        public float LWRPSunIntensity = 1f;
        public float HDRPSunIntensity = 1f;
        //Tempature
        public float temperature = 6500f;
        public bool useTempature = false;
        public Color fogColor = Color.gray;
        public float fogDistance = 2048f;
        public float nearFogDistance = 10f;
        public float fogDensity = 0.01f;

        public bool includeSunInBaking = true;
        public bool horizonSkyEnabled = true;

        public bool scaleHorizonObjectWithFog = true;
        public float horizonScattering = 1f;
        public float horizonFogDensity = 1f;
        public float horizonFalloff = 0.5f;
        public float horizonBlend = 1f;
        public Vector3 horizonSize = new Vector3(2048f, 2500f, 2048);       
        public bool followPlayer = true;
        public Vector3 horizonPosition;
        public float horizonUpdateTime = 10f;

        public ShadowmaskMode shadowmaskMode = ShadowmaskMode.DistanceShadowmask;
        public LightShadows shadowType = LightShadows.Soft;
        public ShadowResolution shadowResolution = ShadowResolution.High;
        public ShadowProjection shadowProjection = ShadowProjection.CloseFit;

        public AmbientSkiesConsts.AmbientMode ambientMode = AmbientSkiesConsts.AmbientMode.Skybox;
        public Color32 skyColor = new Color32(188, 187, 218, 255);
        public Color32 equatorColor = new Color32(108, 115, 148, 255);
        public Color32 groundColor = new Color32(135, 130, 98, 255);
        public Color32 lwrpSkyColor = new Color32(188, 187, 218, 255);
        public Color32 lwrpEquatorColor = new Color32(108, 115, 148, 255);
        public Color32 lwrpGroundColor = new Color32(135, 130, 98, 255);

        public Cubemap hDRISkybox;
        public AmbientSkiesConsts.HDShadowQuality shadowQuality = AmbientSkiesConsts.HDShadowQuality.Resolution2048;
        public AmbientSkiesConsts.VolumeFogType fogType = AmbientSkiesConsts.VolumeFogType.Volumetric;
        public AmbientSkiesConsts.ShadowCascade cascadeCount = AmbientSkiesConsts.ShadowCascade.CascadeCount4;
        public bool enableSunDisk = true;

        //HDRP
        //Current HD Pipeline Volume Settings
        [Header("Volume General Settings")]
        public bool isGlobal = true;
        public float blendDistance = 0f;
        [Range(0f, 1f)]
        public float weight = 1f;
        public int priority = 0;
        [Header("HDRI Skybox")]
        public float hDRIExposure = 0f;
        public float hDRIMultiplier = 1f;
        [Range(0f, 360f)]
        public AmbientSkiesConsts.EnvironementSkyUpdateMode hDRIUpdateMode = AmbientSkiesConsts.EnvironementSkyUpdateMode.OnChanged;
        public float hDRIRotation = 0f;
        [Header("HD Shadow Settings")]
        public float maxShadowDistance = 700f;
        [Range(0f, 1f)]
        public float cascadeSplit1 = 0.05f;
        [Range(0f, 1f)]
        public float cascadeSplit2 = 0.15f;
        [Range(0f, 1f)]
        public float cascadeSplit3 = 0.3f;
        [Header("Contact Shadows")]
        public bool useContactShadows = true;
        [Range(0f, 1f)]
        public float contactShadowsLength = 0.15f;
        [Range(0f, 1f)]
        public float contactShadowsDistanceScaleFactor = 0.5f;
        public float contactShadowsMaxDistance = 50f;
        public float contactShadowsFadeDistance = 5f;
        public AmbientSkiesConsts.HDRPPostProcessingQuality contactShadowsQuality = AmbientSkiesConsts.HDRPPostProcessingQuality.Medium;
        [Range(0, 64)]
        public int contactShadowsSampleCount = 8;
        public float contactShadowsOpacity = 1f;
        [Header("Micro Shadowing")]
        public bool useMicroShadowing = true;
        [Range(0f, 1f)]
        public float microShadowOpacity = 1f;
        [Header("Volumetric Fog")]
        public float hDRPFogDistance = 500f;
        public Color32 volumetricSingleScatteringAlbedo = new Color32(151, 168, 185, 255);
        public float volumetricBaseFogDistance = 40f;
        public float volumetricBaseFogHeight = -2f;
        public float volumetricMeanHeight = 20f;
        [Range(0f, 1f)]
        public float volumetricGlobalAnisotropy = 0.4f;
        [Range(0f, 1f)]
        public float volumetricGlobalLightProbeDimmer = 0.5f;
        public float volumetricMaxFogDistance = 1500f;
        public bool volumetricEnableDistanceFog = true;
        public Color32 volumetricConstFogColor = new Color32(128, 128, 128, 0);
        public float volumetricMipFogNear = 0f;
        public float volumetricMipFogFar = 1000f;
        [Range(0f, 1f)]
        public float volumetricMipFogMaxMip = 0.5f;
        public bool volumetricFilter = true;
        [Header("Volumetric Light Controller")]
        public float volumetricDistanceRange = 127f;
        [Range(0f, 1f)]
        public float volumetricSliceDistributionUniformity = 0f;
        [Header("Exponetial Fog")]
        public float exponentialBaseHeight = 200f;
        public float exponentialHeightAttenuation = 0.2f;
        public float exponentialMaxFogDistance = 5000f;
        public float exponentialMipFogNear = 0f;
        public float exponentialMipFogFar = 1000f;
        public float exponentialMipFogMaxMip = 0.5f;
        [Header("Linear Fog")]
        public float linearHeightStart = 0f;
        public float linearHeightEnd = 10f;
        public float linearMaxFogDistance = 5000f;
        public float linearMipFogNear = 0f;
        public float linearMipFogFar = 1000f;
        public float linearMipFogMaxMip = 0.5f;
        [Header("Indirect Lighting Controller")]
        public float indirectDiffuseIntensity = 1.3f;
        public float indirectSpecularIntensity = 1f;
        public AmbientSkiesConsts.HDRPAmbientMode hdrpAmbientMode = AmbientSkiesConsts.HDRPAmbientMode.Dynamic;
        [Header("Screen Space Reflections")]
        public bool enableScreenSpaceReflections = true;
        [Range(0f, 1f)]
        public float screenEdgeFadeDistance = 0.1f;
        public AmbientSkiesConsts.HDRPPostProcessingQuality screenSpaceReflectionsQuality = AmbientSkiesConsts.HDRPPostProcessingQuality.Medium;
        public int maxNumberOfRaySteps = 32;
        [Range(0f, 1f)]
        public float objectThickness = 0.01f;
        [Range(0f, 1f)]
        public float minSmoothness = 0.9f;
        [Range(0f, 1f)]
        public float smoothnessFadeStart = 0.9f;
        public bool reflectSky = true;
        [Header("Screen Space Refractions")]
        public bool enableScreenSpaceRefractions = true;
        [Range(0f, 1f)]
        public float screenWeightDistance = 0.1f;
        public bool useBakingSky = true;
        public bool useFogDensityVolume = true;
        public Color32 singleScatteringAlbedo = new Color32(255, 255, 255, 255);
        public float densityVolumeFogDistance = 200f;
        public Texture3D fogDensityMaskTexture;
        public Vector3 densityMaskTiling = new Vector3(10f, 0f, 0.2f);
        public Vector3 densityScale = new Vector3(500f, 500f, 500f);
        public float linearFogDensity = 1f;
        public float exponentialFogDensity = 1f;

#if HDPipeline && UNITY_2018_3_OR_NEWER
        public VolumeProfile volumeProfile;
        public FogColorMode fogColorMode = FogColorMode.SkyColor;
        public SkyIntensityMode hDRISkyIntensityMode = SkyIntensityMode.Exposure;
        public FogColorMode volumetricFogColorMode = FogColorMode.SkyColor;
#endif
        #endregion

        #region Defaults Setup
        /// <summary>
        /// Revert current settings back to default settings
        /// </summary>
        public void RevertToDefault()
        {
            isProceduralSkybox = defaultIsProceduralSkybox;
            showSettings = defaultShowSettings;
            fogColor = defaultFogColor;
            skyboxExposure = defaultSkyboxExposure;
            skyboxGroundIntensity = defaultSkyboxGroundIntensity;
            skyboxRotation = defaultSkyboxRotation;
            skyboxPitch = defaultSkyboxPitch;
            skyboxTint = defaultSkyboxTint;
            customSkybox = defaultCustomSkybox;
            shadowStrength = defaultShadowStrength;
            indirectLightMultiplier = defaultIndirectLightMultiplier;
            sunColor = defaultSunColor;
            shadowDistance = defaultShadowDistance;
            shadowType = defaultShadowType;
            fogDistance = defaultFogDistance;
            fogDensity = defaultFogDensity;
            sunIntensity = defaultSunIntensity;
            LWRPSunIntensity = defaultLWRPSunIntensity;
            HDRPSunIntensity = defaultHDRPSunIntensity;
            sunRotation = defaultSunRotation;
            temperature = defaultTemperature;
            useTempature = defaultUseTempature;

            scaleHorizonObjectWithFog = defaultScaleHorizonObjectWithFog;
            horizonSkyEnabled = defaultHorizonSkyEnabled;
            horizonBlend = defaultHorizonBlend;
            horizonFalloff = defaultHorizonFalloff;
            horizonFogDensity = defaultHorizonFogDensity;
            horizonScattering = defaultHorizonScattering;
            nearFogDistance = defaultNearFogDistance;
            horizonSize = defaultHorizonSize;
            followPlayer = defaultFollowPlayer;
            horizonPosition = defaultHorizonPosition;
            horizonUpdateTime = defaultHorizonUpdateTime;
            ambientMode = defaultAmbientMode;
            skyColor = defaultSkyColor;
            equatorColor = defaultEquatorColor;
            groundColor = defaultGroundColor;
            lwrpSkyColor = defaultLWRPSkyColor;
            lwrpEquatorColor = defaultLWRPEquatorColor;
            lwrpGroundColor = defaultLWRPGroundColor;
            hDRISkybox = defaultHDRISkybox;
            shadowQuality = defaultShadowQuality;
            shadowmaskMode = defaultShadowmaskMode;
            shadowResolution = defaultShadowResolution;
            shadowProjection = defaultShadowProjection;
            cascadeCount = defaultCascadeCount;
            enableSunDisk = defaultEnableSunDisk;
            skyMultiplier = defaultSkyMultiplier;
            hDRPFogDistance = defaultHDRPFogDistance;

            isGlobal = defaultIsGlobal;
            blendDistance = defaultBlendDistance;
            weight = defaultWeight;
            priority = defaultPriority;

            hDRIExposure = defaultHDRIExposure;
            hDRIMultiplier = defaultHDRIMultiplier;
            hDRIUpdateMode = defaultHDRIUpdateMode;
            hDRIRotation = defaultHDRIRotation;
            maxShadowDistance = defaultMaxShadowDistance;
            cascadeSplit1 = defaultCascadeSplit1;
            cascadeSplit2 = defaultCascadeSplit2;
            cascadeSplit3 = defaultCascadeSplit3;
            useContactShadows = defaultUseContactShadows;
            contactShadowsLength = defaultContactShadowsLength;
            contactShadowsDistanceScaleFactor = defaultContactShadowsDistanceScaleFactor;
            contactShadowsMaxDistance = defaultContactShadowsMaxDistance;
            contactShadowsFadeDistance = defaultContactShadowsFadeDistance;
            contactShadowsQuality = defaultContactShadowsQuality;
            contactShadowsSampleCount = defaultContactShadowsSampleCount;
            contactShadowsOpacity = defaultContactShadowsOpacity;

            useMicroShadowing = defaultUseMicroShadowing;
            microShadowOpacity = defaultMicroShadowOpacity;

            volumetricSingleScatteringAlbedo = defaultVolumetricSingleScatteringAlbedo;
            volumetricBaseFogDistance = defaultVolumetricBaseFogDistance;
            volumetricBaseFogHeight = defaultVolumetricBaseFogHeight;
            volumetricMeanHeight = defaultVolumetricMeanHeight;
            volumetricGlobalAnisotropy = defaultVolumetricGlobalAnisotropy;
            volumetricGlobalLightProbeDimmer = defaultVolumetricGlobalLightProbeDimmer;
            volumetricMaxFogDistance = defaultVolumetricMaxFogDistance;

            volumetricEnableDistanceFog = defaultVolumetricEnableDistanceFog;

            volumetricConstFogColor = defaultVolumetricConstFogColor;
            volumetricMipFogNear = defaultVolumetricMipFogNear;
            volumetricMipFogFar = defaultVolumetricMipFogFar;
            volumetricMipFogMaxMip = defaultVolumetricMipFogMaxMip;
            volumetricFilter = defaultVolumetricFilter;
            volumetricDistanceRange = defaultVolumetricDistanceRange;
            volumetricSliceDistributionUniformity = defaultVolumetricSliceDistributionUniformity;
            exponentialBaseHeight = defaultExponentialBaseHeight;
            exponentialHeightAttenuation = defaultExponentialHeightAttenuation;
            exponentialMaxFogDistance = defaultExponentialMaxFogDistance;
            exponentialMipFogNear = defaultExponentialMipFogNear;
            exponentialMipFogFar = defaultExponentialMipFogFar;
            exponentialMipFogMaxMip = defaultExponentialMipFogMaxMip;
            linearHeightStart = defaultLinearHeightStart;
            linearHeightEnd = defaultLinearHeightEnd;
            linearMaxFogDistance = defaultLinearMaxFogDistance;
            linearMipFogNear = defaultLinearMipFogNear;
            linearMipFogFar = defaultLinearMipFogFar;
            linearMipFogMaxMip = defaultLinearMipFogMaxMip;
            indirectDiffuseIntensity = defaultIndirectDiffuseIntensity;
            indirectSpecularIntensity = defaultIndirectSpecularIntensity;
            hdrpAmbientMode = defaultHdrpAmbientMode;
            enableScreenSpaceReflections = defaultEnableScreenSpaceReflections;
            screenEdgeFadeDistance = defaultScreenEdgeFadeDistance;
            screenSpaceReflectionsQuality = defaultScreenSpaceReflectionsQuality;
            maxNumberOfRaySteps = defaultMaxNumberOfRaySteps;
            objectThickness = defaultObjectThickness;
            minSmoothness = defaultMinSmoothness;
            smoothnessFadeStart = defaultSmoothnessFadeStart;
            reflectSky = defaultReflectSky;
            enableScreenSpaceRefractions = defaultEnableScreenSpaceRefractions;
            screenWeightDistance = defaultScreenWeightDistance;
            useBakingSky = defaultUseBakingSky;
            useFogDensityVolume = defaultUseFogDensityVolume;
            singleScatteringAlbedo = defaultSingleScatteringAlbedo;
            densityVolumeFogDistance = defaultDensityVolumeFogDistance;
            fogDensityMaskTexture = defaultFogDensityMaskTexture;
            densityMaskTiling = defaultDensityMaskTiling;
            densityScale = defaultDensityScale;
            linearFogDensity = defaultLinearFogDensity;
            exponentialFogDensity = defaultExponentialFogDensity;
            fogType = defaultFogType;

#if HDPipeline
            
#if UNITY_2018_3_OR_NEWER
            volumeProfile = defaultVolumeProfile;
            volumetricFogColorMode = defaultVolumetricFogColorMode;
            fogColorMode = defaultFogColorMode;
            hDRISkyIntensityMode = defaultHDRISkyIntensityMode;            
#endif          
            
#endif
        }

        /// <summary>
        /// Save current settings to default settings
        /// </summary>
        public void SaveCurrentToDefault()
        {
            defaultIsProceduralSkybox = isProceduralSkybox;
            defaultShowSettings = showSettings;
            defaultSkyboxExposure = skyboxExposure;
            defaultSkyboxGroundIntensity = skyboxGroundIntensity;
            defaultSkyboxRotation = skyboxRotation;
            defaultSkyboxPitch = skyboxPitch;
            defaultSkyboxTint = skyboxTint;
            defaultCustomSkybox = customSkybox;
            defaultSunColor = sunColor;
            defaultShadowDistance = shadowDistance;
            defaultShadowType = shadowType;
            defaultFogColor = fogColor;
            defaultFogDistance = fogDistance;
            defaultFogDensity = fogDensity;
            defaultSunIntensity = sunIntensity;
            defaultLWRPSunIntensity = LWRPSunIntensity;
            defaultHDRPSunIntensity = HDRPSunIntensity;
            defaultShadowStrength = shadowStrength;
            defaultIndirectLightMultiplier = indirectLightMultiplier;
            defaultSunRotation = sunRotation;
            defaultTemperature = temperature;
            defaultUseTempature = useTempature;

            defaultScaleHorizonObjectWithFog = scaleHorizonObjectWithFog;
            defaultHorizonSkyEnabled = horizonSkyEnabled;
            defaultHorizonBlend = horizonBlend;
            defaultHorizonFalloff = horizonFalloff;
            defaultHorizonFogDensity = horizonFogDensity;
            defaultHorizonScattering = horizonScattering;
            defaultNearFogDistance = nearFogDistance;
            defaultHorizonSize = horizonSize;
            defaultFollowPlayer = followPlayer;
            defaultHorizonPosition = horizonPosition;
            defaultHorizonUpdateTime = horizonUpdateTime;
            defaultAmbientMode = ambientMode;
            defaultSkyColor = skyColor;
            defaultEquatorColor = equatorColor;
            defaultGroundColor = groundColor;
            defaultLWRPSkyColor = lwrpSkyColor;
            defaultLWRPEquatorColor = lwrpEquatorColor;
            defaultLWRPGroundColor = lwrpGroundColor;
            defaultHDRISkybox = hDRISkybox;
            defaultShadowQuality = shadowQuality;
            defaultShadowmaskMode = shadowmaskMode;
            defaultShadowQuality = shadowQuality;
            defaultShadowResolution = shadowResolution;
            defaultShadowProjection = shadowProjection;
            defaultCascadeCount = cascadeCount;
            defaultEnableSunDisk = enableSunDisk;
            defaultSkyMultiplier = skyMultiplier;

            defaultHDRPFogDistance = hDRPFogDistance;
            defaultIsGlobal = isGlobal;
            defaultBlendDistance = blendDistance;
            defaultWeight = weight;
            defaultPriority = priority;
            defaultFogType = fogType;

            defaultHDRIExposure = hDRIExposure;
            defaultHDRIMultiplier = hDRIMultiplier;
            defaultHDRIUpdateMode = hDRIUpdateMode;
            defaultHDRIRotation = hDRIRotation;
            defaultMaxShadowDistance = maxShadowDistance;
            defaultCascadeSplit1 = cascadeSplit1;
            defaultCascadeSplit2 = cascadeSplit2;
            defaultCascadeSplit3 = cascadeSplit3;
            defaultUseContactShadows = useContactShadows;
            defaultContactShadowsLength = contactShadowsLength;
            defaultContactShadowsDistanceScaleFactor = contactShadowsDistanceScaleFactor;
            defaultContactShadowsMaxDistance = contactShadowsMaxDistance;
            defaultContactShadowsFadeDistance = contactShadowsFadeDistance;
            defaultContactShadowsQuality = contactShadowsQuality;
            defaultContactShadowsSampleCount = contactShadowsSampleCount;
            defaultContactShadowsOpacity = contactShadowsOpacity;

            defaultUseMicroShadowing = useMicroShadowing;
            defaultMicroShadowOpacity = microShadowOpacity;

            defaultVolumetricSingleScatteringAlbedo = volumetricSingleScatteringAlbedo;
            defaultVolumetricBaseFogDistance = volumetricBaseFogDistance;
            defaultVolumetricBaseFogHeight = volumetricBaseFogHeight;
            defaultVolumetricMeanHeight = volumetricMeanHeight;
            defaultVolumetricGlobalAnisotropy = volumetricGlobalAnisotropy;
            defaultVolumetricGlobalLightProbeDimmer = volumetricGlobalLightProbeDimmer;
            defaultVolumetricMaxFogDistance = volumetricMaxFogDistance;
            defaultVolumetricEnableDistanceFog = volumetricEnableDistanceFog;

            defaultVolumetricConstFogColor = volumetricConstFogColor;
            defaultVolumetricMipFogNear = volumetricMipFogNear;
            defaultVolumetricMipFogFar = volumetricMipFogFar;
            defaultVolumetricMipFogMaxMip = volumetricMipFogMaxMip;
            defaultVolumetricFilter = volumetricFilter;
            defaultExponentialFogDensity = exponentialFogDensity;
            defaultExponentialBaseHeight = exponentialBaseHeight;
            defaultExponentialHeightAttenuation = exponentialHeightAttenuation;
            defaultExponentialMaxFogDistance = exponentialMaxFogDistance;
            defaultExponentialMipFogNear = exponentialMipFogNear;
            defaultExponentialMipFogFar = exponentialMipFogFar;
            defaultExponentialMipFogMaxMip = exponentialMipFogMaxMip;
            defaultLinearFogDensity = linearFogDensity;
            defaultLinearHeightStart = linearHeightStart;
            defaultLinearHeightEnd = linearHeightEnd;
            defaultLinearMaxFogDistance = linearMaxFogDistance;
            defaultLinearMipFogNear = linearMipFogNear;
            defaultLinearMipFogFar = linearMipFogFar;
            defaultLinearMipFogMaxMip = linearMipFogMaxMip;
            defaultVolumetricDistanceRange = volumetricDistanceRange;
            defaultVolumetricSliceDistributionUniformity = volumetricSliceDistributionUniformity;
            defaultIndirectDiffuseIntensity = indirectDiffuseIntensity;
            defaultIndirectSpecularIntensity = indirectSpecularIntensity;
            defaultHdrpAmbientMode = hdrpAmbientMode;
            defaultEnableScreenSpaceReflections = enableScreenSpaceReflections;
            defaultScreenEdgeFadeDistance = screenEdgeFadeDistance;
            defaultScreenSpaceReflectionsQuality = screenSpaceReflectionsQuality;
            defaultMaxNumberOfRaySteps = maxNumberOfRaySteps;
            defaultObjectThickness = objectThickness;
            defaultMinSmoothness = minSmoothness;
            defaultSmoothnessFadeStart = smoothnessFadeStart;
            defaultReflectSky = reflectSky;
            defaultEnableScreenSpaceRefractions = enableScreenSpaceRefractions;
            defaultScreenWeightDistance = screenWeightDistance;
            defaultUseBakingSky = useBakingSky;
            defaultUseFogDensityVolume = useFogDensityVolume;
            defaultSingleScatteringAlbedo = singleScatteringAlbedo;
            defaultDensityVolumeFogDistance = densityVolumeFogDistance;
            defaultFogDensityMaskTexture = fogDensityMaskTexture;
            defaultDensityMaskTiling = densityMaskTiling;
            defaultDensityScale = densityScale;
#if HDPipeline
            
#if UNITY_2018_3_OR_NEWER
            defaultVolumeProfile = volumeProfile;
            defaultFogColorMode = fogColorMode;
            defaultHDRISkyIntensityMode = hDRISkyIntensityMode;
            defaultVolumetricFogColorMode = volumetricFogColorMode;
#endif
            
#endif
        }

        #endregion
    }
}