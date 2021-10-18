using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Reflection;
using System.Linq;

public class OptionsDisplay : VisualElement
{
    public new class UxmlFactory : UxmlFactory<OptionsDisplay, UxmlTraits> { }

    private VisualElement m_OptionsScreen;

    public OptionsDisplay()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        m_OptionsScreen = this;
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        { 
#endif

            Toggle fullScreenToggle = m_OptionsScreen.Q<Toggle>("fullscreen-toggle");
            fullScreenToggle.value = Globals.SETTINGS.fullScreen;
            fullScreenToggle.RegisterValueChangedCallback(x => SetSettingsProperty(
                x.newValue, SettingEnums.FULL_S, Globals.SETTINGS, "fullScreen"));

            Toggle vSyncToggle = m_OptionsScreen.Q<Toggle>("vsync-toggle");
            vSyncToggle.value = Globals.SETTINGS.vSyncCount > 0 ? true : false;
            vSyncToggle.RegisterValueChangedCallback(x => SetSettingsProperty(
                x.newValue, SettingEnums.VSYNC, Globals.SETTINGS, "vSyncCount"));

            HandleDropdown(ddf: m_OptionsScreen.Q<DropdownField>("screen-resolution-dd"),
                options: Globals.SETTINGS.GetCurrentAvailableResolutions(),
                enumType: SettingEnums.SCREEN_R,
                settings: Globals.SETTINGS,
                propertyName: "resolution");

            HandleDropdown(ddf: m_OptionsScreen.Q<DropdownField>("aniso-filter-dd"),
                    options: Enum.GetNames(typeof(AnisotropicFiltering)).ToList(),
                    enumType: SettingEnums.ANISOTROPIC_F,
                    settings: Globals.SETTINGS,
                    propertyName: "anisotropicFiltering");

            HandleDropdown(ddf: m_OptionsScreen.Q<DropdownField>("anti-aliasing-dd"),
                   options: new List<int>() { 0, 2, 4, 8 },
                   enumType: SettingEnums.ANTI_A,
                   settings: Globals.SETTINGS,
                   propertyName: "antiAliasing");

            HandleDropdown(ddf: m_OptionsScreen.Q<DropdownField>("texture-quality-dd"),
                   options: new List<int>() { 0, 1, 2 },
                   enumType: SettingEnums.TEXTURE_Q,
                   settings: Globals.SETTINGS,
                   propertyName: "textureDownsample");

            HandleDropdown(ddf: m_OptionsScreen.Q<DropdownField>("shadow-quality-dd"),
                   options: Enum.GetNames(typeof(ShadowResolution)).ToList(),
                   enumType: SettingEnums.SHADOW_Q,
                   settings: Globals.SETTINGS,
                   propertyName: "shadowResolution");

#if UNITY_EDITOR
        }
#endif

        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    private void HandleDropdown<T>(DropdownField ddf, List<T> options, SettingEnums enumType, Settings settings, string propertyName)
    {
        ddf.choices = FormatChoices(options);
        ddf.index = 0;

        SetCurrentValue(ref ddf, enumType);
        ddf.RegisterValueChangedCallback(x => SetSettingsProperty(x.newValue, enumType, settings, propertyName));
    }

    private void SetCurrentValue(ref DropdownField ddField, SettingEnums enumType)
    {
        switch (enumType)
        {
            case SettingEnums.SCREEN_R:
                ddField.value = Screen.currentResolution.ToString();
                break;
            case SettingEnums.ANISOTROPIC_F:
                ddField.value = ((AnisotropicFiltering)Globals.SETTINGS.anisotropicFiltering).ToString();
                break;
            case SettingEnums.ANTI_A:
                ddField.value = Globals.SETTINGS.antiAliasing.ToString();
                break;
            case SettingEnums.TEXTURE_Q:
                ddField.value = Globals.SETTINGS.textureDownsample.ToString();
                break;
            case SettingEnums.SHADOW_Q:
                ddField.value = ((ShadowResolution)Globals.SETTINGS.shadowResolution).ToString();
                break;
            default:
                break;
        }
    }

    private List<string> FormatChoices<Y>(List<Y> choiceType)
    {
        if (choiceType is List<string>)
        {
            return (from i in choiceType select i.ToString()).ToList();
        }
        else if (choiceType is List<int>)
        {
            return (from i in choiceType select i.ToString()).ToList();
        }

        Debug.LogError($"Unsupported list type: {choiceType}");
        return null;
    }

    private void SetSettingsProperty<T>(T value, SettingEnums enumType, Settings settings, string property)
    {
        Type myType = typeof(Settings);
        FieldInfo myFieldInfo = myType.GetField(property,
            BindingFlags.Public | BindingFlags.Instance);

        switch (enumType)
        {
            case SettingEnums.FULL_S:
                if (bool.TryParse(value.ToString(), out bool boolval))
                    myFieldInfo.SetValue(settings, boolval);
                break;
            case SettingEnums.VSYNC:
                if (bool.TryParse(value.ToString(), out bool boolvall))
                    myFieldInfo.SetValue(settings, boolvall ? 1 : 0);
                break;
            case SettingEnums.SCREEN_R:
                myFieldInfo.SetValue(settings, Utils.ResolutionStringToIntArray(value.ToString()));
                break;
            case SettingEnums.ANISOTROPIC_F:
                if (Enum.TryParse<AnisotropicFiltering>(value.ToString(), out AnisotropicFiltering aniso))
                    myFieldInfo.SetValue(settings, (int)aniso);
                break;
            case SettingEnums.SHADOW_Q:
                if (Enum.TryParse<ShadowResolution>(value.ToString(), out ShadowResolution shadow))
                    myFieldInfo.SetValue(settings, (int)shadow);
                break;
            case SettingEnums.ANTI_A:
            case SettingEnums.TEXTURE_Q:
            default:
                if (int.TryParse(value.ToString(), out int intval))
                    myFieldInfo.SetValue(settings, intval);
                break;
        }
    }
}
