using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool showOp, mute, resolution, controls, paused;
    public float audioSlider, volMute;
    public bool res1, res2, res3, res4, fullScreen, windowed;
    public KeyCode forward, backward, right, left;
    public KeyCode holdingKey;
    public AudioSource audi;

    public GUIStyle style;
    public GUIStyle style2;

    // Use this for initialization
    void Start()
    {
        // Set the audi source
        audi = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("mute"))
        {
            // If mute is 0, then audi's Audio Source is set to a float known as volume
            if (PlayerPrefs.GetInt("mute") == 0)
            {
                mute = false;
                audi.volume = PlayerPrefs.GetFloat("volume");
            }
            else
            {
                // Otherwise mute is set to 1, which becomes true
                mute = true;
                audi.volume = 0;
                volMute = PlayerPrefs.GetFloat("volume");
            }
        }
        res3 = true;
        fullScreen = true;
        forward = KeyCode.W;
        backward = KeyCode.S;
        left = KeyCode.A;
        right = KeyCode.D;
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
        // Screen apsect ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        // GUI Skins
        GUI.skin.button = style;
        GUI.skin.box = style2;

        if (paused)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            if (!showOp)
            {
                // Game title box
                GUI.Box(new Rect(4 * scrW, 1.8f * scrH, 8 * scrW, 2 * scrH), "Paused");

                // Load scene via play button
                if (GUI.Button(new Rect(5 * scrW, 4 * scrH, 6 * scrW, scrH), "Return"))
                {
                    TogglePause();
                }

                // When true, options is displayed
                if (GUI.Button(new Rect(5.5f * scrW, 5 * scrH, 5 * scrW, 0.75f * scrH), "Options"))
                {
                    showOp = true;
                }

                // Reload back to the main menu
                if (GUI.Button(new Rect(6.5f * scrW, 6.3f * scrH, 3 * scrW, scrH), "Main Menu"))
                {
                    SceneManager.LoadScene(0);
                }

                // Quit the game
                if (GUI.Button(new Rect(6 * scrW, 5.75f * scrH, 4 * scrW, 0.5f * scrH), "Quit"))
                {
                    Application.Quit();
                }
            }
            else
            {
                // ToggleMute
                if (GUI.Button(new Rect(8 * scrW, 1.2f * scrH, scrW, 0.5f * scrH), "Mute"))
                {
                    ToggleMute();
                }

                // Audio Slider
                GUI.Box(new Rect(0.5f * scrW, 0.5f * scrH, 7.125f * scrW, 1f * scrH), "Audio"); // Title for audio 

                audioSlider = GUI.HorizontalSlider(new Rect(0.5f * scrW, 1.4f * scrH, 7.125f * scrW, 0.25f * scrH), audioSlider, 0.0F, 1.0F); // Audio Horizontal slider

                GUI.Label(new Rect(4f * scrW, 1.5f * scrH, 0.75f * scrW, 0.25f * scrH), Mathf.FloorToInt(audioSlider * 100).ToString()); // 0 out of 100

                if (GUI.Button(new Rect(14 * scrW, 8.5f * scrH, 2 * scrW, 0.5f * scrH), "Back"))
                {
                    SaveOptions();
                    showOp = false;
                }

                // Resolution
                GUI.Box(new Rect(0.5f * scrW, 2.9f * scrH, 7.125f * scrW, 6 * scrH), "Resolutions");

                #region Resolution Types
                if (GUI.Toggle(new Rect(1.5f * scrW, 3.9f * scrH, 2f * scrW, 0.5f * scrH), res1, "1024 x 576"))
                {
                    Screen.SetResolution(1024, 576, fullScreen); // Screen resolution is set to 1024 x 576, fullscreen

                    res1 = true; // res1 is true, while all others are false

                    res2 = false;

                    res3 = false;

                    res4 = false;
                }

                if (GUI.Toggle(new Rect(1.5f * scrW, 4.4f * scrH, 2f * scrW, 0.5f * scrH), res2, "1366 x 768"))
                {
                    Screen.SetResolution(1366, 768, fullScreen); //Screen resolution is set to 1366 x 768, fullscreen

                    res2 = true; // res2 is true, while all others are false

                    res1 = false;

                    res3 = false;

                    res4 = false;
                }

                if (GUI.Toggle(new Rect(1.5f * scrW, 4.9f * scrH, 2f * scrW, 0.5f * scrH), res3, "1600 x 900"))
                {
                    Screen.SetResolution(1600, 900, fullScreen); // 900p, fullscreen

                    res3 = true; // res3 is true, all remainders are false

                    res1 = false;

                    res2 = false;

                    res4 = false;
                }

                if (GUI.Toggle(new Rect(1.5f * scrW, 5.4f * scrH, 2 * scrW, 0.5f * scrH), res4, "1920 x 1080"))
                {
                    Screen.SetResolution(1920, 1080, fullScreen); // 1080p, fullscreen

                    res4 = true; // res4 is true, all remainders are false

                    res1 = false;

                    res2 = false;

                    res3 = false;
                }
                #endregion

                if (GUI.Toggle(new Rect(5.5f * scrW, 3.9f * scrH, 2 * scrW, 0.5f * scrH), fullScreen, "FullScreen"))
                {
                    Screen.fullScreen = true; // Fullscreen enabled when true

                    fullScreen = true; // When true, GUI may be filled

                    windowed = false; // When false window is unselected
                }

                if (GUI.Toggle(new Rect(5.5f * scrW, 4.4f * scrH, 2 * scrW, 0.5f * scrH), windowed, "Windowed"))//when pressed
                {
                    Screen.fullScreen = false; // Set fullscreen to false

                    fullScreen = false; // False, fullscreen unselected

                    windowed = true; // When true, changes to windowed and GUI can be filled
                }

                // Controls
                GUI.Box(new Rect(8.4f * scrW, 1.1f * scrH, 8.2f * scrW, 8f * scrH), "Controls");

                Event e = Event.current;

                #region GUI Buttons
                // Forward
                if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None))
                {
                    if (GUI.Button(new Rect(14f * scrW, 2f * scrH, 1f * scrW, 1f * scrH), forward.ToString()))
                    {
                        holdingKey = forward; // Set holdingKey to specified key

                        forward = KeyCode.None; // None, only this button can be edited
                    }
                }
                else
                {
                    GUI.Box(new Rect(14f * scrW, 1f * scrH, 1f * scrW, 1f * scrH), forward.ToString());
                }

                // Backward
                if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None))
                {
                    if (GUI.Button(new Rect(14f * scrW, 3f * scrH, 1f * scrW, 1f * scrH), backward.ToString()))
                    {
                        holdingKey = backward;

                        backward = KeyCode.None;
                    }
                }
                else
                {
                    GUI.Box(new Rect(14f * scrW, 2f * scrH, 1f * scrW, 1f * scrH), backward.ToString());
                }

                // Left
                if (!(forward == KeyCode.None || backward == KeyCode.None || right == KeyCode.None))
                {
                    if (GUI.Button(new Rect(14f * scrW, 4f * scrH, 1f * scrW, 1f * scrH), left.ToString()))
                    {
                        holdingKey = left;

                        left = KeyCode.None;
                    }
                }
                else
                {
                    GUI.Box(new Rect(14f * scrW, 3f * scrH, 1f * scrW, 1f * scrH), left.ToString());
                }

                // Right
                if (!(forward == KeyCode.None || left == KeyCode.None || backward == KeyCode.None))
                {
                    if (GUI.Button(new Rect(14f * scrW, 5f * scrH, 1f * scrW, 1f * scrH), right.ToString()))
                    {
                        holdingKey = right;

                        right = KeyCode.None;
                    }
                }
                #endregion

                #region Key Change
                if (forward == KeyCode.None)
                {
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode); // Write in console what the key represents

                        if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right))

                        // If that key is not the same as any other movements key press than
                        {
                            forward = e.keyCode; // Set forward to the events key press

                            holdingKey = KeyCode.None; // Set the holding key to none
                        }
                        else
                        {
                            forward = holdingKey; // Set forward back to what holding key is now

                            holdingKey = KeyCode.None; // Set the holding key to none
                        }
                    }
                }

                if (backward == KeyCode.None)
                {
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);

                        if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right))
                        {
                            backward = e.keyCode;

                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            backward = holdingKey;

                            holdingKey = KeyCode.None;
                        }
                    }
                }

                if (left == KeyCode.None)
                {
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);

                        if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == right))
                        {
                            left = e.keyCode;

                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            left = holdingKey;

                            holdingKey = KeyCode.None;
                        }
                    }
                }

                if (right == KeyCode.None)
                {
                    if (e.isKey)
                    {
                        Debug.Log("Detected key code: " + e.keyCode);

                        if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left))
                        {
                            right = e.keyCode;

                            holdingKey = KeyCode.None;
                        }
                        else
                        {
                            right = holdingKey;

                            holdingKey = KeyCode.None;
                        }
                    }
                }
                #endregion

                // GUI Control Button Labels
                GUI.Box(new Rect(8.75f * scrW, 2.2f * scrH, 6.25f * scrW, 1f * scrH), "Forward");

                GUI.Box(new Rect(8.75f * scrW, 3.2f * scrH, 6.25f * scrW, 1f * scrH), "Backwards");

                GUI.Box(new Rect(8.75f * scrW, 4.2f * scrH, 6.25f * scrW, 1f * scrH), "Left");

                GUI.Box(new Rect(8.75f * scrW, 5.2f * scrH, 6.25f * scrW, 1f * scrH), "Right");

            }
        }
    }


    void SaveOptions()
    {
        if (!mute)
        {
            // If not muted value is 0
            // False
            // Set volume to audioSlider
            PlayerPrefs.SetInt("mute", 0);
            PlayerPrefs.SetFloat("volume", audioSlider);
        }
        else
        {
            // Otherwise we are muted, value is 1
            // True
            // Set volume to volMute
            PlayerPrefs.SetInt("mute", 1);
            PlayerPrefs.SetFloat("volume", volMute);
        }
    }

    bool ToggleMute()
    {
        // If muted, our audioSlider is set to a placeholder volume value, then unmute
        if (mute)
        {
            audioSlider = volMute;
            mute = false;
            return false;
        }
        else
        {
            // Otherwise we are unmuted, volume is equal to that of our current volume, then mute
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
