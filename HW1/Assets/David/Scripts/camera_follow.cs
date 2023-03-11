using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void adjust_position()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    private void adjust_rotation()
    {
        Vector3 desiredRotation = target.transform.localEulerAngles;
        this.transform.localEulerAngles = desiredRotation;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        adjust_position();
        //adjust_rotation();
    }
}
