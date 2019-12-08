using UnityEngine;

public enum CameraMode {

    NORMAL,
    PHOTO
}

public class CameraManager : MonoBehaviour {

    public CameraController [] cameras;
    public CameraMode cameraMode { get; private set; }

    public void Init (InputHandler _inputHandler) {
        for (int i = 0; i < cameras.Length; i++) {

            cameras [i].Init (_inputHandler);
        }

        _inputHandler.OnRightMouseButtonPress += () => {

            SetCameraMode (cameraMode.Equals (CameraMode.NORMAL) ? CameraMode.PHOTO : CameraMode.NORMAL);
        };
    }

    public void SetCameraMode (CameraMode mode) {

        cameraMode = mode;
        if (mode.Equals (CameraMode.NORMAL)) {

            cameras [0].SetActive (true);
            cameras [1].SetActive (false);
            cameras [0].transform.eulerAngles = cameras [1].transform.eulerAngles;
        } else {

            cameras [0].SetActive (false);
            cameras [1].SetActive (true);
            cameras [1].transform.eulerAngles = cameras [0].transform.eulerAngles;
        }
    }
}