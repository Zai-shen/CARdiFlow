using UnityEngine;
using UnityEngine.UI;

public class ToggleFix : MonoBehaviour
{
    public Sprite onSprite;
    
    private Image toggleImage;
    private Toggle toggle;
    private Sprite inspectorImageSprite;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggleImage = transform.parent.GetChild(transform.GetSiblingIndex()-1).GetComponent<Image>();
        inspectorImageSprite = toggleImage.sprite;

        toggle.onValueChanged.AddListener(OnToggleValueChangedImage);
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
}
