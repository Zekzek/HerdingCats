using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interestingThing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!SwarmCenter.Instance.interestingThings.Contains(gameObject))
            SwarmCenter.Instance.interestingThings.Add(gameObject);
    }
}
