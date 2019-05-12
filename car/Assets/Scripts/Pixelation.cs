using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Pixelation : MonoBehaviour
{

    public RenderTexture renderTexture;
    public Material paletteMaterial;

    void Start()
    {
        int realRatio = Mathf.RoundToInt(Screen.width / Screen.height);
        //print(realRatio);
        //renderTexture.width = NearestSuperiorPowerOf2(Mathf.RoundToInt(renderTexture.width * realRatio));
        Debug.Log("(Pixelation)(Start)renderTexture.width: " + renderTexture.width);
    }

    void OnGUI()
    {
        GUI.depth = 0;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), renderTexture);
    }

    int NearestSuperiorPowerOf2(int n)
    {
        return (int)Mathf.Pow(2, Mathf.Ceil(Mathf.Log(n) / Mathf.Log(2)));
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        //Graphics.Blit(src, _downscaledRenderTexture, identityMaterial);
        Graphics.Blit(src, renderTexture, paletteMaterial);
    }
}