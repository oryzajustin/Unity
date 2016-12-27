using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    //variable for referencing the player collider box
    private BoxCollider2D playerCollider;

    //allows for changing in unity toolbar
    [SerializeField]
    //variable for referencing the platform collider box
    private BoxCollider2D platformCollider;

    //allows for changing in unity toolbar
    [SerializeField]
    //variable for referencing the trigger collider of the platform
    private BoxCollider2D platformTrigger;

	// Use this for initialization
	void Start ()
    {
        //create reference to the player box collider
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
	}
	
	//function for allowing player to pass through the platform
	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
    }
    
    //function to set trigger again, making the player stand on the platform
    //after passing through it
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }
}
