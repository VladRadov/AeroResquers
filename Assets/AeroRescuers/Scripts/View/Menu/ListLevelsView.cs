using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListLevelsView : MonoBehaviour
{
    [SerializeField] private LevelItemView _levelItemViewPrefab;
    [SerializeField] private Transform _contentPanel;
    [SerializeField] private List<LevelEntity> _levels;
    [SerializeField] Button _back;
    [Header("Открыть все уровни")]
    [SerializeField] private bool _isOpenAllLevels;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxLevel = _levels.Count;
        var maxOpenedLevel = ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxOpenedLevel;
        CreateItemLevels(_isOpenAllLevels ? _levels.Count : maxOpenedLevel);
        _back.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            SetActive(false);
        });
    }

    private void CreateItemLevels(int maxLevel)
    {
        for (int i = 1; i <= maxLevel; i++)
        {
            var levelItem = Instantiate(_levelItemViewPrefab, _contentPanel);
            levelItem.SetNumberLevel(i);
        }
    }
}
