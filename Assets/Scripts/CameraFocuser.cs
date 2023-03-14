using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFocuser : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera[] cameras;
    public CinemachineVirtualCamera myCam;

    void Start()
    {
        cameras = FindObjectsOfType<CinemachineVirtualCamera>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {

        if(CutsceneAnimator.isActive) return;

        if(col.CompareTag("Player"))
        {
            //iterate all cameras
            foreach (var cam in cameras)
            {
                if(cam == myCam)
                {
                    cam.enabled = true;
                } else
                {
                    cam.enabled = false;
                }
            }
        }
    }

}
