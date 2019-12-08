using UnityEngine;

public class CameraController : MonoBehaviour {

    protected InputHandler inputHandler;
    protected Camera cam;
    private Vector3 targetEuler;

    public void Init (InputHandler _inputHandler) {

        inputHandler = _inputHandler;
    }

    void Update () {

        Vector3 eulerValue = Vector3.right * inputHandler.mouseAxis.x * 3f;
        targetEuler.x = Mathf.Clamp (eulerValue.x, -70, 70f);
        transform.eulerAngles -= targetEuler;
    }

    public void SetActive (bool active) {

        gameObject.SetActive (active);
    }
}