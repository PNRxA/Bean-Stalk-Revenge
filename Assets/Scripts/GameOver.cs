using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{ 
    public GUIStyle style1;
    public GUIStyle style2;
    public GUIStyle style3;

    void OnGUI()
    {
        float scrW = Screen.width / 16f;
        float scrH = Screen.height / 9f;

        GUI.skin.button = style1;
        GUI.skin.box = style2;
        GUI.skin.textField = style3;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            GUI.TextField(new Rect(4f * scrW, 0.25f * scrH, 8f * scrW, 1.5f * scrH), "Game Over");

            if (GUI.Button(new Rect(3f * scrW, 5f * scrH, 3f * scrW, 2f * scrH), "Restart"))
            {
            SceneManager.LoadScene(1);
            GameManager.health = 101;
            }

            // Reload back to the main menu
            if (GUI.Button(new Rect(6.7f * scrW, 5.5f * scrH, 3f * scrW, 1.8f * scrH), "Main Menu"))
            {
                SceneManager.LoadScene(0);
            }

            if (GUI.Button(new Rect(11f * scrW, 5f * scrH, 3f * scrW, 2f * scrH), "Quit"))
            {
                Application.Quit();
            }
        }
    }

