using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
        ActivateOnlyFlows(Globals.LEFT_HEART);
    }

    public void ActivateRight()
    {
        ActivateOnlyFlows(Globals.RIGHT_HEART);
    }

    public void SetColorMode(bool deOxyColor)
    {
        if (deOxyColor)
        {
            SetColorMode(1);
        }
        else
        {
            SetColorMode(0);
        }
    }

    public void SetColorMode(int i)
    {
        SetColorMode((ColorMode)i);
    }

    private void SetColorMode(ColorMode cMode)
    {
        ColorController.Instance.SetColors(cMode);
    }

    public void ResetFlows()
    {
        FlowController.Instance.ResetFlows();
    }

    public void SetLabelMode(int i)
    {
        SetLabelMode((LabelDisplay)i);
    }

    private void SetLabelMode(LabelDisplay lMode)
    {
        LabelController.Instance.ShowLabels(lMode);
    }

    public void SetFlowMode(int i)
    {
        SetFlowMode((FlowMode)i);
    }

    private void SetFlowMode(FlowMode fMode)
    {
        FlowController.Instance.flowMode = fMode;
    }
}
