using UnityEngine;
using System.Collections;

public class Pulley : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform counterweight;
    public Transform wheel;
    public float rotationScale = 90;
    public float maxDistance;
    private Vector3 startingPosition;
    private Vector3 counterweightStartingPosition;
    private float wheelStartingRotation;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        counterweightStartingPosition = counterweight.position;
        wheelStartingRotation = wheel.eulerAngles.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //var pos = rb.position;
        //pos.y = Mathf.Clamp(pos.y, startingPosition.y - maxDistance, startingPosition.y);
        //rb.position = pos;
        //Debug.Log(pos.y + " : " + (startingPosition.y - maxDistance) + " / " + startingPosition.y);
        counterweight.position = counterweightStartingPosition - Vector3.up * (rb.position.y - startingPosition.y);
        wheel.rotation = Quaternion.Euler(new Vector3(0, 0, wheelStartingRotation + (rb.position.y - startingPosition.y) * rotationScale));
    }
}
