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
            if (other.collider.CompareTag("TimeForward"))
            {
                other.gameObject.GetComponent<TimeForward>().DoTimeForward();
                Destroy(gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
