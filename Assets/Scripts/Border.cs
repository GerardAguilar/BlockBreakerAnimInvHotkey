using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
On exit collider, reverse velocity
How do we catch objects that go too fast for frames to catch them? 
    There's a solution via raycasting.
    We can also try clamping the position of each projectile
*/

public class Border : MonoBehaviour {

    Vector3 velocity;

    void OnTriggerExit2D(Collider2D other) {
        //Each ball should have a collider and a trigger
        //Parent=?
        //Child=?
        //Debug.Log("Border: Checking if anything exits");
        if (other.gameObject.CompareTag("Projectile")) {
            //Debug.Log("Border: Checking for Projectile exit");
            velocity = other.gameObject.GetComponent<Rigidbody2D>().velocity;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = velocity * -1;
        }
    }

}
