using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class LightingManager : MonoBehaviour
{
    //references
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPresets Preset;
    //variables
    [SerializeField, Range (0, 24)] private float TimeOfDay;


    private void Update()
    {
        DayManager dayManager = GetComponent<DayManager>();
        if (Preset == null)
            return;
        if (Application.isPlaying)
        {
            TimeOfDay = (float)dayManager.getClampHour;

            //clamp between 0 and 24
          //  TimeOfDay %= 24;

            UpdateLighting(TimeOfDay);
        }
        else
        {
            UpdateLighting(TimeOfDay);

            
        }

    }



    private void UpdateLighting(float timePercent)
    {
        timePercent = timePercent / 24F;
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170, 0));
        }



    }

    //Try to find directional light if one isn't set
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //search scene for lighting that fits criteria of directional
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}
