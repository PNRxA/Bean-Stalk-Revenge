using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HUD : MonoBehaviour
{
    public bool placingTower = false;
    public GameObject[] towers;
    public float activeTowerSpeed = 3.0f;
    public bool inUpgradeMenu = false;
    public GameObject towerSelected;
    public Vector2 mousePos;

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
        mousePos = Input.mousePosition;

    }

    void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 10;
        TowerBuyMenu();
        if (inUpgradeMenu)
        {
            ShowUpgradeMenu();
        }
    }

    void TowerBuyMenu()
    {
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

        GUI.Box(new Rect(scrW, scrH, scrW * 4.18f, scrH), "Lives: " + GameManager.health);

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

    void ShowUpgradeMenu()
    {
        // Get the tower component of the tower to upgrade
        Tower towerToUpgrade = towerSelected.GetComponent<Tower>();
        // Get the cost of the upgrade
        int towerUpgradeCost = towerToUpgrade.level * 100;
        string towerUpgradeCostString = towerUpgradeCost + "g";
        // If the tower is level 3 then you can't upgrade it any further
        if (towerToUpgrade.level > 3)
        {
            towerUpgradeCostString = "Fully Upgraded";
        }
        GUI.BeginGroup(new Rect(0, 0, scrW * 4, scrH * 10));
        GUI.Box(new Rect(0, 0, scrW * 4, scrH * 10), "");
        GUI.Box(new Rect(0, 0, scrW * 4, scrH * 2), "Upgrades");
        // Upgrade button displays the cost of the upgrade and upgrades tower
        if (GUI.Button(new Rect(scrW, scrH * 3, scrW * 2, scrH), "Upgrade: " + towerUpgradeCostString))
        {
            // If you have the money then upgrade and deduct the cost 
            if (GameManager.Money >= towerUpgradeCost && towerToUpgrade.level <= 3)
            {
                GameManager.Money -= towerUpgradeCost;
                towerToUpgrade.level += 1;
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
            Vector3 changedPos = activeTower.transform.position;
            changedPos.y += .5f;
            activeTower.transform.position = changedPos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            placingTower = false;
            Tower towerToPlace = activeTower.gameObject.GetComponent<Tower>();
            towerToPlace.placed = true;
            ShowRadii(false);
        }
    }

    // Show the radius for the tower if you are clicking on it or hide the radius if you're not clicking on a tower
    void SelectTower()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Tower")
        {
            // If the tower is selected show the upgrade menu
            inUpgradeMenu = true;
            // Hide existing radius if there are any being shown
            ShowRadii(false);
            // When clicking on a tower, get the child and enable the renderer (shows radius)
            towerSelected = hit.transform.gameObject;
            // The radius needs to be the first child of the tower.
            bool radiusToggle = towerSelected.transform.GetChild(0).GetComponent<Renderer>().enabled;
            towerSelected.transform.GetChild(0).GetComponent<Renderer>().enabled = !radiusToggle;
        }
        else if (mousePos.x >= 0 && mousePos.x <= Screen.width / 16 * 4)
        {
            // Don't hide the upgrade menu when you're clicking on it (example: clicking upgrade)
        }
        else
        {
            // When not selecting tower then hide the upgrade menu and the radius
            ShowRadii(false);
            inUpgradeMenu = false;
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
