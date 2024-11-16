using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    //따라갈 타겟
    public Transform target;

    //포지션을 따라갈 것인지
    public bool followPosition;
    //회전을 따라갈 것인지
    public bool followRotation;

    private void Update()
    {
        if (target == null) return;
        if (followPosition) transform.position = target.position;
        if (followPosition) transform.rotation = target.rotation;
    }
}
