using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image shield1;
    public Image shield2;

    public static int health = 3;
    public static int shields = 2;
    private Vector3 startPosition;
    public static int money = 0;
    public static int moneyEarnedThisLevel = 0;

    public float immortalTime = 2.0f;
    public float shieldImmortalTime = 4.0f;
    public static bool isImmortal = false;

    public static Player instance;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        UpdateUiShields();
        UpdateUiHealth();
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)   
    {
        if (!isImmortal)
        {
            if (shields > 0)
            {
                shields--;
                StartCoroutine(enableImmortality(shieldImmortalTime));
                UpdateUiShields();
                SoundController.PlaySound("player_shield");
            }
            else
            {
                StartCoroutine(enableImmortality(immortalTime));
                health -= damage;
                UpdateUiHealth();
                if (health > 0)
                    destruction();
                else
                    gameOverDestruction();
                SoundController.PlaySound("player_destruction");
                resetPlayerPosition();
            }
        }     
    }    

    //'Player's' destruction procedure
    private void gameOverDestruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        SceneManager.LoadScene("GameOverScene");
        Destroy(gameObject);
    }

    //'Player's' destruction procedure
    private void destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        GetComponent<PlayerShooting>().downgradeWeapon();
    }

    private IEnumerator enableImmortality(float immortalTime)
    {
        isImmortal = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(immortalTime);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        isImmortal = false;

    }

    private void resetPlayerPosition()
    {
        transform.position = startPosition;
    }

    public void UpdateUiShields()
    {
        switch (shields)
        {
            case 0:
                shield1.enabled = false;
                shield2.enabled = false;
                break;
            case 1:
                shield1.enabled = true;
                shield2.enabled = false;
                break;
            case 2:
                shield1.enabled = true;
                shield2.enabled = true;
                break;
        }
    }

    public void UpdateUiHealth()
    {
        switch (health)
        {
            case 0:
                heart1.enabled = false;
                heart2.enabled = false;
                heart3.enabled = false;
                break;
            case 1:
                heart1.enabled = true;
                heart2.enabled = false;
                heart3.enabled = false;
                break;
            case 2:
                heart1.enabled = true;
                heart2.enabled = true;
                heart3.enabled = false;
                break;
            case 3:
                heart1.enabled = true;
                heart2.enabled = true;
                heart3.enabled = true;
                break;
        }
    }

    public static void ResetData()
    {
        ScoreUpdate.ResetScore();
        health = 3;
        shields = 2;
        money = 0;
        moneyEarnedThisLevel = 0;
        isImmortal = false;
    }

}
















