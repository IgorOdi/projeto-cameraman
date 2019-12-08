using UnityEngine;

[System.Serializable]
public class PhotoCameraConfig {

    [Tooltip ("Tempo em frames"), Range (1, 10)]
    public int tempoExposição = 1;

    [Tooltip ("Opacidade por frame"), Range (0f, 0.3f)]
    public float isoValue = 0.15f;
}