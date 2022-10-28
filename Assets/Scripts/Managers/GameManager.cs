using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
    public CinemachineVirtualCamera playerCamera;

    private void Awake() {
        instance = this;
    }

    public void setupCamera(PlayerUnit unitToFollow) {
        playerCamera.Follow = unitToFollow.transform;
    }
}
