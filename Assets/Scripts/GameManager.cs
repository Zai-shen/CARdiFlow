using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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