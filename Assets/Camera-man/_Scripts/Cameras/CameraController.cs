using UnityEngine;

public class CameraController : MonoBehaviour {

    protected InputHandler inputHandler;
    protected Camera cam;

    public void Init (InputHandler _inputHandler) {

        inputHandler = _inputHandler;
        cam = GetComponentInChildren<Camera> ();
    }

    public void SetActive (bool active) {

        gameObject.SetActive (active);
    }
}