using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Lock> locks;
    public int numLocksRequired;

    private float startHeight;
    private bool Open { get { return locks.FindAll(l => !l.Locked).Count >= numLocksRequired; } }
    private float DesiredHeight { get { return Open ? startHeight - transform.localScale.y : startHeight; } }

    void Start()
    {
        startHeight = transform.position.y;
    }

    void Update()
    {
        if (transform.position.y != DesiredHeight)
        {
            float moveDistance = transform.localScale.y * Time.deltaTime;
            if (transform.position.y < DesiredHeight - moveDistance)
                transform.position = new Vector3(transform.position.x, transform.position.y + moveDistance, transform.position.z);
            else if (transform.position.y > DesiredHeight + moveDistance)
                transform.position = new Vector3(transform.position.x, transform.position.y - moveDistance, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, DesiredHeight, transform.position.z);
        }
    }
}
