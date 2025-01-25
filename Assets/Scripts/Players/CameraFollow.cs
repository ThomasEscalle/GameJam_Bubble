using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 velocity = Vector3.zero;
    public float zDistance = 42.0f;



    public float minCam = 13.0f;
    public float maxCam = 19.0f;

    public float minCamY = 8.0f;
    public float maxCamY = 20.0f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        smoothedPosition.z = zDistance;
        transform.position = smoothedPosition;
        
        /// Put the x between the min and max
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCam, maxCam), Mathf.Clamp(transform.position.y, minCamY, maxCamY), transform.position.z);
    }
}
