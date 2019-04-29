using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public List<Lock> locks;
    public int numLocksRequired;
    public int swarmSizeBonus;
    public int maxSwarmSize;
    public string destination = "hub";

    private bool Win { get { return locks.FindAll(l => !l.Locked).Count >= numLocksRequired; } }

    void Update()
    {
        if (Win)
        {
            PersistentData.Instance.ChangeSwarmSize(swarmSizeBonus, maxSwarmSize);
            SceneManager.LoadScene(destination);
        }
    }
}
