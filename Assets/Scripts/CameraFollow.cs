using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    public float damping;

    private Vector2 velocity = Vector2.zero;

    private void FixedUpdate()
    {
        Vector2 movePosition = new Vector2(target.position.x, target.position.y) + offset;
        Vector2 newPosition = Vector2.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
