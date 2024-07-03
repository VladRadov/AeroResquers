using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanelView : MonoBehaviour
{
    [SerializeField] private Button _back;
    [SerializeField] private Button _infinityGame;
    [SerializeField] private Button _storyGame;
    [SerializeField] private ListLevelsView _listLevelsView;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _back.onClick.AddListener(() => { SetActive(false); });
        _infinityGame.onClick.AddListener(() => { OnEntryStoryGame(TypeGame.Infinity); });
        _storyGame.onClick.AddListener(() => { OnEntryStoryGame(TypeGame.Story); });
    }

    private void OnEntryStoryGame(TypeGame typeGame)
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.TypeGame = (int)typeGame;
        _listLevelsView.SetActive(true);
    }
}
