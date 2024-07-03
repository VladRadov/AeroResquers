using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelEntity _currentLevel;

    [SerializeField] private List<LevelEntity> _levels;

    public LevelEntity CurrentLevel => _currentLevel;

    private void Awake()
    {
        var findedLevel =_levels.Find(level => level.LevelNumber == ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentLevel);
        _currentLevel = findedLevel;
    }
}
