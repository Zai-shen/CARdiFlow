using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ActivateFlow(int i)
    {
        FlowController.Instance.overrideCurrent = i;
    }

    public void ActivateFlows(int[] flows)
    {

    }
}
