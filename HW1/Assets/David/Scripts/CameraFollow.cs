using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 2, -5);
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed, rotationSpeed;
    private float angle = 10;
    public bool first_person = false;
    public void focus()
    {
        if (first_person)
        {
            target = GameObject.Find("TankFree_Tower").transform;
            offset = new Vector3(0, 1, -0.6f);
            angle = 0;
        }
        else
        {
            target = GameObject.Find("TankFree_Tower").transform;
            offset = new Vector3(0, 2, -3);
            angle = 10;
        }
    }
    public void unfocus()
    {
        if (first_person)
        {
            target = GameObject.Find("TankFree_Tower").transform;
            offset = new Vector3(0, 1, -0.6f);
            angle = 0;
        }
        else
        {
            target = GameObject.Find("TankFree_Blue").transform;
            offset = new Vector3(0, 2, -5);
            angle = 10;
        }
    }
    void Update()
    {
        HandleRotation();
        HandleTranslation();
    }
    private void HandleRotation()
    {
        Vector3 direction = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
        //var direction = target.position - transform.position;
        Quaternion qangle = Quaternion.Euler(angle, 0, 0);
        var rotataion = Quaternion.LookRotation(direction, Vector3.up) * qangle;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotataion, rotationSpeed * Time.deltaTime);

        //Vector3 tem = target.transform.eulerAngles;
        //Vector3 targetRotation = new Vector3(angle, tem.y, 0);
        //this.transform.eulerAngles = targetRotation;
    }
    private void HandleTranslation()
    {
        var targetPos = target.TransformPoint(offset); // local座標轉世界座標
        transform.position = Vector3.Lerp(transform.position, targetPos, translateSpeed * Time.deltaTime);
    }
}
