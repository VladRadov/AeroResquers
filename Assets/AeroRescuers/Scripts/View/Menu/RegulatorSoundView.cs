using UnityEngine;
using UnityEngine.UI;

public class RegulatorSoundView : MonoBehaviour
{
    [SerializeField] private Slider _regulatorSound;

    private void Start()
    {
        _regulatorSound.value = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound;
        _regulatorSound.onValueChanged.AddListener((value) => { ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound = value; });
    }
}
