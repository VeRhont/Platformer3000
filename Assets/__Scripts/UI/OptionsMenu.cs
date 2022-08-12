using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private Resolution[] _resolutions;

    private void Start()
    {
        _resolutions = Screen.resolutions;

        _resolutionDropdown.ClearOptions();

        var options = new List<string>();

        var currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            var tempOption = $"{_resolutions[i].width} x {_resolutions[i].height}";
            options.Add(tempOption);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        var resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
