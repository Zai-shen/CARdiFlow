using UnityEngine;
using UnityEngine.UI;

public class ToggleBtnFix : MonoBehaviour
{
    public Color highLightColor = Color.red;
    public Color standardColor = Color.white;
    public bool useImages = true;
    public Sprite onSprite;

    private Toggle toggle;
    private Image toggleImage;
    private Sprite inspectorImageSprite;

    private void Start()
    {
        toggle = GetComponent<Toggle>();

        if (useImages)
        {
            toggleImage = GetComponent<Image>();
            inspectorImageSprite = toggleImage.sprite;
            toggle.onValueChanged.AddListener(OnToggleValueChangedImage);
        }
        else
        {
            toggle.onValueChanged.AddListener(OnToggleValueChangedColor);
        }
    }

    private void OnToggleValueChangedImage(bool isOn)
    {
        if (isOn)
        {
            toggleImage.sprite = onSprite;
        }
        else
        {
            toggleImage.sprite = inspectorImageSprite;
        }
    }

    private void OnToggleValueChangedColor(bool isOn)
    {
        ColorBlock cb = toggle.colors;
        if (isOn)
        {
            cb.normalColor = highLightColor;
            cb.highlightedColor = highLightColor;
        }
        else
        {
            cb.normalColor = standardColor;
            cb.highlightedColor = standardColor;
        }
        toggle.colors = cb;
    }
}
