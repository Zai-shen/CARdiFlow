using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : UnitySingleton<SizeController>
{
    private const int initialSize = 12;

    [SerializeField]
    private Vector3 currentSize;

    protected override void Awake()
    {
        base.Awake();
        currentSize = transform.localScale;
    }

    public void ChangeScale(int newSizeInCm)
    {
        currentSize = Vector3.one / initialSize * newSizeInCm;
        transform.localScale = currentSize;
    }
}
