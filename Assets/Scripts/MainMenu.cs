using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool showOp, mute;
    public float audioSlider, amSlider, dirSlider, volMute;
    public AudioSource audi;
    public Light dirLight;

	// Use this for initialization
	void Start ()
    {
        audi = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();
        if (PlayerPrefs.HasKey("mute"))
        {
            RenderSettings.ambientIntensity = PlayerPrefs.GetFloat("amLight");
            dirLight.intensity = PlayerPrefs.GetFloat("dirLight");
            if(PlayerPrefs.GetInt("mute") == 0)
            {
                mute = false;
                audi.volume = PlayerPrefs.GetFloat("volume");
            }
            else
            {
                mute = true;
                audi.volume = 0;
                volMute = PlayerPrefs.GetFloat("volume");
            }
        }
        audioSlider = audi.volume;
        dirSlider = dirLight.intensity;
        amSlider = RenderSettings.ambientIntensity;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(audi.volume != audioSlider)
        {
            audi.volume = audioSlider;
        }

        if(dirLight.intensity != dirSlider)
        {
            dirLight.intensity = dirSlider;
        }

        if(RenderSettings.ambientIntensity != amSlider)
        {
            RenderSettings.ambientIntensity = amSlider;
        }
	}

    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

        if (!showOp)
        {
            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 1.5f * scrH), "Tower Defence Game");

            if (GUI.Button(new Rect(3 * scrW, 5 * scrH, 3 * scrW, 2 * scrH), "Play"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
