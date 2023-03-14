using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_throw_missile : MonoBehaviour
{
    private float ftime;
    public GameObject missile_throw;
    public float speed;
    private void throw_missile()
    {
        ftime += Time.deltaTime;
        if (ftime >= 0.2f)
        {
            ftime = 0f;
            Vector3 rotation = transform.eulerAngles;
            GameObject clone_missile =  Instantiate(missile_throw, transform.position, Quaternion.Euler(rotation));
        }
    }
    private void plane_move()
    {
        float angle_left_right = transform.localEulerAngles.y;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 5f);
        this.Invoke("throw_missile", 0.5f);
        plane_move();
    }
}
