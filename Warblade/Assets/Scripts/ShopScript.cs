using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public static int moneySpent = 0;
    // Update is called once per frame
    void Start()
    {
        UpdateButtons();
        moneySpent = 0;
    }

    public void AddHealth()
    {
        GameObject item = GameObject.Find("ExtraLife");
        int price = item.GetComponent<ShopButton>().Price;
        if (Player.health < 3 && Player.money >= price)
        {
            Player.health++;
            Player.instance.UpdateUiHealth();
            MoneyUpdate.SubtractMoney(price);
        }
        UpdateHealthButton();
        moneySpent += price;
        SoundController.PlaySound("item_bought");
    }

    public void AddShield()
    {
        GameObject item = GameObject.Find("ExtraShield");
        int price = item.GetComponent<ShopButton>().Price;
        if (Player.shields < 3 && Player.money >= price)
        {
            Player.shields++;
            Player.instance.UpdateUiShields();
            MoneyUpdate.SubtractMoney(price);
        }
        UpdateShieldButton();
        moneySpent += price;
        SoundController.PlaySound("item_bought");
    }

    public void GetDoubleShot()
    {
        GameObject item = GameObject.Find("DoubleShot");
        int price = item.GetComponent<ShopButton>().Price;
        if (PlayerShooting.instance.weaponPower < 2 && Player.money >= price)
        {
            PlayerShooting.instance.weaponPower = 2;
            MoneyUpdate.SubtractMoney(price);
        }
        UpdateWeaponsButtons();
        moneySpent += price;
        SoundController.PlaySound("item_bought");
    }

    public void GetTripleShot()
    {
        GameObject item = GameObject.Find("TripleShot");
        int price = item.GetComponent<ShopButton>().Price;
        if (PlayerShooting.instance.weaponPower < 3 && Player.money >= price)
        {
            PlayerShooting.instance.weaponPower = 3;
            MoneyUpdate.SubtractMoney(price);
        }
        UpdateWeaponsButtons();
        moneySpent += price;
        SoundController.PlaySound("item_bought");
    }

    public void GetQuadShot()
    {
        GameObject item = GameObject.Find("QuadShot");
        int price = item.GetComponent<ShopButton>().Price;
        if (PlayerShooting.instance.weaponPower < 4 && Player.money >= price)
        {
            PlayerShooting.instance.weaponPower = 4;
            MoneyUpdate.SubtractMoney(price);
        }
        UpdateWeaponsButtons();
        moneySpent += price;
        SoundController.PlaySound("item_bought");
    }

    public void GetPentaShot()
    {
        GameObject item = GameObject.Find("PentaShot");
        int price = item.GetComponent<ShopButton>().Price;
        if (PlayerShooting.instance.weaponPower < 5 && Player.money >= price)
        {
            PlayerShooting.instance.weaponPower = 5;
            MoneyUpdate.SubtractMoney(price);
        }
        UpdateWeaponsButtons();
        moneySpent += price;
        SoundController.PlaySound("item_bought");
    }

    private void UpdateButtons()
    {
        UpdateHealthButton();
        UpdateShieldButton();
        UpdateWeaponsButtons();
    }
    
    private void UpdateHealthButton()
    {
        if (Player.health >= 3)
            GameObject.Find("ExtraLife").GetComponent<Button>().interactable = false;
        else
            GameObject.Find("ExtraLife").GetComponent<Button>().interactable = true;
    }

    private void UpdateShieldButton()
    {
        if (Player.shields >= 2)
            GameObject.Find("ExtraShield").GetComponent<Button>().interactable = false;
        else
            GameObject.Find("ExtraShield").GetComponent<Button>().interactable = true;
    }

    private void UpdateWeaponsButtons()
    {
        Button doubleShot = GameObject.Find("DoubleShot").GetComponent<Button>();
        Button tripleShot = GameObject.Find("TripleShot").GetComponent<Button>();
        Button quadShot = GameObject.Find("QuadShot").GetComponent<Button>();
        Button pentaShot = GameObject.Find("PentaShot").GetComponent<Button>();

        switch (PlayerShooting.instance.weaponPower)
        {
            case 1:
                doubleShot.interactable = true;
                tripleShot.interactable = true;
                quadShot.interactable = true;
                pentaShot.interactable = true;
                break;
            case 2:
                doubleShot.interactable = false;
                tripleShot.interactable = true;
                quadShot.interactable = true;
                pentaShot.interactable = true;
                break;
            case 3:
                doubleShot.interactable = false;
                tripleShot.interactable = false;
                quadShot.interactable = true;
                pentaShot.interactable = true;
                break;
            case 4:
                doubleShot.interactable = false;
                tripleShot.interactable = false;
                quadShot.interactable = false;
                pentaShot.interactable = true;
                break;
            case 5:
                doubleShot.interactable = false;
                tripleShot.interactable = false;
                quadShot.interactable = false;
                pentaShot.interactable = false;
                break;
        }

    }
}
