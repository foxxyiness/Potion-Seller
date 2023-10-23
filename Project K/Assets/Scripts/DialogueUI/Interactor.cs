using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Variables;

public class Interactor : MonoBehaviour
{

    [SerializeField] private FloatReference interactRadius;
    [SerializeField] private InputAction inputAction;
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
    private void OnTriggerEnter(Collider other)
    {
        if (_closestInteractable != null)
        {
            UpdateInteractables();
        }
    }
    private void OnTriggerExit(Collider other) 
    {  
        if (_closestInteractable != null)
        {
            UpdateInteractables();
        } 
    }
    private void UpdateInteractables()
    {
        _collidersInRange = Physics.OverlapSphere(transform.position, interactRadius.Value);
        _interactablesInRange.Clear();
        if (_collidersInRange == null || _collidersInRange.Length == 0)
            return;
        foreach (var colliderInRange in _collidersInRange)
        {
            var interactable = colliderInRange.GetComponentInParent<Interactable>();
            if (interactable != null)
            {
                _interactablesInRange.Add(interactable);
            }
        }
        _closestInteractable = GetClosestInteractable();
    }
    private Interactable GetClosestInteractable()
    {
        if (_interactablesInRange.Count <= 0) return null;

        int closestInteractableIndex = 0;
        var closestDistance = Mathf.Infinity;
        for(int i = 0; i < _interactablesInRange.Count; i++)
        {
            var distance = Vector3.Distance(_interactablesInRange[i].transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractableIndex = i;
            }

        }
        return _interactablesInRange[closestInteractableIndex];
    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
