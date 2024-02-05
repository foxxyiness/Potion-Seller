using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pages
{
    [CreateAssetMenu]
    public class LostPagesList : ScriptableObject
    {
        [SerializeField] private List<Vector3> mediumLostPages;


        public List<Vector3> getMediumPages()
        {
            return mediumLostPages;
        }
    }
}
