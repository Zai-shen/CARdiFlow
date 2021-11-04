using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : UnitySingleton<ValveController>
{
    public GameObject[] TricuspidValves;
    public GameObject[] PulmonaryValves;
    public GameObject[] MitralValves;
    public GameObject[] AorticValves;

    private GameObject[][] allValves;

    protected override void Awake()
    {
        base.Awake();
        allValves = new GameObject[4][];
        allValves[0] = TricuspidValves;
        allValves[1] = PulmonaryValves;
        allValves[2] = MitralValves;
        allValves[3] = AorticValves;
    }

    public void Activate(int i)
    {
        ResetAll();
        ActivateValves(i);
    }

    public void Activate(int[] indices)
    {
        ResetAll();
        foreach (int i in indices)
        {
            ActivateValves(i);
        }
    }

    private void ActivateValves(int i)
    {
        switch ((Flow)i)
        {
            case Flow.FLOW_S2:
                ToggleValve(allValves[0], true);
                break;
            case Flow.FLOW_S3:
                ToggleValve(allValves[1], true);
                break;
            case Flow.FLOW_S5:
                ToggleValve(allValves[2], true);
                break;
            case Flow.FLOW_S6:
                ToggleValve(allValves[3], true);
                break;
            case Flow.FLOW_S1:
            case Flow.FLOW_S4:
                break;
            default:
                Debug.LogError($"Index {i} out of bounds for valve activation.");
                break;
        }
    }

    public void ResetAll()
    {
        foreach (GameObject[] Valves in allValves)
        {
            Valves[0].SetActive(true);
            Valves[1].SetActive(false);
        }
    }

    private void ToggleValve(GameObject[] valve, bool active)
    {
        valve[0].SetActive(!active);
        valve[1].SetActive(active);
    }
}
