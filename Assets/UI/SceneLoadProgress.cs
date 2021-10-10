using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Vuforia;

public class SceneLoadProgress : MonoBehaviour
{
    private VisualElement root;
    private Button startButton;
    private string startButtonInitial;
    private bool isLoading = false;

    private void Start()
    {
        //Grab a reference to the root element that is drawn
        root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-button");

        startButtonInitial = startButton.text;
        
        startButton.RegisterCallback<ClickEvent, string>(Load, TitleScreenManager.m_SceneName);
        startButton.clickable.clicked += Load;
    }

    private void Load()
    {
        Load(new ClickEvent(), TitleScreenManager.m_SceneName);
    }

    private void Load(ClickEvent e, string sceneName)
    {
        if (isLoading)
            return;

        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        isLoading = true;
        yield return null;
        Debug.Log("Loading!");
        //Tell vuforia to init
        VuforiaRuntime.Instance.InitVuforia();

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        
        //Don't let the Scene activate until you allow it to
        //asyncOperation.allowSceneActivation = false;

        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            ///m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            string prog = (asyncOperation.progress * 100) + "%";
            startButton.text = $"{startButtonInitial}: {prog}";

            //// Check if the load has finished
            //if (asyncOperation.progress >= 0.9f)
            //{
            //    //Change the Text to show the Scene is ready
            //    ///m_Text.text = "Press the space bar to continue";
            //    Debug.Log("Press the space bar to continue");
            //    //Wait to you press the space key to activate the Scene
            //    if (Input.GetKeyDown(KeyCode.Space))
            //        //Activate the Scene
            //        asyncOperation.allowSceneActivation = true;
            //}

            yield return null;
        }
    }
}
