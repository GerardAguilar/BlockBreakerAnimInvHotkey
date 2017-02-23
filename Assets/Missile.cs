using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    Vector3 target;
    Vector3 startPosition;
    Rigidbody2D rb;
    public float lerpStart;
    public float timeSinceStartedLerping;
    public float lerpDuration;
    public float percentageComplete;

    void OnEnable() {
        rb = GetComponent<Rigidbody2D>();
        lerpStart = Time.time;
        lerpDuration = 1f;
        percentageComplete = 0f;
        startPosition = transform.position;
    }

    void Update() {
        if (percentageComplete > .99)
        {
            ResetMissile();
        }
    }

    void FixedUpdate()
    {
        timeSinceStartedLerping = Time.time - lerpStart;//
        percentageComplete = timeSinceStartedLerping / lerpDuration;
        transform.position = Vector3.Lerp(startPosition, target, percentageComplete);

    }

    public void ResetMissile() {        
        timeSinceStartedLerping = 0f;
        percentageComplete = 0f;
        transform.position = transform.parent.position;
        gameObject.SetActive(false);
    }

    public void SetTarget(Vector3 tempTarget) {
        target = tempTarget;
    }


}
