using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : Toggle
{
    public Sprite activatedSprite;

    private Image toggleImage;
    private Image backgroundImage;
    private Sprite toggleImageSprite;

    public Color32 backgroundOff = new Color32(255, 255, 255, 255);
    public Color32 backgroundActivated = new Color32(215, 215, 215, 255);

    protected override void Awake()
    {
        base.Awake();

#if UNITY_EDITOR
        if (Application.isPlaying)
        {
#endif
            backgroundImage = transform.parent.GetChild(transform.GetSiblingIndex() - 2).GetComponent<Image>();
            toggleImage = transform.parent.GetChild(transform.GetSiblingIndex() - 1).GetComponent<Image>();
            toggleImageSprite = toggleImage.sprite;
            if (activatedSprite == null) activatedSprite = toggleImageSprite;
#if UNITY_EDITOR
        }
#endif
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.onValueChanged.AddListener(OnToggleValueChangedImage);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.onValueChanged.RemoveListener(OnToggleValueChangedImage);
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

#if UNITY_EDITOR
        if (Application.isPlaying)
        {
#endif
            Color32 currentColor = this.isOn ? backgroundActivated : backgroundOff;
            Color32 currentInverseColor = this.isOn ? backgroundOff : backgroundActivated;

            switch (state)
            {
                case SelectionState.Normal:
                    backgroundImage.color = currentColor;
                    break;
                case SelectionState.Pressed:
                    backgroundImage.color = currentInverseColor;
                    break;
                case SelectionState.Selected:
                    backgroundImage.color = currentInverseColor;
                    break;
                case SelectionState.Disabled:
                    backgroundImage.color = Color.white;
                    break;
                case SelectionState.Highlighted:
                default:
                    break;
            }
#if UNITY_EDITOR
        }
#endif

    }

    private void OnToggleValueChangedImage(bool isOn)
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
#endif
            if (isOn)
            {
                toggleImage.sprite = activatedSprite;
            }
            else
            {
                toggleImage.sprite = toggleImageSprite;
            }
#if UNITY_EDITOR
        }
#endif
    }

}
