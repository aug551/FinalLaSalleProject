using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resolutions : MonoBehaviour
{
    public TMP_Dropdown dropdownMenu;   
    Resolution[] resolutions;   

    private void Start()
    {
        resolutions = Screen.resolutions;   

        dropdownMenu.ClearOptions();    

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)    
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        dropdownMenu.AddOptions(options);
    }

    public void quality(int index) 
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void fullscreen(bool isActive) 
    {
        Screen.fullScreen = isActive;
    }

    public void resolution(int index)   
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
