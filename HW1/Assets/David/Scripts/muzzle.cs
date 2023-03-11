using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzle : MonoBehaviour
{
    // Start is called before the first frame update
    private float angle_up_down = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate();
        float mouseCenter = Input.GetAxis("Mouse ScrollWheel");
        if (mouseCenter > 0)
        {
            if(angle_up_down < 45)
            {
                angle_up_down += 3f;
            }
        }
        if (mouseCenter < 0)
        {
            if (angle_up_down > 0)
            {
                angle_up_down -= 3f;
            }
        }
        this.gameObject.transform.localRotation = Quaternion.Euler(-angle_up_down, 0, 0);
    }
}
