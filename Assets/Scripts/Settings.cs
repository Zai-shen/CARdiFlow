using System.Collections.Generic;
using UnityEngine;

public enum SettingEnums
{
    DISPLAY_FPS,
    FULL_S,
    VSYNC,
    SCREEN_R,
    ANISOTROPIC_F,
    ANTI_A,
    TEXTURE_Q,
    SHADOW_Q
}

[System.Serializable]
public class Settings
{
    public int[] resolution;
    [System.NonSerialized]public List<string> availableResolutions;
    public bool displayFPS;
    public bool fullScreen;
    public int vSyncCount;//0=off,1=60fps,2=30fps
    public int graphicsLevel;//5>0
    public int anisotropicFiltering;//2>0
    public int antiAliasing; //8>0
    public int textureDownsample;//0=full,1=half,2=quad
    public int shadowResolution; //3>0

    public Settings()
    {
        availableResolutions = new List<string>();
        resolution = new int[2];
    }

    public void InitCurrent()
    {
        this.resolution[0] = Screen.currentResolution.width;
        this.resolution[1] = Screen.currentResolution.height;
        this.vSyncCount = QualitySettings.vSyncCount;
        this.fullScreen = Screen.fullScreen;
        this.graphicsLevel = QualitySettings.GetQualityLevel();
        this.anisotropicFiltering = (int)QualitySettings.anisotropicFiltering;
        this.antiAliasing = QualitySettings.antiAliasing;
        this.textureDownsample = QualitySettings.masterTextureLimit;
        this.shadowResolution = (int)QualitySettings.shadowResolution;
    }

    public void Apply()
    {
        Screen.SetResolution(resolution[0], resolution[1], fullScreen);
        QualitySettings.vSyncCount = vSyncCount;
        QualitySettings.anisotropicFiltering = (AnisotropicFiltering)anisotropicFiltering;
        QualitySettings.antiAliasing = antiAliasing;
        QualitySettings.masterTextureLimit = textureDownsample;
        QualitySettings.shadowResolution = (ShadowResolution)shadowResolution;
        FPSDisplay.Instance?.Show(displayFPS);
    }

    public List<string> GetCurrentAvailableResolutions()
    {
        foreach (var res in Screen.resolutions)
        {
            availableResolutions.Add($"{res.width}x{res.height}@{res.refreshRate}HZ");
        }
        return availableResolutions;
    }

    public override string ToString()
    {
        return base.ToString() + JsonUtility.ToJson(this);
    }
}
