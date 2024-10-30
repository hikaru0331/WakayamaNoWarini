using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private void Start()
    {
        bgmSlider.value = AudioManager.instance_AudioManager.BGMVolume;
        seSlider.value = AudioManager.instance_AudioManager.SEVolume;

        // スライダーのイベントにリスナーを追加
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
    }

    private void OnBGMVolumeChanged(float value)
    {
        AudioManager.instance_AudioManager.BGMVolume = value;
    }

    private void OnSEVolumeChanged(float value)
    {
        AudioManager.instance_AudioManager.SEVolume = value;
    }
}
