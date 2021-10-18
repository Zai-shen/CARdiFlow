using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        if (Globals.OPCONTROLS == null)
            Globals.OPCONTROLS = new OPlanControls();

        if (Globals.SETTINGS == null)
        {
            Globals.SETTINGS = SaveSystem.LoadJSON<Settings>("currentSettings");
            if (Globals.SETTINGS == null)
            {
                Globals.SETTINGS = new Settings();
                Globals.SETTINGS.InitCurrent();
                SaveSystem.SaveJSON<Settings>(Globals.SETTINGS, "currentSettings");
            }
            //Globals.SETTINGS.Apply();
        }

        if (SceneManager.GetActiveScene().name.Equals("CARdiFlow") && VuforiaRuntime.Instance.InitializationState == VuforiaRuntime.InitState.NOT_INITIALIZED)
        {
            Debug.Log("COMMAND: Had to init Vuforia manually!");
            VuforiaConfiguration.Instance.Vuforia.DelayedInitialization = false;
            VuforiaRuntime.Instance.InitVuforia();
        }
    }

    private void OnEnable()
    {
        Globals.OPCONTROLS.Player.Enable();
    }

    private void OnDisable()
    {
        Globals.OPCONTROLS.Player.Disable();
    }
}