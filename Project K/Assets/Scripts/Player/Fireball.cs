using System;
using UnityEngine;

namespace Player
{
    public class Fireball : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            audioSource.Play();
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(this.gameObject);
        }
    }
}
