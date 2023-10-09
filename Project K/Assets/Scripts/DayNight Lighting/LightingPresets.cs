using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preset", order =1)]
public class LightingPresets : ScriptableObject
{


    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;



    // Start is called before the first frame update
    void Start()
    {
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
