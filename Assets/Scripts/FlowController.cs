using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowMode
{
    CONTINUOUS = 0,
    SIMULTANEOUS = 1,
    DUAL_CONTINUOUS = 2
}

/// <summary>
/// Handles flows and their de/activation.
/// </summary>
/// <author> Miran https://github.com/Zai-shen Jank </author>
public class FlowController : MonoBehaviour
{
    private static FlowController _instance;
    public static FlowController Instance { get { return _instance; } private set { } }

    [HideInInspector] public AnimateVAT[] Flows;
    private List<AnimateVAT> activeFlows;

    private float duration = 2f;
    private float durationTimer = 0f;

    private int current = 0;

    public FlowMode flowMode = FlowMode.CONTINUOUS;

    private bool isTracked;

    // Start is called before the first frame update
    private void Awake()
    {
        Init();
        InitFlows();

        DisableAll();
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

    /// <summary>
    /// Disables all flows, then enables the flow with index <see cref="current"/>.
    /// </summary>
    private void EnableOnlyCurrent()
    {
        DisableAll();
        if (activeFlows != null && activeFlows.Count != 0)
        {
            activeFlows[current].gameObject.SetActive(true);
        }
    }

    private void EnableOnlyActiveFlows()
    {
        DisableAll();
        if (activeFlows != null && activeFlows.Count != 0)
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
        current = 0;
    }

    public void SetFlowActive(int i)
    {
        if (activeFlows.Contains(Flows[i]))
        {
            return;
        }
        activeFlows.Add(Flows[i]);
    }

    public void SetOnlyFlowsActive(int[] flowArray)
    {
        ResetFlows();
        SetFlowsActive(flowArray);
        current = 0;
    }

    public void SetFlowsActive(int[] flowArray)
    {
        for (int i = 0; i < flowArray.Length; i++)
        {
            if (activeFlows.Contains(Flows[flowArray[i]]))
            {
                return;
            }
            activeFlows.Add(Flows[flowArray[i]]);
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

            if (activeFlows != null && activeFlows.Count != 0)
            {
                HandleFlowing();
            }
        }
    }

    private void HandleFlowing()
    {
        switch (flowMode)
        {
            case FlowMode.DUAL_CONTINUOUS:
                //0&3
                //1&4
                //2&5
                break;
            case FlowMode.SIMULTANEOUS:
                EnableOnlyActiveFlows();
                PlaySimultaneous();
                break;
            case FlowMode.CONTINUOUS:
            default:
                EnableOnlyCurrent();
                Play();
                break;
        }
    }
}
