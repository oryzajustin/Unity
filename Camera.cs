using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    //allows for changing of the variables in the unity toolbar
    [SerializeField]
    //float variables for the maximum reach of the camera
    private float xMax, xMin, yMax, yMin;

    //variable to position camera on player
    private Transform target;

	// Use this for initialization
	void Start ()
    {
        //at the start set the target to the player
        target = GameObject.Find("Player").transform;
	}
	
	// delayed update so it follows player
	void LateUpdate ()
    {
        //clamp the values so the camera does not go out of bounds
        //vector 3 to allow camera functionality
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
	}
}
