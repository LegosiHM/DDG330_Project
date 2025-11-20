using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeMovementSO", menuName = "ScriptableObjects/SlimeMovementSO")]
public class SlimeMovementSO : ScriptableObject
{
    [SerializeField] private float _speed = 5f;
    public float speed => _speed;

    [SerializeField] private float _jumpHeight = 0.5f;
    public float jumpHeight => _jumpHeight;

    [SerializeField] private float _climbSpeed = 3;
    public float climbSpeed => _climbSpeed;
}
