﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollower : MonoBehaviour
{
    public Transform target;
    public Transform background;
    public float backgroundFollowFactor;
    public float backgroundDistance;
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + new Vector3(0, 2, -10);
        background.localPosition = transform.position *backgroundFollowFactor + Vector3.forward * backgroundDistance;
    }
}
