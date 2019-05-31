using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion


public class Level : MonoBehaviour
{

    //Serializable classes implements
    public EnemyWaves[] enemyWaves;
    private float lastWavesSpawnTime;

    public void startLevel()
    {
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        }
    }

    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
            Instantiate(Wave);
    }

    public bool isLevelCleared()
    {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
                return false;

            return true;
    }

    public float GetLastWaveSpawnTime()
    {
       return enemyWaves[enemyWaves.Length - 1].timeToStart;
    }
}
