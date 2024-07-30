using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetUp_HCH : MonoBehaviour
{
    public Camera[] portalCam_A;
    public Camera[] portalCam_B;

    public Material[] portalCamMat_A;
    public Material[] portalCamMat_B;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < portalCam_A.Length; i++)
        {
            if (portalCam_A[i].targetTexture != null)
            {
                portalCam_A[i].targetTexture.Release();
            }
            portalCam_A[i].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            portalCamMat_A[i].mainTexture = portalCam_A[i].targetTexture;

            if (portalCam_B[i].targetTexture != null)
            {
                portalCam_B[i].targetTexture.Release();
            }
            portalCam_B[i].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            portalCamMat_B[i].mainTexture = portalCam_B[i].targetTexture;
        }
    }
}
