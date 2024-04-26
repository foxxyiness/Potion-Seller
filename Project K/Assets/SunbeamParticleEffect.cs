using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunbeamParticleEffect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
