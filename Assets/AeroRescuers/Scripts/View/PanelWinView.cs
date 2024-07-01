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
    }

    private void OnNextLevel()
    {
        
    }

    private void OnQuit()
    {
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
    }
}
