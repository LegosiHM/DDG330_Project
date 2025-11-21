using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeIconSO", menuName = "ScriptableObjects/SlimeTypeSO")]
public class SlimeIconSO : ScriptableObject
{
    [SerializeField] private List<GameObject> _icons = new List<GameObject>();
    public List<GameObject> icons => _icons;
}
