using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] private Transform _linkedObject;
    [SerializeField] private Vector3 _movedBy;
    [SerializeField] private float _linkedObjectMoveSpeed = 5f;

    private Vector3 _objectOriginalPosition;
    private Vector3 _objectNewPosition;

    private bool _isInsideTrigger = false;

    void Start()
    {
        _objectOriginalPosition = _linkedObject.transform.position;
        _objectNewPosition = _objectOriginalPosition + _movedBy;
    }

    void Update()
    {
        if (!_isInsideTrigger && _linkedObject.transform.position != _objectOriginalPosition)
        {
            _linkedObject.transform.position = Vector3.MoveTowards(_linkedObject.transform.position, _objectOriginalPosition, _linkedObjectMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        SoundManager.Instance.PlaySFX("button_press");
        _isInsideTrigger = true;
    }

    
    private void OnTriggerStay(Collider other)
    {
        _linkedObject.transform.position = Vector3.MoveTowards(_linkedObject.transform.position, _objectNewPosition, _linkedObjectMoveSpeed * Time.deltaTime);
    }
    

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        _isInsideTrigger = false;
        //_linkedObject.transform.position = Vector3.MoveTowards(_linkedObject.transform.position, _objectOriginalPosition, _linkedObjectMoveSpeed * Time.deltaTime);
    }
}
