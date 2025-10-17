using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevate : MonoBehaviour
{
    [SerializeField] private GameObject _extendBodyPart;
    [SerializeField] private GameObject _slimeHeadPart;
    [SerializeField] private GameObject _extendCollider;
    private CharacterController _characterController;

    [SerializeField] private float _extendScaleY = 3f;
    [SerializeField] private float _extendScaleSpeed = 0.5f;

    
    [SerializeField] private string playerLayer = "Player";

    private Vector3 _originalScale;
    private Vector3 _extendBodyPartScale;
    private Vector3 _extendBodyPartPosition;
    private Vector3 _extendColliderPosition;
    private Vector3 _extendSlimeHeadPosition;

    private Vector3 _originalPosition;


    void Start()
    {
        _originalScale = _extendBodyPart.transform.localScale;
        _extendBodyPartScale = _originalScale;

        _originalPosition = _extendBodyPart.transform.localPosition;
        _extendBodyPartPosition = _originalPosition;

        _extendColliderPosition = _extendCollider.transform.localPosition;

        _extendSlimeHeadPosition = _slimeHeadPart.transform.localPosition;

        _characterController = GetComponentInParent<CharacterController>();
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.transform == transform.parent)
        {
            Debug.Log("Hell no");
            return;
        }
        
        if (other.CompareTag(playerLayer)) //detect player (human and slime) that is not its parent object
        {
            ExtendSlimeOnOneSide();
        }
    }

    public void ExtendSlimeOnOneSide()
    {
        ChangeSlimeScale();
        ChangeSlimePosition();
    }

    private void ChangeSlimeScale()
    {
        //change scale

        if (_extendBodyPart.transform.localScale.y < _extendScaleY)
        {
            _extendBodyPartScale.y += _extendScaleSpeed * Time.deltaTime;
            _extendBodyPart.transform.localScale = _extendBodyPartScale;
        }
    }

    private void ChangeSlimePosition()
    {
        float deltaY = (_extendScaleY - _originalScale.y) / 2f;  //change position

        if (_extendBodyPart.transform.localPosition.y < _originalPosition.y + deltaY)
        {
            _extendBodyPartPosition.y += _extendScaleSpeed / 2 * Time.deltaTime;
            _extendBodyPart.transform.localPosition = _extendBodyPartPosition;
        }

        if (_slimeHeadPart.transform.localPosition.y < _extendScaleY * 1/5)
        {
            _extendSlimeHeadPosition.y += _extendScaleSpeed / 2 * Time.deltaTime;
            _slimeHeadPart.transform.localPosition = _extendSlimeHeadPosition;
        }

        if (_extendCollider.transform.localPosition.y < _extendScaleY / 2)
        {
            _extendColliderPosition.y += _extendScaleSpeed / 2 * Time.deltaTime;
            _extendCollider.transform.localPosition = _extendColliderPosition;
        }
    }
}
