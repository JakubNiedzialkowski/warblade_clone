using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    public static int Score = 0;
    TextMeshProUGUI scoreTMP;
    // Start is called before the first frame update
    void Start()
    {
        scoreTMP = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreTMP != null)
            scoreTMP.text = "Score: " + Score;
    }

    public static void ResetScore()
    {
        Score = 0;
    }
}
