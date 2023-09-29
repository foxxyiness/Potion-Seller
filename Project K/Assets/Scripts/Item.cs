using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private bool _isGrounded;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            rb.velocity = Vector3.zero;
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    private enum Type
    {
        Light,
        Healing,
        Poison,
        Death
    }

    [SerializeField]
    private Type type = Type.Light;
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;

    public string GetName()
    {
        return name;
    }
    
}
