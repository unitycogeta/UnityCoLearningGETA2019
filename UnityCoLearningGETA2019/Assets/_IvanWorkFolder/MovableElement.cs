using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovableElement : MonoBehaviour
{
    internal Vector3 initialPosition;
    internal Quaternion initialRotation;
    internal Vector3 initialScale;
    internal Rigidbody2D rb;

    internal virtual void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void ResetToInitial()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
        if(rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }

}