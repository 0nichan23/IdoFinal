using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera frontCam;
    [SerializeField] CinemachineVirtualCamera topCam;


    public void SwitchToTop()
    {
        frontCam.Priority = 1;
        topCam.Priority = 2;
    }
    public void SwitchToFront()
    {
        frontCam.Priority = 2;
        topCam.Priority = 1;
    }


}
