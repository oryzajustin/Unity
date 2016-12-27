using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // variable for 2d physics on character
    private Rigidbody2D myRigidBody;

    // variable for linking animation from idle to run
    private Animator myAnimator;

    // allows for float to be changed in unity toolbar
    [SerializeField]
    //variable for speed of movement
    private float speed;

    //variable for checking if the character is facing in the right direction
    private bool facingRight;

    // allows for array to be edited in unity toolbar
    [SerializeField]
    //array containing the ground points of the player
    private Transform[] groundPoints;

    // allows for variable to be changed in unity toolbar
    [SerializeField]
    // a radius that indicates how close the player needs to be to the ground
    private float groundRadius;

    //allows for variable to be changed in unity toolbar
    [SerializeField]
    //variable to indicate which ground objects are considered ground
    private LayerMask isGround;

    //boolean for if the player is grounded
    private bool isGrounded;

    //boolean for jumping
    private bool jump;

    //allows for variable to be changed in unity toolbar
    [SerializeField]
    //float for the force of the jump on the player
    private float jumpPower;

    //allows for variable to be changed in unity toolbar
    [SerializeField]
    //boolean for player movement while jumping
    private bool airControl;

	// Use this for initialization
	private void Start ()
    {
        //initialize at the start of the game
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
        //this line of code gets the Horizontal variable in the unity toolbar's value
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        //calling functions
        HandleInput();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleLayers();

        //reset values after other function calls
        ResetValues();
    }

    //handles player movement
    //takes horizontal as parameter
    private void HandleMovement(float horizontal)
    {
        //if the player is falling
        if(myRigidBody.velocity.y < 0)
        {
            //play the landing animation
            myAnimator.SetBool("landing", true);
        }
        //if the player is grounded or has air control
        //allow for player movement when jumping
        if(isGrounded || airControl)
        {
            myRigidBody.velocity = new Vector2(horizontal * speed, myRigidBody.velocity.y);
        }
        //if the player is grounded and the jump action is initiated,
        //the player is not grounded anymore
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpPower));
            myAnimator.SetTrigger("jump");
        }
        //sets the velocity forced on the player's horizontal to the horizontal multiplied by the
        //specified speed in the unity toolbar
        myRigidBody.velocity = new Vector2(horizontal * speed, myRigidBody.velocity.y);
        //sets the "runspeed" trigger to the absolute value of the horizontal
        myAnimator.SetFloat("runspeed", Mathf.Abs(horizontal));
    }

    //function for handling player input
    private void HandleInput()
    {
        //set jump to true if the player presses the spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    //function for resetting values after each update call
    private void ResetValues()
    {
        jump = false;
    }

    //function for flipping the character to facing left/right
    private void Flip(float horizontal)
    {
        //if you are facing the opposite way as the movement
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            //flip the character
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    //function for dertermining if the player is on the ground
    private bool IsGrounded()
    {
        //if the player is falling, not moving, or just running on the ground
        if (myRigidBody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                //make a new array of colliders
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, isGround);
                for(int i=0; i < colliders.Length; i++)
                {
                    //check if the current collider is the player's collider
                    //return true if player is colliding with something other than itself
                    if(colliders[i].gameObject != gameObject)
                    {
                        //reset the trigger for jump, so that the landing animation can be played after
                        myAnimator.ResetTrigger("jump");
                        //when the landing animation is done playing, reset it
                        myAnimator.SetBool("landing", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        //if player is in the air
        if (!isGrounded)
        {
            //the weight of the air layer is set to 1, so that the jumping animation
            //plays over anything else
            myAnimator.SetLayerWeight(1, 1);
        }
        //if the player is on the ground
        else
        {
            //set the weight of the air layer to 0, so that the jumping animation
            //does not play
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
