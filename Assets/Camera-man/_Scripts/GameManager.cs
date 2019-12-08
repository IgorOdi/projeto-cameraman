using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private InputHandler inputHandler;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private CameraManager cameraManager;

    void Awake () {

        inputHandler.Init ();
        cameraManager.Init (inputHandler);
        player.Init (inputHandler, cameraManager);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}