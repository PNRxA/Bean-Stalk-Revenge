using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Starting money
    public static int Money = 500;
    // Are you in a wave?
    public static bool inWave = false;
    // Health
    public static int health = 100;

    void Update()
    {
        Health();

        Pause();
    }

    void Health()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(2);
        }
    }
}
