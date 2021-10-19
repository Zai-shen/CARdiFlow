using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorMode
{
    STANDARD = 0,
    DE_OXYGENATED = 1
}

public class ColorController : MonoBehaviour
{
    private static ColorController _instance;
    public static ColorController Instance { get { return _instance; } private set { } }

    private Color32 blood;
    private Color32 red;
    private Color32 blue;
    public ColorMode colorMode { get; set; }

    private void Awake()
    {
        Init();
        blood = new Color32(128, 13, 0, 192);
        red = new Color32(215, 22, 0, 192);
        blue = new Color32(23, 23, 197, 192);
        colorMode = ColorMode.STANDARD;
    }

    private void Start()
    {
        SetColors();
    }

    private void Init()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetColors()
    {
        for (int i = 0; i < Globals.LEFT_HEART.Length; i++)
        {
            FlowController.Instance.Flows[Globals.LEFT_HEART[i]].
                SetShaderColor(colorMode == ColorMode.STANDARD ? blood : red);
        }
        for (int i = 0; i < Globals.RIGHT_HEART.Length; i++)
        {
            FlowController.Instance.Flows[Globals.RIGHT_HEART[i]].
                SetShaderColor(colorMode == ColorMode.STANDARD ? blood : blue);
        }
    }
}
