using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandleText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text shown will be formatted using this string.  {0} is replaced with the actual value")]
    private string formatText = "{0} cm";

    private TextMeshProUGUI tmproText;

    private void Start()
    {
        tmproText = GetComponent<TextMeshProUGUI>();
        Slider parentSlider = GetComponentInParent<Slider>();
        parentSlider.onValueChanged.AddListener(HandleValueChanged);
        HandleValueChanged(parentSlider.value);
    }

    private void HandleValueChanged(float value)
    {
        tmproText.text = string.Format(formatText, value);
    }
}
