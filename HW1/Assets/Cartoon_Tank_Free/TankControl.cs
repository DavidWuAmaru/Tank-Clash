using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankControl : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalInput, verticalInput, currentBreakForce;
    private float steerAngle;
    private bool isBreaking;
    [SerializeField] private WheelCollider fLeftWheel, fRightWheel, bLeftWheel, bRightWheel;
    [SerializeField]
    private Transform fLeftWheelTransform, fRightWheelTransform
        , bLeftWheelTransform, bRightWheelTransform;
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    public float centerOfMass = -0.9f;
    private Vector3 originalPos, currentPos;
    private AudioSource movingAudio;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, centerOfMass, 0);
        // originalPos = transform.position;
        movingAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        HandleAudio();
        // CheckIsMovingAndPlayAudio();
    }

    private void HandleAudio()
    {
        if(!movingAudio.isPlaying)
        {
            movingAudio.Play();
        }
        if (verticalInput > 0.5 || verticalInput < -0.5)
        {
            movingAudio.UnPause();
        }
        else
        {
            movingAudio.Pause();
        }
    }

  

    private void UpdateWheels()
    {
        UpdateSingleWheel(fRightWheel, fRightWheelTransform);
        UpdateSingleWheel(fLeftWheel, fLeftWheelTransform);
        UpdateSingleWheel(bRightWheel, bRightWheelTransform);
        UpdateSingleWheel(bLeftWheel, bLeftWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        // 取得collider的位置與旋轉角度, 並套用到Transform
        Vector3 pos;
        Quaternion quat;
        wheelCollider.GetWorldPose(out pos, out quat);
        wheelTransform.position = pos;
        wheelTransform.rotation = quat;
    }
    private void HandleSteering()
    {
        steerAngle = horizontalInput * maxSteerAngle;
        fLeftWheel.steerAngle = steerAngle;
        fRightWheel.steerAngle = steerAngle;
    }
    private void HandleMotor()
    {

        // 四輪驅動
        fLeftWheel.motorTorque = verticalInput * motorForce;
        fRightWheel.motorTorque = verticalInput * motorForce;
        bLeftWheel.motorTorque = verticalInput * motorForce;
        bRightWheel.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        if (true)
        {
            fLeftWheel.brakeTorque = currentBreakForce;
            fRightWheel.brakeTorque = currentBreakForce;
            bLeftWheel.brakeTorque = currentBreakForce;
            bRightWheel.brakeTorque = currentBreakForce;
        }
        // Debug.Log(currentBreakForce.ToString());
        //Debug.Log(fLeftWheel.brakeTorque.ToString());
    }
    private void GetInput()
    {
        // 取得輸入量(WASD及空白)
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);

    }
}
