using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

public class Interactor : MonoBehaviour
{

    [SerializeField] private FloatReference interactRadius;
    [SerializeField] private InputConfig interactInput;
    private Collider[] _collidersInRange;
    private List<Interactable> _interactablesInRange;
    private Interactable _closestInteractable;




   
    private void Start()
    {
        _interactablesInRange = new List<Interactable>();
    }

    private void FixedUpdate()
    {
        if (interactInput.GetDown())
        {
            Debug.Log("Interacting");
            Interact();
        }
    }

    private void Interact()
    {
        if (_closestInteractable != null)
        {
            _closestInteractable.Interact();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
