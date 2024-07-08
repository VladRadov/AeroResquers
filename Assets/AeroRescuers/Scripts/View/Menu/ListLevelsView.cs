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
    [Header("Открыть все уровни")]
    [SerializeField] private bool _isOpenAllLevels;

    public void SetActive(bool value)
        => _contentPanel.parent.gameObject.SetActive(value);

    private void Start()
    {
        if (_isOpenAllLevels)
            ViewAllItemLevels();
        else
            ViewItemLevels();

        _openListLevels.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            SetActive(true);
        });
    }

    private void ViewItemLevels()
    {
        int count = 1;
        var openedLevels = ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenedLevels;
        for (int i = 0; i < _levels.Count; i++)
        {
            if (openedLevels.Contains(_levels[i].LevelNumber + ";"))
            {
                CreateLevelItem(count, _levels[i].LevelNumber);
                ++count;
            }
        }
    }

    private void ViewAllItemLevels()
    {
        int count = 1;
        for (int i = 0; i < _levels.Count; i++)
        {
            CreateLevelItem(count, _levels[i].LevelNumber);
            ++count;
        }
    }

    private void CreateLevelItem(int numberLevelView, int numberLevel)
    {
        var levelItem = Instantiate(_levelItemViewPrefab, _contentPanel);
        levelItem.SetViewNumberLevel(numberLevelView);
        levelItem.SetNumberLevel(numberLevel);
        levelItem.SetBackground(_background);
    }
}
