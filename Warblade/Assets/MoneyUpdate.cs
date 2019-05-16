using UnityEngine;
using TMPro;

public class MoneyUpdate : MonoBehaviour
{
    TextMeshProUGUI money;

    // Start is called before the first frame update
    void Start()
    {
        money = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        money.text = "Money: " + Player.money;
    }
}
