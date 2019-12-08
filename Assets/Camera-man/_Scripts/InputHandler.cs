using System;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    [HideInInspector]
    public Vector2 mouseAxis;
    [HideInInspector]
    public Vector3 moveAxis;

    public Action OnRightMouseButtonPress;
    public Action OnRightMouseButtonRelease;

    public Action OnLeftMouseButtonPress;

    private const string MOUSE_HORIZONTAL_AXIS = "Mouse X";
    private const string MOUSE_VERTICAL_AXIS = "Mouse Y";
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    public void Init () {

    }

    private void Update () {

        mouseAxis = Vector2.up * Input.GetAxisRaw (MOUSE_HORIZONTAL_AXIS) + Vector2.right * Input.GetAxisRaw (MOUSE_VERTICAL_AXIS);
        moveAxis = Vector3.forward * Input.GetAxisRaw (VERTICAL_AXIS) + Vector3.right * Input.GetAxisRaw (HORIZONTAL_AXIS);

        if (Input.GetMouseButtonDown (1)) {

            OnRightMouseButtonPress?.Invoke ();
        }

        if (Input.GetMouseButtonUp (1)) {

            OnRightMouseButtonRelease?.Invoke ();
        }

        if (Input.GetMouseButtonDown (0)) {

            OnLeftMouseButtonPress?.Invoke ();
        }
    }
}