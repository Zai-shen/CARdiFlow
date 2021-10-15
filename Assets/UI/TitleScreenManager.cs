using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleScreenManager : VisualElement
{
    VisualElement m_TitleScreen;
    VisualElement m_OptionsScreen;
    Button m_OptionsButton;
    Button m_OptionsBackButton;
    Button m_OptionsApplyButton;
    Button m_ExitButton;

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

            m_OptionsButton = m_TitleScreen?.Q<Button>("options-button");
            m_OptionsButton.RegisterCallback<ClickEvent>(ev => EnableOptionsScreen());
            m_OptionsButton.clickable.clicked += EnableOptionsScreen;

            m_OptionsBackButton = m_OptionsScreen.Q<Button>("back-button");
            m_OptionsBackButton.RegisterCallback<ClickEvent>(ev => EnableTitleScreen());
            m_OptionsBackButton.clickable.clicked += EnableTitleScreen;

            m_OptionsApplyButton = m_OptionsScreen.Q<Button>("apply-button");
            m_OptionsApplyButton.RegisterCallback<ClickEvent>(ev => { Globals.SETTINGS.Apply(); EnableTitleScreen(); });
            m_OptionsApplyButton.clickable.clicked += () => { Globals.SETTINGS.Apply(); EnableTitleScreen(); };

            m_ExitButton = this.Q<Button>("exit-button");
            m_ExitButton.RegisterCallback<ClickEvent>(ev => ExitApplication());
            m_ExitButton.clickable.clicked += ExitApplication;

#if UNITY_EDITOR
        }
#endif

        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void EnableTitleScreen()
    {
        m_TitleScreen.style.display = DisplayStyle.Flex;
        m_OptionsScreen.style.display = DisplayStyle.None;
        m_ExitButton.style.display = DisplayStyle.Flex;
    }

    void EnableOptionsScreen()
    {
        m_TitleScreen.style.display = DisplayStyle.None;
        m_OptionsScreen.style.display = DisplayStyle.Flex;
        m_ExitButton.style.display = DisplayStyle.None;
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
