using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles flows and their de/activation.
/// </summary>
/// <author> Miran https://github.com/Zai-shen Jank </author>
public class FlowController : MonoBehaviour
{
    private List<GameObject> flows;

    private float duration = 2f;
    private float durationTimer = 0f;

    public int overrideCurrent = -1;
    private int current = 0;
    private bool forward = true;

    // Start is called before the first frame update
    void Start()
    {
        flows = new List<GameObject>();
        foreach (Transform child in transform)
        {
            flows.Add(child.gameObject);
        }

        EnableOnly(0);
    }

    /// <summary>
    /// Enables the flow with index <paramref name="flowNr"/> and disables all others.
    /// Then plays the animation for selected flow.
    /// </summary>
    /// <param name="flowNr"> Index of flow to animate in <see cref="flows"/> list. </param>
    private void EnableOnly(int flowNr)
    {
        for (int i = 0; i < flows.Count; i++)
        {
            GameObject currFlow = flows[i];
            if (i != flowNr)
            {
                currFlow.SetActive(false);
            }
            else
            {
                currFlow.SetActive(true);
                Play(currFlow);
            }
        }
    }

    /// <summary>
    /// Searches for <see cref="AnimateVAT"/> component in children, then invokes the animation coroutine.
    /// </summary>
    /// <param name="currFlow"> Parent of the flow to animate. </param>
    private void Play(GameObject currFlow)
    {
        AnimateVAT animator = currFlow.GetComponentInChildren<AnimateVAT>();
        if (animator == null)
        {
            Debug.Log("No AnimateVAT script found for: " + currFlow.name);
            return;
        }

        if (forward)
        {
            animator.PlayAnimation(0f, 1f, 2f);
        }
        else
        {
            animator.PlayAnimation(1f, 0f, 2f);
        }
    }

    /// <summary>
    /// Disables all flows.
    /// </summary>
    private void DisableAll()
    {
        foreach (GameObject flow in flows)
        {
            flow.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        durationTimer += Time.deltaTime;

        if (durationTimer >= duration)
        {
            durationTimer = 0f;


            if (overrideCurrent != -1)
            {
                EnableOnly(overrideCurrent);
                forward = !forward;
                return;
            }

            PingPongFlows();
        }

    }

    /// <summary>
    /// In/Decrements the <paramref name="current"></paramref> value in alternating as/descending order. Repeats edge cases 2 times.
    /// </summary>
    private void PingPongFlows()
    {
        if (forward)
        {
            current++;
        }
        else
        {
            current--;
        }

        EnableOnly(current);

        if (current == flows.Count - 1 && forward)
        {
            forward = !forward;
            current++;
        }
        else if (current == 0 && !forward)
        {
            forward = !forward;
            current--;
        }
    }
}
