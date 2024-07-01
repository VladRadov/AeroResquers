using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour
{
    [Header("Кол-во спасенных парашютистов")]
    [SerializeField] private Text _viewCountSavedSkydrivers;
    [SerializeField] private Slider _countSavedSkydrivers;
    [Header("Кол-во собранных монет")]
    [SerializeField] private Text _viewCountMoney;

    public void SetMaxSaveSkydrivers(int count)
        => _countSavedSkydrivers.maxValue = count;

    public void IncreaseCountSkydrivers()
    {
        var countSavedSkydrivers = int.Parse(_viewCountSavedSkydrivers.text) + 1;
        _viewCountSavedSkydrivers.text = countSavedSkydrivers.ToString();
        _countSavedSkydrivers.value = countSavedSkydrivers;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver = countSavedSkydrivers;
    }

    public void IncreaseCountMoney()
    {
        var countMoney = int.Parse(_viewCountMoney.text) + 1;
        _viewCountMoney.text = countMoney.ToString();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.GameMoney = countMoney;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Money += 1;
    }

    private void Start()
    {
        _viewCountSavedSkydrivers.text = "0";
        _countSavedSkydrivers.value = 0;

        _viewCountMoney.text = "0";
        ContainerSaveerPlayerPrefs.Instance.SaveerData.GameMoney = 0;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver = 0;
    }
}
