using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCameraController : CameraController {

    private float fieldOfView = 60;

    [SerializeField]
    private Image lastPicture;

    private void OnEnable () => inputHandler.OnLeftMouseButtonPress += TakePhoto;
    private void OnDisable () => inputHandler.OnLeftMouseButtonPress -= TakePhoto;

    void Update () {

        fieldOfView -= inputHandler.mouseScrollDelta;
        fieldOfView = Mathf.Clamp (fieldOfView, 15, 70);
        cam.fieldOfView = fieldOfView;
    }

    private void TakePhoto () {

        StartCoroutine (TakeCoroutine ());
    }

    private IEnumerator TakeCoroutine () {

        yield return new WaitForEndOfFrame ();

        int width = Screen.width;
        int height = Screen.height;
        RenderTexture renderTexture = new RenderTexture (width, height, 16);
        Texture2D tex = new Texture2D (width, height, TextureFormat.RGB24, false);
        Rect rect = new Rect (0, 0, width, height);

        cam.targetTexture = renderTexture;
        cam.Render ();
        RenderTexture.active = renderTexture;
        tex.ReadPixels (rect, 0, 0);
        tex.Apply ();

        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy (renderTexture);

        lastPicture.sprite = Sprite.Create (tex, rect, Vector2.zero);
    }
}