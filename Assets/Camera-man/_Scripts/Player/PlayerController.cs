using UnityEngine;

public class PlayerController : MonoBehaviour {

    private InputHandler inputHandler;
    private CameraManager cameraManager;
    private float velocity;
    private float turnVelocity;
    private Vector3 moveAmount;
    private Vector3 targetEuler;
    private Rigidbody rb;

    private bool CanMove { get; set; }

    public void Init (InputHandler _inputHandler, CameraManager _cameraManager) {

        velocity = 10f;
        turnVelocity = 3f;
        inputHandler = _inputHandler;
        cameraManager = _cameraManager;
        rb = GetComponent<Rigidbody> ();
    }

    void Update () {

        CanMove = cameraManager.cameraMode.Equals (CameraMode.NORMAL);
        targetEuler.y = inputHandler.mouseAxis.y * turnVelocity;
        transform.eulerAngles += targetEuler;

        if (CanMove) {

            moveAmount = inputHandler.moveAxis * velocity;
            rb.MovePosition (rb.position + transform.TransformDirection (moveAmount * Time.deltaTime));
        }
    }
}