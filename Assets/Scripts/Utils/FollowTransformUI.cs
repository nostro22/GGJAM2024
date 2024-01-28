using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransformUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset;

    private void Start()
    {
        if (canvas != null)
        {
            SetParentCanvas(canvas);
        }
    }

    public void SetParentCanvas(Canvas canvas)
    {
        transform.SetParent(canvas.transform);
    }

    private void Update()
    {
        transform.position = Target.position + Offset;
    }
}
