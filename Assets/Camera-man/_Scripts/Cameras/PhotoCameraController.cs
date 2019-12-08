using UnityEngine;

public class PhotoCameraController : CameraController {

    private void OnEnable () => inputHandler.OnLeftMouseButtonPress += TakePhoto;
    private void OnDisable () => inputHandler.OnLeftMouseButtonPress -= TakePhoto;

    private void TakePhoto () {

        Debug.Log ("Photo taken");
    }
}