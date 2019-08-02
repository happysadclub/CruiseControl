using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTextureToScreen : MonoBehaviour {

    RenderTexture rtFull; //full size rendertarget
    int upsampleFactor = 2; //render at half res
    Camera downSampleCamera;
    Rect orgCameraRect;


void Start()
    {
        downSampleCamera = GetComponent<Camera>();
        orgCameraRect = new Rect(downSampleCamera.rect);

        rtFull = new RenderTexture((int)(Screen.width), (int)(Screen.height), 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        downSampleCamera.rect = new Rect(orgCameraRect.x / upsampleFactor, orgCameraRect.y / upsampleFactor, orgCameraRect.width / upsampleFactor, orgCameraRect.height / upsampleFactor);
        //downSampleCamera.targetTexture = rtFull;

        //mtlUpscale = new Material(Shader.Find("Hidden/Upsampler/hq2x"));
    }

    public RenderTexture half;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // We are completely ignoring src
        half.filterMode = FilterMode.Point; //Set filtering of the source image to point for hq2x to work
        Graphics.Blit(half, dest); //Upscale the image
    }

    void OnGUI()
    {
        //Display the image on screen...this is an ugly solution...
        GUI.DrawTexture(new Rect(orgCameraRect.x * Screen.width,
                                 orgCameraRect.y * Screen.height,
                                 rtFull.width,
                                 rtFull.height),
                        rtFull);
    }
}
