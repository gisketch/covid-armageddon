using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraShaker : MonoBehaviour {

    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 0.5f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 1.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    public Animator hpHurt;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start()
    {   
        Debug.Log("STARTED CAMERA MANAGER");
        ResetCams();
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
        {
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        
            virtualCameraNoise.m_AmplitudeGain = 0;
            virtualCameraNoise.m_FrequencyGain = 0;
        }
            

    }

    void ResetCams()
    {
        CinemachineVirtualCamera[] cameras = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var cam in cameras)
        {
            cam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }

    public void ShakeScreen(bool hp, float shakeDur)
    {
        CinemachineVirtualCamera[] cameras = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var cam in cameras)
        {
            if(cam.enabled == true)
            {
                VirtualCamera = cam;
                virtualCameraNoise = cam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
            } else
            {
                continue;
            }
        }
        ShakeElapsedTime = shakeDur;
        if(hp) hpHurt.SetTrigger("hurt");
    }
}