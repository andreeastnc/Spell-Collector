using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text volumeText = null;
    // Start is called before the first frame update

    private void Start()
    {
        LoadValues();
    }
    public void VolumeSlider(float volume)
    {
        volumeText.text = volume.ToString("0");
    }
    
    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeText.text = volumeValue.ToString();
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
