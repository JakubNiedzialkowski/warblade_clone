using UnityEngine;
using TMPro;
public class ShopButton : MonoBehaviour
{

    public TextMeshProUGUI ItemNameTMP;
    public TextMeshProUGUI PriceTMP;

    public string ItemName;
    public int Price;


    // Start is called before the first frame update
    void Start()
    {
        ItemNameTMP.text = ItemName;
        PriceTMP.text = Price.ToString();
    }
}
