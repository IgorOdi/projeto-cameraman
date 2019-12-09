using UnityEngine;

[ExecuteInEditMode]
public class PostProcessController : MonoBehaviour {

    public Material material;

    void OnRenderImage (RenderTexture src, RenderTexture dst) {

        if (material == null) return;
        Graphics.Blit (src, dst, material);
    }
}