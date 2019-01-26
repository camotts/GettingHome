using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Rendering/Fog Layer")]
[RequireComponent(typeof(Camera))]
public class FogToggle : MonoBehaviour
{
    /*
 This script lets you enable and disable per camera.
 By enabling or disabling the script in the title of the inspector, you can turn fog on or off per camera.
*/
    private bool revertFogState = false;

    void OnPreRender()
    {
        revertFogState = RenderSettings.fog;
        RenderSettings.fog = false;
    }

    void OnPostRender()
    {
        RenderSettings.fog = revertFogState;
    }
    
}
