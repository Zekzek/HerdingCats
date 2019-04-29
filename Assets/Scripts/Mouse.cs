using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private const float MINIMUM_ATTENTION_SPAN = 2f;

    public new Rigidbody rigidbody;
    private float speed = 20;
    private static float maxVelocity = 25;
    private static float sqrMaxVelocity = maxVelocity * maxVelocity;
    private Vector3 chosenDirection;
    private float attentiveLevel = 0f;

    void Update()
    {
        if (Random.value > 0.2f)
            attentiveLevel -= Time.deltaTime;
        if (attentiveLevel <= 0)
            PickDirection();
        else
            MoveInDirection(chosenDirection);
    }

    private void MoveInDirection(Vector3 positionDelta)
    {
        rigidbody.velocity += positionDelta * Time.deltaTime * speed;
        if (rigidbody.velocity.sqrMagnitude > sqrMaxVelocity)
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;

        transform.LookAt(transform.position + positionDelta);
    }

    private void PickDirection()
    {
        chosenDirection = new Vector3(2 * Random.value - 1, 0, 2 * Random.value - 1);
        attentiveLevel = MINIMUM_ATTENTION_SPAN;
    }
}
