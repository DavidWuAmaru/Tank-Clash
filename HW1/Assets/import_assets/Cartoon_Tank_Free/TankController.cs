using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    public Transform[] WheelMeshes;
    public WheelCollider[] WheelColls;
    Vector3 pos, rotation;
    Quaternion quat;
    public float force, RotSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = rotation;
        for (int i=0; i<WheelColls.Length; i++)
        {
            WheelColls[i].GetWorldPose(out pos, out quat);
            WheelMeshes[i].position = pos;
            WheelMeshes[i].rotation = quat;
        }
        foreach (var wheelcols in WheelColls)
        {
            wheelcols.motorTorque = Input.GetAxis("Vertical") * force * Time.deltaTime;
        }
        rotation.y += Input.GetAxis("Horizontal") * RotSpeed;
    }

}
