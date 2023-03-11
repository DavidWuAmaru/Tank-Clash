using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed, rotationSpeed;

    void Update()
    {
        HandleRotation();
        HandleTranslation();
    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotataion = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotataion, rotationSpeed * Time.deltaTime);


    }
    private void HandleTranslation()
    {
        var targetPos = target.TransformPoint(offset); // local座標轉世界座標
        transform.position = Vector3.Lerp(transform.position, targetPos, translateSpeed * Time.deltaTime);

    }
}
