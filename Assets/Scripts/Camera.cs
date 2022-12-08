using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour
{
    private CinemachineVirtualCamera vcCamera;
    private GameObject player;

    private void OnEnable()
    {
        CharacterSelectUI.playerSpawnEvent += CameraOn;
    }

    private void OnDisable()
    {
        CharacterSelectUI.playerSpawnEvent -= CameraOn;
    }

    void CameraOn()
    {
        vcCamera = GetComponent<CinemachineVirtualCamera>();
        vcCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
