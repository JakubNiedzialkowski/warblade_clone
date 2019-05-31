using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour {

    public GameObject[] Levels;

    [SerializeField]
    private GameObject shop;

    [SerializeField]
    private GameObject messageBox;

    [SerializeField]
    private GameObject enterShopButton;


    [SerializeField]
    [Tooltip("Interval between shop screen visits")]
    private int shopLevelInterval = 5;

    private bool isLevelInProgress = false;
    public static bool isPlayerInShop = false;
    private int levelCounter = 0;
    private float timeSinceLevelStarted = 0f;
    private float timeBeforeCanCheckForLevelOver;

    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    List<GameObject> planetsList = new List<GameObject>();

    Camera mainCamera;
    

    private void Start()
    {
        mainCamera = Camera.main;

        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
    }

    private void Update()
    {
        if (!isPlayerInShop)
        {
            if (!isLevelInProgress)
            {
                if(levelCounter<Levels.Length)
                {
                    isLevelInProgress = true;
                    Level currentLevel = Instantiate(Levels[levelCounter]).GetComponent<Level>();
                    currentLevel.startLevel();
                    timeBeforeCanCheckForLevelOver = currentLevel.GetLastWaveSpawnTime() + 1f;
                    Debug.Log(timeBeforeCanCheckForLevelOver);
                    levelCounter++;
                    if (levelCounter % shopLevelInterval == 1 && levelCounter!=1)
                        DisplayEnterShopMessage();
                    else
                        DisplayLevelInfo();
                    timeSinceLevelStarted = 0f;
                }
            }
            else
            {
                timeSinceLevelStarted += Time.deltaTime;
                if (Levels[levelCounter - 1].GetComponent<Level>().isLevelCleared() && timeSinceLevelStarted > timeBeforeCanCheckForLevelOver)
                {
                    isLevelInProgress = false;
                    if (levelCounter % shopLevelInterval == 0)
                    {
                        StartCoroutine(enterShop());
                    }
                }
            }

        }
    }


    //endless coroutine generating 'levelUp' bonuses. 
    IEnumerator PowerupBonusCreation() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeForNewPowerup);
            Instantiate(
                powerUp,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), 
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2), 
                Quaternion.identity
                );
        }
    }

    private void DisplayLevelInfo()
    {
        var TMP = messageBox.GetComponent<TextMeshProUGUI>();
        TMP.text = "Level " + levelCounter.ToString();
        messageBox.SetActive(true);
    }

    private void DisplayEnterShopMessage()
    {
        var TMP = messageBox.GetComponent<TextMeshProUGUI>();
        TMP.text = "Approaching shop";
        messageBox.SetActive(true);
    }

    IEnumerator enterShop()
    {
        yield return new WaitForSecondsRealtime(10);
        Time.timeScale = 0;
        DisplayLevelInfo();
        enterShopButton.SetActive(true);
    }
    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }
}
