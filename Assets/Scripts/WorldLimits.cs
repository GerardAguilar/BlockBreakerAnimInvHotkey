using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Based on the background, we'll generate limits that can be publicly exposed for the Circle, the Ball, and the Borders to utilize
*/
public class WorldLimits : MonoBehaviour {

    public Bounds bgBounds;
    public float pixelPerUnit;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public Vector2 recenter;
    public float mouseXMin;
    public float mouseXMax;
    public float mouseYMin;
    public float mouseYMax;



    // Use this for initialization
    void Start() {
        bgBounds = GetComponent<SpriteRenderer>().sprite.bounds;
        pixelPerUnit = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        xMin = -1 * bgBounds.extents.x;
        xMax = 1 * bgBounds.extents.x - (8.8f/ pixelPerUnit);//Side UI
        yMin = -1 * bgBounds.extents.y;
        yMax = 1 * bgBounds.extents.y;

        //Debug.Log(Screen.width+":"+Screen.height);
        //Debug.Log(bgBounds.extents.x * pixelPerUnit);

    }

}
