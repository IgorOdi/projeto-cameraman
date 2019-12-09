using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO: Corrigir valores de foco
//TODO: Aplicar efeito de granulado no iso

public class PhotoCameraController : CameraController {

    [SerializeField]
    private PhotoCameraConfig configs;
    private float fieldOfView = 60;

    [SerializeField]
    private PostProcessController postProcessController;

    [SerializeField]
    private LastPictureController lastPictureController;
    private List<Texture2D> textures = new List<Texture2D> ();

    [SerializeField]
    private LayerMask focusLayer;
    private RaycastHit focusHit;
    private float focusDistance;

    private float MIN_FOCUS_DISTANCE = 5;

    private void OnEnable () => inputHandler.OnLeftMouseButtonPress += TakePhoto;
    private void OnDisable () => inputHandler.OnLeftMouseButtonPress -= TakePhoto;

    public override void Init (InputHandler _inputHandler) {

        base.Init (_inputHandler);
        postProcessController.Init ();
        lastPictureController.Init ();
        inputHandler.OnNumberKeyPress += SetFilter;
    }

    void Update () {

        fieldOfView -= inputHandler.mouseScrollDelta;
        fieldOfView = Mathf.Clamp (fieldOfView, 15, 70);
        cam.fieldOfView = fieldOfView;
        CalculateFocus ();
        postProcessController.SetAperture (1 - configs.aberturaFocal);
    }

    private void CalculateFocus () {

        if (Physics.Raycast (cam.transform.position, cam.transform.forward, out focusHit, 100, focusLayer)) {

            focusDistance = Mathf.Max (MIN_FOCUS_DISTANCE, Vector3.Distance (cam.transform.position, focusHit.point) - (60 - fieldOfView));
            postProcessController.SetFocusDistance (focusDistance);
        }
    }

    private void SetFilter (int filterIndex) {

        postProcessController.material = configs.filterList [filterIndex];
    }

    private void TakePhoto () {

        StartCoroutine (TakeCoroutine ());
    }

    private IEnumerator TakeCoroutine () {

        int width = 512;
        int height = 512;
        Texture2D tex = new Texture2D (width, height, TextureFormat.ARGB32, false);
        Rect rect = new Rect (0, 0, width, height);

        for (int i = 0; i < configs.tempoExposição; i++) {

            RenderTexture renderTexture = new RenderTexture (width, height, 16);
            cam.targetTexture = renderTexture;
            cam.Render ();
            RenderTexture.active = renderTexture;

            Texture2D lastTex = new Texture2D (width, height, TextureFormat.ARGB32, false);
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
        finalColors = Enumerable.Range (0, finalColors.Length).Select (x => Color.black).ToArray ();
        for (int i = 0; i < textures.Count; i++) {

            Color [] texturePixelColors = textures [i].GetPixels ();
            for (int j = 0; j < finalColors.Length; j++) {

                finalColors [j] += texturePixelColors [j] * configs.isoValue;
            }
        }

        mainTex.SetPixels (finalColors);
        mainTex.Apply ();
    }
}