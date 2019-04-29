using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData
{
    private static PersistentData instance;
    public static PersistentData Instance { get { return instance == null ? (instance = new PersistentData()) : instance; } }
    private PersistentData() { SwarmSize = 1; }


    public int SwarmSize { get; private set; }

    private void ReduceSwarmSize(int num) { SwarmSize = Mathf.Max(SwarmSize - num, 1); }

    private void IncreaseSwarmSize(int num, int max) { SwarmSize = Mathf.Max(SwarmSize, Mathf.Min(max, SwarmSize + num)); }

    public void ChangeSwarmSize(int num, int max)
    {
        if (num < 0)
            ReduceSwarmSize(-num);
        else
            IncreaseSwarmSize(num, max);
    }
}
