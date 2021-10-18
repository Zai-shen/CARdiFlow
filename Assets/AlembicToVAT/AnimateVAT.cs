using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation handler.
/// </summary>
/// <author> Miran https://github.com/Zai-shen Jank </author>
public class AnimateVAT : MonoBehaviour
{
    private Renderer rend;
    private Coroutine vatAnimation;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    /// <summary>
    /// Exposes AnimateFor coroutine.
    /// </summary>
    public void PlayAnimation(float startValue, float endValue, float animationDuration)
    {
        vatAnimation = StartCoroutine(AnimateFor(startValue, endValue, animationDuration));
    }

    /// <summary>
    /// Stops the current animation coroutine.
    /// </summary>
    public void StopAnimation()
    {
        if (vatAnimation != null)
        {
            StopCoroutine(vatAnimation);
        }
    }

    /// <summary>
    /// Linearly interpolates from <paramref name="startValue"/> to <paramref name="endValue"/> 
    /// over the duration of <paramref name="animationDuration"/> and sets the 
    /// renderer.material._Timeposition to the calculated value. This animates the Vertex Animation Texture. 
    /// </summary>
    /// <param name="startValue"> Starting value for time [0..1]. </param>
    /// <param name="endValue"> End value for for time [1..0]. </param>
    /// <param name="animationDuration"> Duration of the animation. </param>
    /// <returns> Coroutine. </returns>
    private IEnumerator AnimateFor(float startValue, float endValue, float animationDuration)
    {
        Debug.Log("Animating: " + transform.name);
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            float timeValue = Mathf.Lerp(startValue, endValue, (elapsedTime / animationDuration));
            rend.material.SetFloat("_Timeposition", timeValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rend.material.SetFloat("_Timeposition", endValue);
    }
}
