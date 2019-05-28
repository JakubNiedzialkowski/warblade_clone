using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour {

    public GameObject[] Levels;

    //Serializable classes implements
    //public EnemyWaves[] enemyWaves;

    [SerializeField]
    private GameObject shop;

    [SerializeField]
    private GameObject messageBox;


    [SerializeField]
    [Tooltip("Interval between shop screen visits")]
    private int shopLevelInterval = 5;

    private bool isLevelInProgress = false;
    public static bool isPlayerInShop = false;
    private int levelCounter = 0;
    private float timeSinceLevelStarted = 0f;

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
                    DisplayLevelInfo();
                    isLevelInProgress = true;
                    Instantiate(Levels[levelCounter]).GetComponent<Level>().startLevel();
                    levelCounter++;
                    timeSinceLevelStarted = 0f;
                }
            }
            else
            {
                timeSinceLevelStarted += Time.deltaTime;
                if (Levels[levelCounter - 1].GetComponent<Level>().isLevelCleared() && timeSinceLevelStarted > 10f)
                {
                    isLevelInProgress = false;
                    if (levelCounter % shopLevelInterval == 0)
                        EnterShop();
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

    private void EnterShop()
    {  
        Time.timeScale = 0;
        isPlayerInShop = true;
        shop.SetActive(true);
    }

}
