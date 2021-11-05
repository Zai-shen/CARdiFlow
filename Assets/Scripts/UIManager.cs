using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void SetHeartSize(float sizeInCm)
    {
        SizeController.Instance.ChangeScale((int)sizeInCm);
    }

    private void ActivateFlow(int i)
    {
        FlowController.Instance.SetFlowActive(i);
    }

    private void ActivateFlows(int[] flows)
    {
        FlowController.Instance.SetFlowsActive(flows);
    }

    private void ActivateOnlyFlow(int i)
    {
        FlowController.Instance.SetOnlyFlowActive(i);
    }

    private void ActivateOnlyFlows(int[] flows)
    {
        FlowController.Instance.SetOnlyFlowsActive(flows);
    }

    public void ActivateRightAtrium(bool active)
    {
        if (active) ActivateOnlyFlow(0); 
    }

    public void ActivateRightVentricle(bool active)
    {
        if (active) ActivateOnlyFlow(1);
    }

    public void ActivatePulmonaryArtery(bool active)
    {
        if (active) ActivateOnlyFlow(2);
    }

    public void ActivateLeftAtrium(bool active)
    {
        if (active) ActivateOnlyFlow(3);
    }

    public void ActivateLeftVentricle(bool active)
    {
        if (active) ActivateOnlyFlow(4);
    }

    public void ActivateAorta(bool active)
    {
        if (active) ActivateOnlyFlow(5);
    }

    public void SetColorMode(bool deOxyColor)
    {
        if (deOxyColor) SetColorMode(ColorMode.DE_OXYGENATED);
        else SetColorMode(ColorMode.STANDARD);
    }

    private void SetColorMode(ColorMode cMode)
    {
        ColorController.Instance.SetColors(cMode);
    }

    public void SetLabelModeNone(bool active)
    {
        if (active) SetLabelMode(LabelDisplay.NONE);
    }

    public void SetLabelModeSparse(bool active)
    {
        if (active) SetLabelMode(LabelDisplay.SPARSE);
    }

    public void SetLabelModeModerate(bool active)
    {
        if (active) SetLabelMode(LabelDisplay.MODERATE);
    }

    public void SetLabelModePrecise(bool active)
    {
        if (active) SetLabelMode(LabelDisplay.PRECISE);
    }

    private void SetLabelMode(LabelDisplay lMode)
    {
        LabelController.Instance.ShowLabels(lMode);
    }

    public void SetFlowModeDual(bool active)
    {
        if (active) SetFlowMode(FlowMode.DUAL_CONTINUOUS);
        else SetFlowMode(FlowMode.SINGLE);
    }

    private void SetFlowMode(FlowMode fMode)
    {
        FlowController.Instance.SetFlowMode(fMode);
    }

    public void ResetFlows(bool active)
    {
        if (active)
        {
            FlowController.Instance.ResetAll();
            ValveController.Instance.ResetAll();
        }
    }
}
