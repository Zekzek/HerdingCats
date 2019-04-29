using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmie : MonoBehaviour
{
    private const float MINIMUM_ATTENTION_SPAN = 8f;
    public new Rigidbody rigidbody;
    private Vector3 sitAtPosition = Vector3.zero;

    private float speed = 1;
    private static float maxVelocity = 15;
    private static float sqrMaxVelocity = maxVelocity * maxVelocity;
    private GameObject chaseTarget;
    private float attentiveLevel = 0f;

    void Update()
    {
        if (Random.value > 0.2f)
            attentiveLevel -= Time.deltaTime;

        if (attentiveLevel <= 0)
            FindInterest();
        else if (chaseTarget != null && chaseTarget.activeSelf)
            MoveTowards(chaseTarget.transform.position);
        else
            chaseTarget = null;
    }

    private void MoveTowards(Vector3 position)
    {
        if (rigidbody.velocity.sqrMagnitude > 1)
            attentiveLevel = MINIMUM_ATTENTION_SPAN;

        Vector3 positionDelta = position - transform.position;
        float distanceModifier = 10 * positionDelta.magnitude;
        rigidbody.velocity += positionDelta * Time.deltaTime * speed * distanceModifier;
        if (rigidbody.velocity.sqrMagnitude > sqrMaxVelocity)
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;

        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            chaseTarget = collider.gameObject;
            attentiveLevel = MINIMUM_ATTENTION_SPAN;
        }
    }

    private void FindInterest()
    {
        List<GameObject> interestingThings = SwarmCenter.Instance.interestingThings;
        if (interestingThings.Count > 0)
        {
            int index = Random.Range(0, interestingThings.Count);
            chaseTarget = interestingThings[index];
            attentiveLevel = MINIMUM_ATTENTION_SPAN;
        }
    }
}