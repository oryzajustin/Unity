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
        //calling functions
        HandleMovement(horizontal);
        Flip(horizontal);
	}

    //handles player movement
    //takes horizontal as parameter
    private void HandleMovement(float horizontal)
    {
        //sets the velocity forced on the player's horizontal to the horizontal multiplied by the
        //specified speed in the unity toolbar
        myRigidBody.velocity = new Vector2(horizontal * speed, myRigidBody.velocity.y);
        //sets the "runspeed" trigger to the absolute value of the horizontal
        myAnimator.SetFloat("runspeed", Mathf.Abs(horizontal));
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
}
