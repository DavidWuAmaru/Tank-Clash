using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rig;
    public float force;
    private bool already_boom = false;
    private void shoot()
    {
        float angle_up_down;
        float tem = this.gameObject.transform.localEulerAngles.x;
        if (tem > 45)
        {
            angle_up_down = 360 - tem;
        }
        else
        {
            angle_up_down = tem;
        }

        float angle_left_right = transform.localEulerAngles.y;
        float force_left_right = force - angle_up_down;

        float xComponent = Mathf.Sin(angle_left_right * Mathf.PI / 180) * force_left_right;
        float zComponent = Mathf.Cos(angle_left_right * Mathf.PI / 180) * force_left_right;

        //this.gameObject.transform.localRotation = Quaternion.Euler(-angle, 0, 0);
        rig.AddForce(xComponent, angle_up_down, zComponent, ForceMode.Impulse);
    }
    private void destroy()
    {
        Destroy(this.gameObject);
    }
    private void boom()
    {
        if (!already_boom)
        {
            already_boom = true;
            transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.Invoke("destroy", 0.1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        rig.isKinematic = true;
        boom();
    }
    void Start()
    {
        rig = this.gameObject.GetComponent<Rigidbody>();
        //Debug.Log(angle);
        shoot();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 5f);
    }
}
