using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowMode
{
    CONTINUOUS,
    SIMULTANEOUS,
    DUAL_CONTINUOUS
}

/// <summary>
/// Handles flows and their de/activation.
/// </summary>
/// <author> Miran https://github.com/Zai-shen Jank </author>
public class FlowController : MonoBehaviour
{
    private static FlowController _instance;
    public static FlowController Instance { get { return _instance; } private set { } }

    private AnimateVAT[] flows;
    private List<AnimateVAT> activeFlows;

    private float duration = 2f;
    private float durationTimer = 0f;

    public int overrideCurrent = -1;
    private int current = 0;
    private bool forward = true;

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
        flows = new AnimateVAT[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            flows[i] = transform.GetChild(i).GetComponentInChildren<AnimateVAT>();
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
        activeFlows[current].gameObject.SetActive(true);
    }

    private void EnableOnlyActiveFlows()
    {
        DisableAll();
        foreach (AnimateVAT flow in activeFlows)
        {
            flow.gameObject.SetActive(true);
        }
    }

    public void SetOnlyFlowActive(int i)
    {
        DisableAll();
        activeFlows.Clear();
        SetFlowActive(i);
        current = 0;
    }

    public void SetFlowActive(int i)
    {
        if (activeFlows.Contains(flows[i]))
        {
            return;
        }
        activeFlows.Add(flows[i]);
    }

    public void SetOnlyFlowsActive(int[] flowArray)
    {
        DisableAll();
        activeFlows.Clear();
        SetFlowsActive(flowArray);
        current = 0;
    }

    public void SetFlowsActive(int[] flowArray)
    {
        for (int i = 0; i < flowArray.Length; i++)
        {
            if (activeFlows.Contains(flows[flowArray[i]]))
            {
                return;
            }
            activeFlows.Add(flows[flowArray[i]]);
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
        foreach (AnimateVAT flow in flows)
        {
            flow.StopAnimation();
            flow.gameObject.SetActive(false);
        }
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

            if (activeFlows.Count == 0)
            {
                return;
            }
            HandleFlowing();
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

    /// <summary>
    /// In/Decrements the <paramref name="current"></paramref> value in alternating as/descending order. Repeats edge cases 2 times.
    /// </summary>
    //private void PingPongFlows()
    //{
    //    if (forward)
    //    {
    //        current++;
    //    }
    //    else
    //    {
    //        current--;
    //    }

    //    EnableOnly(current);

    //    if (current == flows.Length - 1 && forward)
    //    {
    //        forward = !forward;
    //        current++;
    //    }
    //    else if (current == 0 && !forward)
    //    {
    //        forward = !forward;
    //        current--;
    //    }
    //}
}
