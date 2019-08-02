using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pixellate : MonoBehaviour
{
    public Material paletteMaterial;
    public Material identityMaterial;

    private RenderTexture _downscaledRenderTexture;

    private void OnEnable()
    {
        var camera = GetComponent<Camera>();
        int height = camera.pixelHeight / 2;
        int width = Mathf.RoundToInt(camera.aspect * height);
        _downscaledRenderTexture = new RenderTexture(height, width, 16);
        _downscaledRenderTexture.filterMode = FilterMode.Point;
    }

    private void OnDisable()
    {
        DestroyImmediate(_downscaledRenderTexture);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        Graphics.Blit(src, _downscaledRenderTexture, identityMaterial);
        Graphics.Blit(_downscaledRenderTexture, dst, paletteMaterial);
    }
}