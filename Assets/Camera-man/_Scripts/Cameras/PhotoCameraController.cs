using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhotoCameraController : CameraController {

    [SerializeField]
    private PhotoCameraConfig configs;
    private float fieldOfView = 60;

    [SerializeField]
    private LastPictureController lastPictureController;
    private List<Texture2D> textures = new List<Texture2D> ();

    private void OnEnable () => inputHandler.OnLeftMouseButtonPress += TakePhoto;
    private void OnDisable () => inputHandler.OnLeftMouseButtonPress -= TakePhoto;

    public new void Init (InputHandler _inputHandler) {

        base.Init (_inputHandler);
        lastPictureController.Init ();
    }

    void Update () {

        fieldOfView -= inputHandler.mouseScrollDelta;
        fieldOfView = Mathf.Clamp (fieldOfView, 15, 70);
        cam.fieldOfView = fieldOfView;
    }

    private void TakePhoto () {

        StartCoroutine (TakeCoroutine ());
    }

    private IEnumerator TakeCoroutine () {

        int width = 512;
        int height = 512;
        RenderTexture renderTexture = new RenderTexture (width, height, 16);
        Texture2D tex = new Texture2D (width, height, TextureFormat.RGB24, false);
        Rect rect = new Rect (0, 0, width, height);

        for (int i = 0; i < configs.tempoExposição; i++) {

            cam.targetTexture = renderTexture;
            cam.Render ();
            RenderTexture.active = renderTexture;

            Texture2D lastTex = new Texture2D (width, height, TextureFormat.RGB24, false);
            lastTex.ReadPixels (rect, 0, 0);
            lastTex.Apply ();
            textures.Add (lastTex);

            cam.targetTexture = null;
            RenderTexture.active = null;
            Destroy (renderTexture);

            yield return new WaitForEndOfFrame ();
        }

        ProcessAllPictures (ref tex);

        lastPictureController.SetPicture (Sprite.Create (tex, rect, Vector2.zero));
        textures.Clear ();

        yield return new WaitForSeconds (2f);
        lastPictureController.DisablePicture ();
    }

    private void ProcessAllPictures (ref Texture2D mainTex) {

        Color [] finalColors = new Color [mainTex.GetPixels ().Length];
        for (int j = 0; j < textures.Count; j++) {

            Color [] texturePixelColors = textures [j].GetPixels ();
            for (int i = 0; i < finalColors.Length; i++) {

                finalColors [i] += texturePixelColors [i] * configs.isoValue;
            }
        }
        mainTex.SetPixels (finalColors);
        mainTex.Apply ();
    }
}