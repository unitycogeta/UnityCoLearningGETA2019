using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    Vector2 impulse = Vector2.zero;
    Vector2 force = Vector2.zero;

    private float deadzone = 0.1f;

    private bool isOnGround;

    public GameController gameController;
    public GameObject ejectedSandPrefab;
    internal Sandpile checkpoint;
    internal bool inRangeOfSandpile;

    public float horizontalMaxSpeed;
    public float upMaxSpeed;
    public float downMaxSpeed;
    //public float verticalMaxSpeed;
    public float horizontalGroundForce;
    public float horizontalAirForce;
    public float jumpImpulse;
    private bool doJump;

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
        if (Input.GetButtonDown("CheckpointReset") && checkpoint != null)
        {
            PerformSoftReset();
            return;
        }

        isOnGround = Physics2D.OverlapArea(
                        new Vector2(transform.position.x - 0.05f * size * sizeScaleChange, transform.position.y + 0.05f * size * sizeScaleChange),
                        new Vector2(transform.position.x + 0.05f * size * sizeScaleChange, transform.position.y - 0.05f * size * sizeScaleChange),
                    groundLayers);


        var horizontalInput = Input.GetAxis("Horizontal");



        //Use a deadzone for controllers
        if (Mathf.Abs(horizontalInput) > deadzone)
        {
            horizontalInput = horizontalInput > 0 ?
                (horizontalInput - deadzone) / (1f - deadzone) :
                (horizontalInput + deadzone) / (1f - deadzone);

            force.x =  horizontalInput * (isOnGround ? horizontalGroundForce : horizontalAirForce);
        }

        


        if (Input.GetButtonDown("Jump"))
        {
            if (!doJump && (isOnGround || ChangeSize(-1))) {
                impulse.y = jumpImpulse;
                doJump = true;
            }
        }

        if (Input.GetButtonDown("Absorb")) {
            if (inRangeOfSandpile && checkpoint != null)
            {
                while(checkpoint.currentPileSize > 0 && ChangeSize(1))
                {
                    checkpoint.DrainPile(1);
                }
            }

            //gameController.allEjectedSand.RemoveAll(e => Vector3.SqrMagnitude(transform.position - e.transform.position) < 1 && ChangeSize(1));

            //var toBeAbsorbed = new List<EjectedSand>();
            foreach (var ejectedSand in gameController.allEjectedSand.ToArray())
            {
                if (ejectedSand.isAbsorable && Vector3.SqrMagnitude(transform.position - ejectedSand.transform.position) < 1 && ChangeSize(1))
                {
                    gameController.allEjectedSand.Remove(ejectedSand);
                    ejectedSand.Absorb();
                }
            }

        }


    }

    public void PerformSoftReset()
    {
        rb.velocity = Vector3.zero;
        transform.position = checkpoint.transform.position;
        ResetSize();
        gameController.PerformCheckpointReset();
        return;
    }

    private void FixedUpdate()
    {
        rb.AddForce(impulse * rb.mass, ForceMode2D.Impulse);
        doJump = false;
        rb.AddForce(force * rb.mass, ForceMode2D.Force);
        
        var clampingVelocity = rb.velocity;
        clampingVelocity.x = Mathf.Clamp(clampingVelocity.x, -horizontalMaxSpeed, +horizontalMaxSpeed);
        clampingVelocity.y = Mathf.Clamp(clampingVelocity.y, -downMaxSpeed, upMaxSpeed);
        rb.velocity = clampingVelocity;

        force = Vector2.zero;
        impulse = Vector2.zero;
    }

    private bool ChangeSize(int sizechange)
    {
        //Debug.Log(new Vector2(transform.position.x - col.size.x * 0.5f * (transform.localScale.x + sizechange * sizeScaleChange), transform.position.y + col.size.y * (transform.localScale.y + sizechange * sizeScaleChange))
        //    + "-" +
        //    new Vector2(transform.position.x + col.size.x * (transform.localScale.x + sizechange * sizeScaleChange), transform.position.y + 0.1f));
        //Don't shrink or Grow past the minimum size;
        if (size + sizechange >= minSize &&
            size + sizechange <= maxSize &&
            //If you're growing, check if there's room
            (sizechange < 0 || !Physics2D.OverlapArea(
            new Vector2(transform.position.x - col.size.x * 0.5f * (transform.localScale.x + sizechange * sizeScaleChange), transform.position.y + col.size.y * (transform.localScale.y + sizechange * sizeScaleChange)),
            new Vector2(transform.position.x + col.size.x * 0.5f * (transform.localScale.x + sizechange * sizeScaleChange), transform.position.y + 0.1f),
            groundLayers))){

            size += sizechange;
            rb.mass += sizechange * massChange;
            transform.localScale += Vector3.one * sizechange * sizeScaleChange;

            //This will only happen if we shrink
            for(int i = 0; i< -sizechange; i++)
            {
                var eject = Instantiate(ejectedSandPrefab, transform.position + Vector3.down*0.3f, Quaternion.identity);
                eject.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                var ejectedSand = eject.GetComponent<EjectedSand>();
                gameController.AddEjectedSand(ejectedSand);
            }
            return true;
        }
        else
        {
            //TODO Some sort of feedback?
            return false;
        }

    }

    private void ResetSize()
    {
        var oldsize = size;
        size = 1;
        rb.mass -= massChange * (oldsize - 1);
        transform.localScale -= Vector3.one * (oldsize - 1) * sizeScaleChange;
    }
}
