using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItemView : MonoBehaviour
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private Button _play;
    [SerializeField] private Text _numberLevelView;

    public void SetNumberLevel(int number)
        => _numberLevel = number;

    private void Start()
    {
        _numberLevelView.text = _numberLevel.ToString();
        _play.onClick.AddListener(OnPlay);
    }

    private void OnPlay()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentLevel = _numberLevel;
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }
}
