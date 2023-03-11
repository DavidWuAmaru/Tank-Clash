using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 2, -5);
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed, rotationSpeed;
    [SerializeField] private float angle;
    public void focus()
    {
        target = GameObject.Find("TankFree_Tower").transform;
        offset = new Vector3(0, 2, -3);
    }
    public void unfocus()
    {
        target = GameObject.Find("TankFree_Blue").transform;
        offset = new Vector3(0, 2, -5);
    }

    void Update()
    {
        HandleRotation();
        HandleTranslation();
    }
    private void HandleRotation()
    {
        //var direction = target.position - transform.position;
        //var rotataion = Quaternion.LookRotation(direction, Vector3.up);
        //Debug.Log(rotataion);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotataion, rotationSpeed * Time.deltaTime);

        Vector3 tem = target.transform.eulerAngles;
        Vector3 targetRotation = new Vector3(angle, tem.y, 0);
        this.transform.eulerAngles = targetRotation;
    }
    private void HandleTranslation()
    {
        var targetPos = target.TransformPoint(offset); // local座標轉世界座標
        transform.position = Vector3.Lerp(transform.position, targetPos, translateSpeed * Time.deltaTime);
    }
}
