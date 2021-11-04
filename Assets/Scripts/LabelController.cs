using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LabelDisplay
{
    NONE = 0,
    SPARSE = 1,
    MODERATE = 2,
    PRECISE = 3
}

public class LabelController : UnitySingleton<LabelController>
{
    private List<GameObject> labels_sparse;
    private List<GameObject> labels_moderate;
    private List<GameObject> labels_precise;

    public LabelDisplay labelDisplay = LabelDisplay.NONE;

    protected override void Awake()
    {
        base.Awake();
        InitLabels();

        ShowLabels(labelDisplay);
    }

    private void InitLabels()
    {
        labels_sparse = new List<GameObject>();
        labels_moderate = new List<GameObject>();
        labels_precise = new List<GameObject>();

        Transform[] flowParents = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            flowParents[i] = transform.GetChild(i);
        }

        foreach (Transform parent in flowParents)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject temp = parent.GetChild(i).gameObject;
                switch (temp.tag)
                {
                    case "label_sparse":
                        labels_sparse.Add(temp);
                        break;
                    case "label_moderate":
                        labels_moderate.Add(temp);
                        break;
                    case "label_precise":
                        labels_precise.Add(temp);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void DisableAll()
    {
        ChangeLabelState(labels_sparse, false);
        ChangeLabelState(labels_moderate, false);
        ChangeLabelState(labels_precise, false);
    }

    private void ChangeLabelState(List<GameObject> label_category, bool state)
    {
        if (label_category == null && label_category.Count == 0)
        {
            Debug.Log($"label cat {label_category} was null or 0");
            return;
        }

        foreach (GameObject label in label_category)
        {
            label.SetActive(state);
        }
    }

    private void DisableCurrent()
    {
        switch (this.labelDisplay)
        {
            case LabelDisplay.SPARSE:
                ChangeLabelState(labels_sparse, false);
                break;
            case LabelDisplay.MODERATE:
                ChangeLabelState(labels_moderate, false);
                break;
            case LabelDisplay.PRECISE:
                ChangeLabelState(labels_precise, false);
                break;
            case LabelDisplay.NONE:
            default:
                break;
        }
    }

    public void ShowLabels(LabelDisplay lMode)
    {
        DisableCurrent();
        labelDisplay = lMode;
        switch (lMode)
        {
            case LabelDisplay.SPARSE:
                ChangeLabelState(labels_sparse, true);
                break;
            case LabelDisplay.MODERATE:
                ChangeLabelState(labels_moderate, true);
                break;
            case LabelDisplay.PRECISE:
                ChangeLabelState(labels_precise, true);
                break;
            case LabelDisplay.NONE:
            default:
                break;
        }
    }
}
