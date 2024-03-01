using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class Fountain : MonoBehaviour
{
    [SerializeField] private GameObject waterOrb;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float waterSpawnRate;
    private bool _canSpawnWater;

    private void Start()
    {
        _canSpawnWater = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fire") && _canSpawnWater)
        {
            _canSpawnWater = false;
            StartCoroutine(SpawnWater());
        }
    }

    private IEnumerator SpawnWater()
    {
        Instantiate(waterOrb, spawnPoint.transform.position, quaternion.identity);
        Debug.Log("WATER SPAWNED");
        yield return new WaitForSeconds(waterSpawnRate);
        _canSpawnWater = true;
    }
}
