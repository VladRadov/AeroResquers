using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelWinView : MonoBehaviour
{
    [SerializeField] private Button _nextLevel;
    [SerializeField] private Button _quit;
    [SerializeField] private Text _money;
    [SerializeField] private Text _countSkydriver;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _nextLevel.onClick.AddListener(OnNextLevel);
        _quit.onClick.AddListener(OnQuit);

        Initialize();
    }

    private void Initialize()
    {
        _money.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.GameMoney.ToString();
        _countSkydriver.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver.ToString();

        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentLevel == ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxLevel)
            _nextLevel.enabled = false;
    }

    private void OnNextLevel()
    {
        AudioManager.Instance.PlayClickButton();
        if(ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentLevel >= ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxOpenedLevel)
            ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxOpenedLevel = ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxOpenedLevel + 1;

        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentLevel = ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentLevel + 1;
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }

    private void OnQuit()
    {
        AudioManager.Instance.PlayClickButton();
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
    }
}
