using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class Cam
{
    static CinemachineBrain camBrain;
    static CinemachineVirtualCamera camVirtual;
    static CinemachineOrbitalTransposer camOrbital;
    static CinemachineComposer camAim;
    static CinemachineCollider camCollider;

    public static Vector3 getForward
    {
        get { return camBrain.transform.forward; }
    }

    public static void Initialize()
    {
        camBrain = Camera.main.GetComponent<CinemachineBrain>();
        camVirtual = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        camOrbital = camVirtual.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        camAim = camVirtual.GetCinemachineComponent<CinemachineComposer>();
        camCollider = camVirtual.GetComponent<CinemachineCollider>();
    }

    public static void Update()
    {
        if (Time.timeScale > 0.5)
        {
            camBrain.enabled = true;
            Vector3 follow = camOrbital.m_FollowOffset;
            follow.z = Mathf.Clamp(follow.z - Input.mouseScrollDelta.y * 10f * Time.deltaTime, 6, 14);
            follow.y = Mathf.Clamp(follow.y - Input.GetAxis("Mouse Y") * 1f * Time.deltaTime, -2, 5);
            camOrbital.m_FollowOffset = follow;
        }
        else
            camBrain.enabled = false;
    }
}
