using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static AudioClip player_laser;
    public static AudioClip player_destruction;
    public static AudioClip player_shield;
    public static AudioClip enemy_laser;
    public static AudioClip enemy_destruction;
    public static AudioClip item_bought;
    public static AudioClip money_collected;
    public static AudioClip level_completed;
    public static AudioClip power_up;

    private static AudioSource audioSource;
    private void Start()
    {
        player_laser = Resources.Load<AudioClip>("player_laser");
        player_destruction = Resources.Load<AudioClip>("player_destruction");
        player_shield = Resources.Load<AudioClip>("player_shield");
        enemy_destruction = Resources.Load<AudioClip>("enemy_destruction");
        enemy_laser = Resources.Load<AudioClip>("enemy_laser");
        item_bought = Resources.Load<AudioClip>("item_bought");
        money_collected = Resources.Load<AudioClip>("money_collect");
        level_completed = Resources.Load<AudioClip>("level_completed");
        power_up = Resources.Load<AudioClip>("power_up");
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "player_laser":
                audioSource.PlayOneShot(player_laser, 0.1f);
                break;
            case "player_destruction":
                audioSource.PlayOneShot(player_destruction);
                break;
            case "player_shield":
                audioSource.PlayOneShot(player_shield);
                break;
            case "enemy_laser":
                audioSource.PlayOneShot(enemy_laser, 0.05f);
                break;
            case "enemy_destruction":
                audioSource.PlayOneShot(enemy_destruction, 0.3f);
                break;
            case "item_bought":
                audioSource.PlayOneShot(item_bought);
                break;
            case "money_collected":
                audioSource.PlayOneShot(money_collected, 0.15f);
                break;
            case "level_completed":
                audioSource.PlayOneShot(level_completed, 1.15f);
                break;
            case "power_up":
                audioSource.PlayOneShot(power_up);
                break;
        }
    }
}
