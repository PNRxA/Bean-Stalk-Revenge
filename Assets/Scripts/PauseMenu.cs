using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool showOp, mute, paused;
    public float audioSlider, volMute;
    public AudioSource audi;

    public GUIStyle style1;
    public GUIStyle style2;
    public GUIStyle style3;

    // Use this for initialization
    void Start()
    {
        audi = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("mute"))
        {
            if (PlayerPrefs.GetInt("mute") == 0)
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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (audi.volume != audioSlider)
        {
            audi.volume = audioSlider;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (showOp == true)
            {
                showOp = false;
            }
            else
            {
                TogglePause();
            }
        }
    }

    void OnGUI()
    {
        float scrW = Screen.width / 16f;
        float scrH = Screen.height / 9f;

        GUI.skin.button = style1;
        GUI.skin.box = style2;
        GUI.skin.textField = style3;

        if (paused)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
   
            if (!showOp)
            {
                GUI.TextField(new Rect(4f * scrW, 0.25f * scrH, 8f * scrW, 1.5f * scrH), "Paused");

                if (GUI.Button(new Rect(3f * scrW, 5f * scrH, 3f * scrW, 2f * scrH), "Return"))
                {
                   TogglePause();
                }

                if (GUI.Button(new Rect(7f * scrW, 5f * scrH, 3f * scrW, 2f * scrH), "Options"))
                {
                    showOp = true;
                }

                // Reload back to the main menu
                if (GUI.Button(new Rect(6.5f * scrW, 7f * scrH, 3 * scrW, scrH), "Main Menu"))
                {
                    SceneManager.LoadScene(0);
                }

                if (GUI.Button(new Rect(11f * scrW, 5f * scrH, 3f * scrW, 2f * scrH), "Quit"))
                {
                    Application.Quit();
                }
            }
            else
            {
                if (!mute)
                {
                    audioSlider = GUI.HorizontalSlider(new Rect(5f * scrW, 3f * scrH, 5f * scrW, 0.5f * scrH), audioSlider, 0f, 1f);
                }
                else
                {
                    GUI.HorizontalSlider(new Rect(5f * scrW, 3f * scrH, 5f * scrW, 0.5f * scrH), audioSlider, 0f, 1f);
                }

                GUI.TextField(new Rect(2f * scrW, 2.8f * scrH, 3f * scrW, 0.5f * scrH), "Volume");

                if (GUI.Button(new Rect(2f * scrW, 1f * scrH, 2f * scrW, 1f * scrH), "Back"))
                {
                    SaveOptions();
                    showOp = false;
                }

                if (GUI.Button(new Rect(5f * scrW, 1f * scrH, 2f * scrW, 1f * scrH), "Mute"))
                {
                    ToggleMute();
                }
            }
        }
    }

    void SaveOptions()
    {
        if (!mute)
        {
            PlayerPrefs.SetInt("mute", 0);
            PlayerPrefs.SetFloat("volume", audioSlider);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 1);
            PlayerPrefs.SetFloat("volume", volMute);
        }
    }

    bool ToggleMute()
    {
        if (mute)
        {
            audioSlider = volMute;
            mute = false;
            return false;
        }
        else
        {
            volMute = audioSlider;
            audioSlider = 0;
            mute = true;
            return true;
        }
    }

    bool TogglePause()
    {
        if (paused)
        {
            paused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            return false;
        }
        else
        {
            paused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            return true;
        }
    }
}
