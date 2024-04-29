using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pages
{
    [CreateAssetMenu]
    public class LostPagesList : ScriptableObject
    {
        [SerializeField] private List<Vector3> lostPageLocations;


        public List<Vector3> GetLostPages()
        {
            return lostPageLocations;
        }
    }
}
