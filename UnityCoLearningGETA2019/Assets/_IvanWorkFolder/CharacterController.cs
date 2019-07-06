using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;

    private float deadzone = 0.1f;

    private bool isOnGround;

    public float horizontalMaxSpeed;
    //public float verticalMaxSpeed;
    public float horizontalGroundForce;
    public float horizontalAirForce;
    public float jumpImpulse;

    public LayerMask groundLayers;

    public int size = 1;
    public int minSize = 1;
    public int maxSize = 5;

    public float massChange = 1;
    public float sizeScaleChange = 1;
    //public float heightChange;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the bottom of the figure overlaps with the ground

        isOnGround = Physics2D.OverlapArea(
                        new Vector2(transform.position.x - 0.25f, transform.position.y + 0.1f),
                        new Vector2(transform.position.x + 0.25f, transform.position.y - 0.1f),
                    groundLayers); 


        var horizontalInput = Input.GetAxis("Horizontal");

        Vector2 impulse = Vector2.zero;
        Vector2 force = Vector2.zero;

        if (Mathf.Sign(horizontalInput) * rb.velocity.x <= horizontalMaxSpeed)
        {
            //Use a deadzone for controllers
            if (Mathf.Abs(horizontalInput) > deadzone)
            {
                horizontalInput = horizontalInput > 0 ?
                    (horizontalInput - deadzone) / (1f - deadzone) :
                    (horizontalInput + deadzone) / (1f - deadzone);

                force.x =  horizontalInput * Time.deltaTime * (isOnGround ? horizontalGroundForce : horizontalAirForce);
            }
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (isOnGround || ChangeSize(-1)) {
                force.y = jumpImpulse;
            }
        }

        if (Input.GetButtonDown("Absorb")){
            ChangeSize(1);
        }


        //rb.AddForce(impulse, ForceMode2D.Impulse);
        rb.AddForce(force*rb.mass, ForceMode2D.Impulse);


    }

    private bool ChangeSize(int sizechange)
    {
        Debug.Log(new Vector2(transform.position.x - col.size.x * 0.5f * (transform.localScale.x + sizechange * sizeScaleChange), transform.position.y + col.size.y * (transform.localScale.y + sizechange * sizeScaleChange))
            + "-" +
            new Vector2(transform.position.x + col.size.x * (transform.localScale.x + sizechange * sizeScaleChange), transform.position.y + 0.1f));
        //Don't shrink or Grow past the minimum size;
        if (size + sizechange >= minSize &&
            size + sizechange <= maxSize &&
            //If you're growing, check if there's room
            (sizechange < 0 || !Physics2D.OverlapArea(
            new Vector2(transform.position.x - col.size.x * 0.5f * (transform.localScale.x + sizechange * sizeScaleChange), transform.position.y + col.size.y * (transform.localScale.y + sizechange * sizeScaleChange)),
            new Vector2(transform.position.x + col.size.x * (transform.localScale.x + sizechange * sizeScaleChange), transform.position.y + 0.1f),
            groundLayers))){

            size += sizechange;
            rb.mass += sizechange;
            transform.localScale += Vector3.one * sizechange * sizeScaleChange;
            return true;
        }
        else
        {
            //TODO Some sort of feedback?
            return false;
        }

    }
}
