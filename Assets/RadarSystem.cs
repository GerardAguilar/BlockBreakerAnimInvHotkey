using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSystem : MonoBehaviour
{

    Vector3 target;
    Vector2 center;
    float radius;
    //string lookOutFor;
    MissilePool missilePool;

    //[SerializeField]
    private int layerMask;
    [SerializeField]
    private Collider2D[] colliders;

    void Awake()
    {
        center = new Vector2(transform.position.x, transform.position.y);
        radius = 50f;
        //lookOutFor = "Player";
        
        //layerMask = LayerMask.NameToLayer("Player");
        layerMask = 1 << 8;//Use the bitmask operator for the right layer (it's not 8 for 8), it's actually 256 for layer 8
    }

    void Update()
    {
        Sense();       
        
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag(lookOutFor))
    //    {
    //        if (!colliderList.Contains(other))
    //        {
    //            colliderList.Add(other);
    //        }
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag(lookOutFor))
    //    {
    //        if (colliderList.Contains(other))
    //        {
    //            colliderList.Remove(other);
    //        }
    //    }
    //}

    //Similar to raycast, any thing inside sphere would be collected.
    void Sense()
    {
        //Debug.Log("Sense()");
        colliders = Physics2D.OverlapCircleAll(center, radius, layerMask);
    }

    public int HowManyTargetted() {
        return colliders.Length;
    }

    public Vector3 WheresThePlayer() {
        return colliders[0].transform.position;
    }

    

}
