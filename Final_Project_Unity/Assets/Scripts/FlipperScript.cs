using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FlipperScript : MonoBehaviour
{
    public float flipTime = 1.5f;
    public float flipStartTime = 0.0f;
    public bool facingLeft = false;
    public bool isFlipping = false;

    public float tiltFrequency = 20f;
    public float maxTiltAngle = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlipping)
        {
            float angle;
            float finalAngle = facingLeft ? 180 : 0;
            float timeFlipping = Time.time - flipStartTime;
            angle = Mathf.Lerp(180 - finalAngle, finalAngle, timeFlipping/flipTime);
            if (timeFlipping > flipTime)
            {
                angle = finalAngle;
                isFlipping = false;
            }
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.y);
        }
    }

    public void Flip()
    {
        facingLeft = !facingLeft;
        flipStartTime =  Time.time;
        isFlipping = true;
    }

    public void Tilt()
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            maxTiltAngle * Mathf.Sin(Time.time * tiltFrequency));
    }

    public void ResetTilt()
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            0.0f
            );
    }
}
