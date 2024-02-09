using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Fire") && _canPlay)
        {
            _canPlay = false;
            StartCoroutine(PlaySound);
        }
    }


    private IEnumerator PlaySound()
    {
        
        yield return new WaitForSeconds(2.0F);
    }
}
