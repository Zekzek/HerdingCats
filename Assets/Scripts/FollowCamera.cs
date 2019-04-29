using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private static FollowCamera instance;
    public Transform target;
    public Vector3 offset;

    public static float DistanceToTarget { get { return instance.offset.magnitude; } }

    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
