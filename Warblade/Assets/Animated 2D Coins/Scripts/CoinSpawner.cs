using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject BronzeCoin;
    public GameObject GreenCoin;
    public GameObject RedCoin;

    public int BronzeCoin_MinimumRollToSpawn = 15;
    public int GreenCoin_MinimumRollToSpawn = 8;
    public int RedCoin_MinimumRollToSpawn = 3;

    [SerializeField]
    private int numberOfBossCoinRolls = 25;

    public void RollToSpawnCoin(Vector3 spawnPosition)
    {
        int roll = UnityEngine.Random.Range(0, 100);
        if (roll <= BronzeCoin_MinimumRollToSpawn)
            spawnCoin(roll, spawnPosition);
    }

    private void spawnCoin(int roll, Vector3 spawnPosition)
    {
        if (roll <= RedCoin_MinimumRollToSpawn)
            Instantiate(RedCoin, spawnPosition, Quaternion.identity);
        else if(roll<=GreenCoin_MinimumRollToSpawn)
            Instantiate(GreenCoin, spawnPosition, Quaternion.identity);
        else
            Instantiate(BronzeCoin, spawnPosition, Quaternion.identity);
    }

    public void BossRollToSpawnCoins(Vector3 spawnPosition)
    {
        int roll;
        for (int i = 0; i < numberOfBossCoinRolls; i++)
        {
            Debug.Log("Rolling");
            roll = UnityEngine.Random.Range(0, 100);
            if (roll <= BronzeCoin_MinimumRollToSpawn)
                bossSpawnCoin(roll, spawnPosition);
        }
    }

    private void bossSpawnCoin(int roll, Vector3 spawnPosition)
    {
        spawnPosition.x += Random.Range(-2f,2f);
        spawnPosition.y += Random.Range(-2f,2f);
        if (roll <= RedCoin_MinimumRollToSpawn)
            Instantiate(RedCoin, spawnPosition, Quaternion.identity);
        else if (roll <= GreenCoin_MinimumRollToSpawn)
            Instantiate(GreenCoin, spawnPosition, Quaternion.identity);
        else
            Instantiate(BronzeCoin, spawnPosition, Quaternion.identity);
    }

}
