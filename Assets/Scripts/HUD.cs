using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public bool placingTower = false;
    public GameObject[] towers;
    public float activeTowerSpeed = 3.0f;

    private GameObject activeTower;
    private Vector3 activeTowerTargetPos;
    private float scrW;
    private float scrH;
    private RaycastHit hit;
    private Ray ray;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If clicking and not placing a tower then select the tower
        if (Input.GetMouseButtonDown(0) && !placingTower)
        {
            SelectTower();
        }
        // If placing a tower, stick the tower to the cursor
        if (placingTower)
        {
            TowerToCursor();
        }
    }

    void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;
        GUI.BeginGroup(new Rect(scrW * 12, 0, scrW * 4.18f, scrH * 10.05f));

        GUI.Box(new Rect(0, 0, scrW * 4.18f, scrH * 10.05f), "");
        // Only show wave info if countdown is above 0
        string waveInfo;
        if (WaveSpawner.countdown > 0)
        {
            waveInfo = "\nNext Wave: " + WaveSpawner.countdown;
        }
        else
        {
            waveInfo = null;
        }

        GUI.Box(new Rect(0, 0, scrW * 4.18f, scrH * 1.2f), "Buy Towers \nMoney: " + GameManager.Money + waveInfo);

        if (GUI.Button(new Rect(scrW, scrH * 2, scrW * 3, scrH), "Basic Tower \n250 Beans"))
        {
            CreateTower(0, 250);
        }

        if (GUI.Button(new Rect(scrW, scrH * 3.5f, scrW * 3, scrH), "Intermediate Tower \n500 Beans"))
        {
            CreateTower(1, 500);
        }

        if (GUI.Button(new Rect(scrW, scrH * 5, scrW * 3, scrH), "Advanced Tower \n1000 Beans"))
        {
            CreateTower(2, 1000);
        }
        // If countdown is more than 3 show options to skip countdown
        if (WaveSpawner.countdown > 3)
        {
            if (GUI.Button(new Rect(scrW, scrH * 6.5f, scrW * 3, scrH), "Start next wave NOW"))
            {
                WaveSpawner.countdown = 1;
            }
        }

        GUI.EndGroup();
    }

    // Create tower
    void CreateTower(int tower, int cost)
    {
        if (GameManager.Money >= cost)
        {
            GameManager.Money -= cost;
            placingTower = true;
            activeTower = Instantiate(towers[tower], transform.position, transform.rotation);
        }

    }

    // Lock tower to ""
    void TowerToCursor()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Placeable")
        {
            activeTower.transform.position = hit.point;
            activeTower.transform.Translate(Vector3.up * 1, Space.World);
        }

        if (Input.GetMouseButtonDown(0))
        {
            placingTower = false;
            Tower towerToPlace = activeTower.gameObject.GetComponent<Tower>();
            towerToPlace.placed = true;

            //activeTower.GetComponentInChildren<Renderer>().enabled = false;  
            ShowRadii(false);
        }
    }

    // Show the radius for the tower if you are clicking on it or hide the radius if you're not clicking on a tower
    void SelectTower()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Tower")
        {
            // When clicking on a tower, get the child and enable the renderer (shows radius)
            GameObject towerSelected = hit.transform.gameObject;
            bool radiusToggle = towerSelected.transform.GetChild(0).GetComponent<Renderer>().enabled;
            towerSelected.transform.GetChild(0).GetComponent<Renderer>().enabled = !radiusToggle;

            //ShowRadii(true);
        }
        else
        {
            ShowRadii(false);
        }
    }

    // Show or hide the tower radius if true/false
    void ShowRadii(bool bigIfTrue)
    {
        GameObject[] disableRadius = GameObject.FindGameObjectsWithTag("Radius");
        foreach (var tower in disableRadius)
        {
            tower.GetComponent<Renderer>().enabled = bigIfTrue;
        }
    }
}
