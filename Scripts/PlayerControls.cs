using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Input variables
    float xThrust;
    float yThrust;
    // Variables 
    [Header("Input Clamps and General Variables")]
    [Tooltip("Set Speed Factor for the Ship")][SerializeField] float ControlSpeed = 10f;
    [Tooltip("Set Clamp Range for Screen Space - Horizontal")][SerializeField] float xRange = 10f;
    [Tooltip("Set Clamp Range for Screen Space - Vertical")][SerializeField] float yRange = 10f;

    // Laser array for turning on/off
    [Header("Lasers Array for Ship")]
    [Tooltip("Add all Ship Lasers present to this array")]
    [SerializeField] GameObject[] Lasers;

    // Rotation Variables
    [Header("Position Factors Tuning")]
    [SerializeField] float PositionPitchFactor = 2f;
    [SerializeField] float PositionYawFactor = 2f;

    [Header("Control Factors Tuning")]
    [SerializeField] float ControlPitchFactor = -10f;
    [SerializeField] float ControlRollFactor  = -15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // not stable rn
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrust = Input.GetAxis("Horizontal");
        //Debug.Log(xThrust);

        // Inverted for this instance
        yThrust = Input.GetAxis("Vertical");
        //Debug.Log(yThrust);


        float xOffset = xThrust * Time.deltaTime * ControlSpeed;
        float RawXPos = transform.localPosition.x + xOffset;
        float LimitedxPos = Mathf.Clamp(RawXPos, -xRange, xRange);

        float yOffset = yThrust * Time.deltaTime * ControlSpeed;
        float RawYPos = transform.localPosition.y + yOffset;
        float LimitedyPos = Mathf.Clamp(RawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(LimitedxPos, LimitedyPos, transform.localPosition.z);
    }

    // Added effects using cinemachine for follow camera if needed
    void ProcessRotation()
    {
        // Break Equation for Pitch
        float PitchDueToPosition = transform.localPosition.y * -PositionPitchFactor;
        float PitchDueToControlThrust = yThrust * ControlPitchFactor;
        float Pitch = PitchDueToPosition + PitchDueToControlThrust;

        // Break Equation for Yaw
        float YawDueToPositon = transform.localPosition.x * PositionYawFactor;
        float Yaw = YawDueToPositon;
        
        // Break Equation for Roll
        float Roll = xThrust * ControlRollFactor;
        transform.localRotation = Quaternion.Euler(Pitch, Yaw, Roll);
    }

    // Process Firing function
    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            //Debug.Log("Im Shooting");
            SetLasersActive(true);
        }
        else
        {
            //Debug.Log("Not Shooting");
            SetLasersActive(false);
        }
    }

    // Activation function for lasers - Deactivation based on bool - [Redacted mehtod - 2 functions]
    private void SetLasersActive(bool isActive)
    {
        foreach(GameObject Laser in Lasers)
        {
            // causes already shot lasers to diappear - [Redacted]
            //Laser.SetActive(true); w
            // one by one set active fot both or more if powered up / upgraded

            var EmissionComponent = Laser.GetComponent<ParticleSystem>().emission;
            EmissionComponent.enabled = isActive;
        }
    }
}