using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhotoCameraConfig {

    [Tooltip ("Tempo em frames"), Range (1, 10)]
    public int tempoExposição = 3;
    [Tooltip ("Opacidade por frame"), Range (0f, 1f)]
    public float isoValue = 0.33f;
    public List<Material> filterList;
}