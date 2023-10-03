using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isGrounded { get; private set; }
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _rb.velocity = Vector3.zero;
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private enum Difficulty
    {
        Easy,
        Medium,
        Hard
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
    [SerializeField] 
    private Difficulty difficulty = Difficulty.Easy;
    public string GetName()
    {
        return name;
    }
    
}
