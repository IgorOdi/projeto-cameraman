using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteInEditMode]
public class PostProcessController : MonoBehaviour {

    public Material material;
    [SerializeField]
    private PostProcessVolume volume;

    private DepthOfField depthOfField;

    void OnRenderImage (RenderTexture src, RenderTexture dst) {

        if (material == null) return;
        Graphics.Blit (src, dst, material);
    }

    public void Init () {

        volume.profile.TryGetSettings<DepthOfField> (out depthOfField);
    }

    public void SetFocusDistance (float distance) {

        depthOfField.focusDistance.value = distance;
    }

    public void SetAperture (float aperture) {

        depthOfField.aperture.value = aperture;
    }
}