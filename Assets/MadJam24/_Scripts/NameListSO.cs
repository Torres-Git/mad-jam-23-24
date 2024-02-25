using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameList", menuName = "NameList")]
public class NameListSO : ScriptableObject
{
    [SerializeField] string[] _names;

    public string[] Names { get => _names;  }
}
