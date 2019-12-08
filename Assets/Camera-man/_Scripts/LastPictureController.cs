using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LastPictureController : MonoBehaviour {

    [SerializeField]
    private Image lastPicture;
    [SerializeField]
    private Image background;
    [SerializeField]
    private Image vignette;

    public void Init () { }

    public void SetPicture (Sprite sprite) {

        gameObject.SetActive (true);

        vignette.DOColor (Color.white, 0.5f)
            .From (Color.black)
            .OnComplete (() => {

                vignette.DOFade (0, 0.5f);
                lastPicture.sprite = sprite;
            });
    }

    public void DisablePicture () {

        gameObject.SetActive (false);
        vignette.color = Color.black;
    }
}