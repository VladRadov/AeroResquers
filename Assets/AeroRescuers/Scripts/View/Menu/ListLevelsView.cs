using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListLevelsView : MonoBehaviour
{
    [SerializeField] private LevelItemView _levelItemViewPrefab;
    [SerializeField] private Transform _contentPanel;
    [SerializeField] private List<LevelEntity> _levels;
    [SerializeField] private Button _openListLevels;
    [SerializeField] private Sprite _background;
    [SerializeField] private int _startNumberLevel;
    [SerializeField] private int _endNumberLevel;
    [Header("Открыть все уровни")]
    [SerializeField] private bool _isOpenAllLevels;

    public void SetActive(bool value)
        => _contentPanel.parent.gameObject.SetActive(value);

    private void Start()
    {
        var maxOpenedLevel = ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxOpenedLevel;
        CreateItemLevels(_isOpenAllLevels ? _endNumberLevel : maxOpenedLevel);
        _openListLevels.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            SetActive(true);
        });

        if (TryOpenedLocation() == false)
            _openListLevels.enabled = false;
    }

    private bool TryOpenedLocation()
        => ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxOpenedLevel >= _startNumberLevel;

    private void CreateItemLevels(int maxLevel)
    {
        int count = 1;
        for (int i = _startNumberLevel; i <= maxLevel; i++)
        {
            var levelItem = Instantiate(_levelItemViewPrefab, _contentPanel);
            levelItem.SetViewNumberLevel(count);
            levelItem.SetNumberLevel(i);
            levelItem.SetBackground(_background);
            ++count;
        }
    }
}
