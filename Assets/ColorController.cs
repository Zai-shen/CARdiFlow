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

    [SerializeField] private Color32 blood = new Color32(128, 13, 0, 192);
    [SerializeField] private Color32 red = new Color32(215, 22, 0, 192);
    [SerializeField] private Color32 blue = new Color32(23, 23, 197, 192);
    public ColorMode colorMode = ColorMode.STANDARD;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        SetColors(colorMode);
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

    public void SetColors(ColorMode cMode)
    {
        this.colorMode = cMode;
        for (int i = 0; i < Globals.LEFT_HEART.Length; i++)
        {
            FlowController.Instance.Flows[Globals.LEFT_HEART[i]].
                SetShaderColor(cMode == ColorMode.STANDARD ? blood : red);
        }
        for (int i = 0; i < Globals.RIGHT_HEART.Length; i++)
        {
            FlowController.Instance.Flows[Globals.RIGHT_HEART[i]].
                SetShaderColor(cMode == ColorMode.STANDARD ? blood : blue);
        }
    }
}
