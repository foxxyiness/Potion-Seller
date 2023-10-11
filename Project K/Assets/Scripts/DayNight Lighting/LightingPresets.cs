using UnityEngine;
using UnityEngine.Serialization;

namespace DayNight_Lighting
{
    [CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preset", order =1)]
    public class LightingPresets : ScriptableObject
    {


        [FormerlySerializedAs("AmbientColor")] public Gradient ambientColor;
        [FormerlySerializedAs("DirectionalColor")] public Gradient directionalColor;
        [FormerlySerializedAs("FogColor")] public Gradient fogColor;
        
    }
}
