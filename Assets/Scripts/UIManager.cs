using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int[] leftHeart = new int[3] {3,4,5};
    private int[] rightHeart = new int[3] {0,1,2};

    public void ActivateFlow(int i)
    {
        FlowController.Instance.SetFlowActive(i);
    }

    public void ActivateFlows(int[] flows)
    {
        FlowController.Instance.SetFlowsActive(flows);
    }

    public void ActivateOnlyFlow(int i)
    {
        FlowController.Instance.SetOnlyFlowActive(i);
    }

    public void ActivateOnlyFlows(int[] flows)
    {
        FlowController.Instance.SetOnlyFlowsActive(flows);
    }

    public void ActivateLeft()
    {
        ActivateOnlyFlows(leftHeart);
    }

    public void ActivateRight()
    {
        ActivateOnlyFlows(rightHeart);
    }
}
