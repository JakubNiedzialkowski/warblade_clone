using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour {

    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    
    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path

    private CoinSpawner coinSpawner;

    public string Type = "Enemy";
    #endregion

    private void Start()
    {
        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
        coinSpawner = GetComponent<CoinSpawner>();
    }

    //coroutine making a shot
    void ActivateShooting() 
    {
            switch (Type)
            {
                case "Enemy":
                    if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
                    {
                        Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
                        SoundController.PlaySound("enemy_laser");
                    }
                break;
                case "Boss":
                    if (Random.value < (float)shotChance / 100)
                    {
                        Vector3 angle = new Vector3(0, 0, -60);
                        for (int i = 0; i < 7; i++)
                        {
                            Instantiate(Projectile, gameObject.transform.position, Quaternion.Euler(angle));
                            angle.z += 20;
                        }
                    SoundController.PlaySound("enemy_laser");
                    }
                Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
                break;
                default:
                    if (Random.value < (float)shotChance / 100)
                    {
                        Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
                        SoundController.PlaySound("enemy_laser");
                    }
                break;
            }
  
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage) 
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            {
                switch (Type)
                {
                    case "Enemy":
                        Destruction();
                        ScoreUpdate.Score += 100;
                        break;
                    case "Boss":
                        BossDestruction();
                        ScoreUpdate.Score += 5000;
                        break;
                    default:
                        Destruction();
                        ScoreUpdate.Score += 100;
                        break;
                }
            }
        else
            Instantiate(hitEffect,transform.position,Quaternion.identity,transform);
    }    

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        coinSpawner.RollToSpawnCoin(transform.position);
        SoundController.PlaySound("enemy_destruction");
        Destroy(gameObject);
    }

    void BossDestruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        coinSpawner.BossRollToSpawnCoins(transform.position);
        Destroy(gameObject);
    }
}
