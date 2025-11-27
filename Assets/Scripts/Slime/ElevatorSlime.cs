using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSlime : MonoBehaviour
{
    [SerializeField] private float _maxMoveDistance = 5f;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _moveDelay = 1f;
    private float _moveDelayCount;
    [SerializeField] private string playerLayer = "Player";

    private Vector3 _newPosition;
    private bool _isInsideTrigger = false;

    private Slime slimeComponent;
    private Vector3 _originalPosition => slimeComponent.slimeOriginalPosition;
    private Vector3 _previousPosition;

    private bool isLevitationPlaying = false;


    void Start()
    {
        slimeComponent = GetComponent<Slime>();
        _moveDelayCount = _moveDelay;
    }

    private void Update()
    {
        if (slimeComponent.isDead)
        {

            if (!_isInsideTrigger && transform.position != _originalPosition) //try to move back if not inside trigger
            {
                if(_moveDelayCount <= 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _originalPosition, _moveSpeed * Time.deltaTime);
                }
                else
                {
                    _moveDelayCount -= Time.deltaTime;
                    _moveDelayCount = Mathf.Clamp(_moveDelayCount, 0, _moveDelay);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject) //make sure to not detect itself
        {
            return;
        }

        if (other.CompareTag(playerLayer)) //detect player (human and slime) that is not its parent object
        {
            _isInsideTrigger = true;
            _moveDelayCount = 1;
            other.transform.SetParent(transform); //change player parent so it move along smoothly with the slime
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == gameObject)
            return;

        if (other.CompareTag(playerLayer))
        {
            float currentDistance = Vector3.Distance(_originalPosition, transform.position);

            if (_moveDelayCount <= 0)
            {
                if (currentDistance < _maxMoveDistance)
                {
                    if (!isLevitationPlaying)
                    {
                        SoundManager.Instance.PlayContinuous("slime_levitate", 1f);
                        isLevitationPlaying = true;
                    }

                    float remainingDistance = _maxMoveDistance - currentDistance;
                    float moveStep = MathF.Min(_moveSpeed * Time.deltaTime, remainingDistance);

                    transform.Translate(Vector3.up * moveStep, Space.Self);

                    Vector3 movementDelta = transform.position - _previousPosition;

                    CharacterController controller = other.GetComponent<CharacterController>();
                    if (controller != null)
                    {
                        controller.Move(movementDelta);
                    }
                }
                else
                {
                    if (isLevitationPlaying)
                    {
                        SoundManager.Instance.StopContinuous("slime_levitate");
                        isLevitationPlaying = false;
                    }
                }
            }
            else
            {
                _moveDelayCount -= Time.deltaTime;
                _moveDelayCount = Mathf.Clamp(_moveDelayCount, 0, _moveDelay);
            }
        }

        _previousPosition = transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gameObject)
            return;

        if (other.CompareTag(playerLayer))
        {
            _isInsideTrigger = false;
            _moveDelayCount = 1;
            other.transform.SetParent(null);

            if (isLevitationPlaying)
            {
                SoundManager.Instance.StopContinuous("slime_levitate");
                isLevitationPlaying = false;
            }
        }
    }

}
