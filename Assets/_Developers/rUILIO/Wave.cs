using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Waves")]
public class Wave : ScriptableObject
{
    [SerializeField] List<Entity> _entities;
    [SerializeField] List<string> _narrationStrings;
    [SerializeField] float _durationInSeconds;
    [SerializeField] int _bulletsNumber;
    [SerializeField] bool _topConvexWall;
    [SerializeField] bool _bottomConvexWall;
    [SerializeField] bool _leftConvexWall;
    [SerializeField] bool _rightConvexWall;

    public List<Entity> Entities { get => _entities; }
    public List<string> narrationStrings { get => _narrationStrings; }
    public float durationInSeconds { get => _durationInSeconds; }
    public int bulletsNumber { get => _bulletsNumber; }
    public bool topConvexWall { get => _topConvexWall; }
    public bool bottomConvexWall { get => _bottomConvexWall; }
    public bool leftConvexWall { get => _leftConvexWall; }
    public bool rightConvexWall { get => _rightConvexWall; }
}
