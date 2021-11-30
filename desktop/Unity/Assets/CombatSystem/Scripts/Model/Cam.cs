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

    static Queue<float> zoomQueue = new Queue<float>();
    static float zoomOffset;

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

        zoomQueue.Enqueue(-6);
        zoomQueue.Enqueue(-10);
        zoomQueue.Enqueue(-14);
        zoomOffset = -6;
    }

    public static void Update()
    {
        if (Time.timeScale > 0.5)
        {
            camBrain.enabled = true;
            Vector3 follow = camOrbital.m_FollowOffset;

            //zoom control
            if (Input.GetMouseButtonDown(2))
            {
                zoomQueue.Enqueue(zoomQueue.Dequeue());
                zoomOffset = zoomQueue.Peek();
            }
            
            //vertical control
            follow.y = Mathf.Clamp(follow.y - Input.GetAxis("Mouse Y") * 1f * Time.deltaTime, 0, 5);


            //update zoom offset;
            follow.z = Mathf.Lerp(follow.z, zoomOffset, 0.2f);
            camOrbital.m_FollowOffset = follow;
        }
        else
            camBrain.enabled = false;    
    }
}
