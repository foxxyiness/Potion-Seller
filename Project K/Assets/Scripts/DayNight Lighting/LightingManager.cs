using UnityEngine;
using UnityEngine.Serialization;

namespace DayNight_Lighting
{
    [ExecuteInEditMode]
    public class LightingManager : MonoBehaviour
    {
        //references
        [FormerlySerializedAs("DirectionalLight")] [SerializeField] private Light directionalLight;
        [FormerlySerializedAs("Preset")] [SerializeField] private LightingPresets preset;
        //variables
        [FormerlySerializedAs("TimeOfDay")] [SerializeField, Range (0, 24)] private float timeOfDay;
        private DayManager _dayManager;
        private bool _isPresetNull;


        private void Start()
        {
            _isPresetNull = preset == null;
            _dayManager = GetComponent<DayManager>();
        }

        private void Update()
        {
            if (_isPresetNull)
                return;
            if (Application.isPlaying)
            {
                timeOfDay = (float)_dayManager.getClampHour;

                //clamp between 0 and 24
                //  TimeOfDay %= 24;

                UpdateLighting(timeOfDay);
            }
            else
            {
                UpdateLighting(timeOfDay);

            
            }

        }



        private void UpdateLighting(float timePercent)
        {
            timePercent = timePercent / 24F;
            RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
            RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

            if (directionalLight != null)
            {
                directionalLight.color = preset.directionalColor.Evaluate(timePercent);
                directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170, 0));
            }



        }

        //Try to find directional light if one isn't set
        private void OnValidate()
        {
            if (directionalLight != null)
                return;

            //search for lighting tab sun
            if (RenderSettings.sun != null)
            {
                directionalLight = RenderSettings.sun;
            }
            //search scene for lighting that fits criteria of directional
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        directionalLight = light;
                        return;
                    }
                }
            }
        }

    }
}
