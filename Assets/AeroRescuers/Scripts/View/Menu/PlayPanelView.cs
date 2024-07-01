using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanelView : MonoBehaviour
{
    [SerializeField] private Button _back;
    [SerializeField] private Button _infinityGame;
    [SerializeField] private Button _storeGame;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _back.onClick.AddListener(() => { SetActive(false); });
        _infinityGame.onClick.AddListener(() => { OnEntryGame(TypeGame.Infinity); });
        _storeGame.onClick.AddListener(() => { OnEntryGame(TypeGame.Story); });
    }

    private void OnEntryGame(TypeGame typeGame)
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.TypeGame = (int)typeGame;
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }
}
