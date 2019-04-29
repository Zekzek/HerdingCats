using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool Locked { get { return sittingSwarmies.Count == 0; } }

    private HashSet<Swarmie> sittingSwarmies = new HashSet<Swarmie>();

    void OnTriggerEnter(Collider collider)
    {
        Swarmie swarmie = collider.gameObject.GetComponent<Swarmie>();
        if (swarmie != null)
        {
            sittingSwarmies.Add(swarmie);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Swarmie swarmie = collider.gameObject.GetComponent<Swarmie>();
        if (swarmie != null)
        {
            sittingSwarmies.Remove(swarmie);
        }
    }

    /*
        void OnMouseDown()
        {
            SwarmCenter.SendSwarmieTo(transform.position);
        }
        */
}