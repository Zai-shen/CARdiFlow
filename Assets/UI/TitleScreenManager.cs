using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class TitleScreenManager : VisualElement
{
    VisualElement m_TitleScreen;
    VisualElement m_OptionsScreen;
    VisualElement m_CreditsScreen;
    VisualElement m_TutorialScreen;
    Button m_OptionsButton;
    Button m_OptionsBackButton;
    Button m_OptionsApplyButton;
    Button m_CreditsButton;
    Button m_CreditsBackButton;
    Button m_TutorialButton;
    Button m_TutorialBackButton;
    Button m_ExitButton;

    private List<VisualElement> screens;

    public static string m_SceneName = "CARdiFlow";

    public new class UxmlFactory : UxmlFactory<TitleScreenManager, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription m_StartScene = new UxmlStringAttributeDescription { name = "start-scene", defaultValue = "CARdiFlow" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var sceneName = m_StartScene.GetValueFromBag(bag, cc);

            ((TitleScreenManager)ve).Init(sceneName);
        }
    }

    public TitleScreenManager()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
#endif
            m_TitleScreen = this.Q("TitleScreenDisplay");
            m_OptionsScreen = this.Q("OptionsDisplay");
            m_CreditsScreen = this.Q("CreditsDisplay");
            m_TutorialScreen = this.Q("TutorialDisplay");

            screens = new List<VisualElement>()
            {
                m_TitleScreen,
                m_OptionsScreen,
                m_TutorialScreen,
                m_CreditsScreen
            };

            m_OptionsButton = m_TitleScreen?.Q<Button>("options-button");
            m_OptionsButton.RegisterCallback<ClickEvent>(ev => EnableOptionsScreen());
            m_OptionsButton.clickable.clicked += EnableOptionsScreen;

            m_OptionsBackButton = m_OptionsScreen.Q<Button>("back-button");
            m_OptionsBackButton.RegisterCallback<ClickEvent>(ev => EnableTitleScreen());
            m_OptionsBackButton.clickable.clicked += EnableTitleScreen;

            m_OptionsApplyButton = m_OptionsScreen.Q<Button>("apply-button");
            m_OptionsApplyButton.RegisterCallback<ClickEvent>(ev => { Globals.SETTINGS.Apply(); EnableTitleScreen(); });
            m_OptionsApplyButton.clickable.clicked += () => { Globals.SETTINGS.Apply(); EnableTitleScreen(); };

            m_TutorialButton = m_TitleScreen?.Q<Button>("tutorial-button");
            m_TutorialButton.RegisterCallback<ClickEvent>(ev => EnableTutorialScreen());
            m_TutorialButton.clickable.clicked += EnableTutorialScreen;

            m_TutorialBackButton = m_TutorialScreen.Q<Button>("back-button");
            m_TutorialBackButton.RegisterCallback<ClickEvent>(ev => EnableTitleScreen());
            m_TutorialBackButton.clickable.clicked += EnableTitleScreen;

            m_CreditsButton = m_TitleScreen?.Q<Button>("credits-button");
            m_CreditsButton.RegisterCallback<ClickEvent>(ev => EnableCreditsScreen());
            m_CreditsButton.clickable.clicked += EnableCreditsScreen;

            m_CreditsBackButton = m_CreditsScreen.Q<Button>("back-button");
            m_CreditsBackButton.RegisterCallback<ClickEvent>(ev => EnableTitleScreen());
            m_CreditsBackButton.clickable.clicked += EnableTitleScreen;

            m_ExitButton = m_TitleScreen?.Q<Button>("exit-button");
            m_ExitButton.RegisterCallback<ClickEvent>(ev => ExitApplication());
            m_ExitButton.clickable.clicked += ExitApplication;

#if UNITY_EDITOR
        }
#endif

        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void DisplayOnly(VisualElement ve)
    {
        foreach (VisualElement screen in screens)
        {
            screen.style.display = DisplayStyle.None;
        }
        ve.style.display = DisplayStyle.Flex;
    }

    void EnableTitleScreen()
    {
        DisplayOnly(m_TitleScreen);
    }

    void EnableOptionsScreen()
    {
        DisplayOnly(m_OptionsScreen);
    }

    void EnableCreditsScreen()
    {
        DisplayOnly(m_CreditsScreen);
    }
    void EnableTutorialScreen()
    {
        DisplayOnly(m_TutorialScreen);
    }

    void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    void StartApplication()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
#endif
            SceneManager.LoadSceneAsync(m_SceneName);
#if UNITY_EDITOR
        else
            Debug.Log("Loading: " + m_SceneName);
#endif
    }

    void Init(string sceneName)
    {
        m_SceneName = sceneName;
    }


}
