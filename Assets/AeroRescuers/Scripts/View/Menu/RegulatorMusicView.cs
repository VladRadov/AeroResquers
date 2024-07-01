using UnityEngine;
using UnityEngine.UI;

public class RegulatorMusicView : MonoBehaviour
{
    [SerializeField] private Slider _regulatorMusic;

    private void Start()
    {
        _regulatorMusic.value = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic;
        _regulatorMusic.onValueChanged.AddListener((value) => { ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic = value; });
    }
}
