using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles flows and their de/activation.
/// </summary>
/// <author> Miran https://github.com/Zai-shen Jank </author>
public class FlowController : MonoBehaviour
{
    private static FlowController _instance;
    public static FlowController Instance { get { return _instance; } private set { } }

    private GameObject[] flows;
    private List<GameObject> enabledFlows;

    private float duration = 2f;
    private float durationTimer = 0f;

    public int overrideCurrent = -1;
    private int current = 0;
    private bool forward = true;

    public bool isTracked { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Init();
        InitFlows();

        EnableOnly(0);//Replace with start 0
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
        enabledFlows = new List<GameObject>();
        flows = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            flows[i] = transform.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// Enables the flow with index <paramref name="flowNr"/> and disables all others.
    /// Then plays the animation for selected flow.
    /// </summary>
    /// <param name="flowNr"> Index of flow to animate in <see cref="flows"/> list. </param>
    private void EnableOnly(int flowNr)
    {
        DisableAll();

        GameObject currFlow = flows[flowNr];
        currFlow.SetActive(true);
        Play(currFlow);
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
            flow.GetComponentInChildren<AnimateVAT>().StopAnimation();
            flow.SetActive(false);
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

            HandleFlowing();
        }
    }

    private void HandleFlowing()
    {
        if (overrideCurrent != -1)
        {
            EnableOnly(overrideCurrent);
            forward = !forward;
            return;
        }

        PingPongFlows();
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

        if (current == flows.Length - 1 && forward)
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
