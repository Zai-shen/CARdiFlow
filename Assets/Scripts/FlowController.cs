using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowMode
{
    SINGLE = 0,
    DUAL_CONTINUOUS = 1
}

/// <summary>
/// Handles flows and their de/activation.
/// </summary>
/// <author> Miran https://github.com/Zai-shen Jank </author>
public class FlowController : UnitySingleton<FlowController>
{
    [HideInInspector] public AnimateVAT[] Flows;
    private List<AnimateVAT> activeFlows;

    private float duration = 2f;
    private float durationTimer = 0f;

    private int current = 0;

    public FlowMode flowMode = FlowMode.SINGLE;

    private bool isTracked;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        InitFlows();

        DisableAll();
    }

    private void InitFlows()
    {
        activeFlows = new List<AnimateVAT>();
        Flows = new AnimateVAT[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            Flows[i] = transform.GetChild(i).GetComponentInChildren<AnimateVAT>();
        }
    }

    public void OnTargetChange(bool tracked)
    {
        isTracked = tracked;

        if (tracked)
        {
            foreach (AnimateVAT flow in activeFlows)
            {
                flow.waiting = false;
            }
            return;
        }

        foreach (AnimateVAT flow in activeFlows)
        {
            flow.waiting = true;
        }
    }

    private bool ActiveFlowsExist() => activeFlows != null && activeFlows.Count != 0;

    /// <summary>
    /// Disables all flows, then enables the flow with index <see cref="current"/>.
    /// </summary>
    private void EnableOnlyCurrent()
    {
        DisableAll();
        if (ActiveFlowsExist())
        {
            activeFlows[current].gameObject.SetActive(true);
        }
    }

    private void EnableOnlyActiveFlows()
    {
        DisableAll();
        if (ActiveFlowsExist())
        {
            foreach (AnimateVAT flow in activeFlows)
            {
                flow.gameObject.SetActive(true);
            }
        }
    }

    public void SetOnlyFlowActive(int i)
    {
        ResetFlows();
        SetFlowActive(i);
        ForceAnimation();
    }

    public void SetFlowActive(int i)
    {
        if (ActiveFlowsExist() && activeFlows.Contains(Flows[i]))
        {
            return;
        }
        activeFlows.Add(Flows[i]);
    }

    public void SetOnlyFlowsActive(int[] flowArray)
    {
        ResetFlows();
        SetFlowsActive(flowArray);
        ForceAnimation();
    }

    public void SetFlowsActive(int[] flowArray)
    {
        for (int i = 0; i < flowArray.Length; i++)
        {
            if (!activeFlows.Contains(Flows[flowArray[i]]))
            {
                activeFlows.Add(Flows[flowArray[i]]);
            }
        }
    }

    private void PlaySimultaneous()
    {
        for (int i = 0; i < activeFlows.Count; i++)
        {
            activeFlows[i].PlayAnimation(0f, 1f, duration);
        }
    }

    private void Play()
    {
        activeFlows[current++].PlayAnimation(0, 1f, duration);
        if (current == activeFlows.Count)
        {
            current = 0;
        }
    }

    /// <summary>
    /// Disables all flows.
    /// </summary>
    private void DisableAll()
    {
        foreach (AnimateVAT flow in Flows)
        {
            flow.StopAnimation();
            flow.gameObject.SetActive(false);
        }
    }

    public void ResetFlows()
    {
        DisableAll();
        activeFlows.Clear();
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTracked)
        {
            return;
        }


        durationTimer += Time.deltaTime;
        if (durationTimer >= duration)
        {
            durationTimer = 0f;

            if (ActiveFlowsExist())
            {
                HandleFlowing();
            }
        }
    }

    private void ForceAnimation()
    {
        durationTimer = duration;
    }

    public void SetFlowMode(FlowMode fMode)
    {
        flowMode = fMode;
        if (fMode == FlowMode.DUAL_CONTINUOUS)
        {
            HandleDualMode();
            ForceAnimation();
        }
    }

    private void HandleDualMode() {
        activeFlows.Clear();

        if (current >= (Flows.Length / 2))
        {
            current = 0;
        }

        activeFlows.Add(Flows[current]);
        activeFlows.Add(Flows[current + (Flows.Length / 2)]);

        current++;
    }

    private void HandleFlowing()
    {
        switch (flowMode)
        {
            case FlowMode.DUAL_CONTINUOUS:
                HandleDualMode();
                EnableOnlyActiveFlows();
                PlaySimultaneous();
                break;
            case FlowMode.SINGLE:
            default:
                EnableOnlyCurrent();
                Play();
                break;
        }
    }
}
