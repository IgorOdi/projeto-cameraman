using UnityEngine;

public enum CameraMode {

    NORMAL,
    PHOTO
}

public class CameraManager : MonoBehaviour {

    public CameraController [] cameras;
    public CameraMode cameraMode { get; private set; }
    private InputHandler inputHandler;
    private float targetLookRotation;

    public void Init (InputHandler _inputHandler) {

        inputHandler = _inputHandler;
        for (int i = 0; i < cameras.Length; i++) {

            cameras [i].Init (_inputHandler);
        }

        _inputHandler.OnRightMouseButtonPress += () => {

            SetCameraMode (cameraMode.Equals (CameraMode.NORMAL) ? CameraMode.PHOTO : CameraMode.NORMAL);
        };
    }

    void Update () {

        targetLookRotation -= inputHandler.mouseAxis.x * 3f;
        targetLookRotation = Mathf.Clamp (targetLookRotation, -70f, 70f);
        transform.localEulerAngles = Vector3.right * targetLookRotation;
    }

    public void SetCameraMode (CameraMode mode) {

        cameraMode = mode;
        if (mode.Equals (CameraMode.NORMAL)) {

            cameras [0].SetActive (true);
            cameras [1].SetActive (false);
        } else {

            cameras [0].SetActive (false);
            cameras [1].SetActive (true);
        }
    }
}