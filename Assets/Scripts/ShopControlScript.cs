using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



[System.Serializable] public class PlayerData
{
    public int coins = PlayerPrefs.GetInt("Coins");

}

public static class ShopControlScript
{
    static PlayerData playerData = new PlayerData();

    // Use this for initialization
    static void Start()
    {

    }

    // Update is called once per frame
    static void Update()
    {
        playerData.coins = PlayerPrefs.GetInt("Coins");
    }

    public static int GetCoins()
    {
        return playerData.coins;
    }

    public static void AddCoins(int amount)
    {
        playerData.coins += amount;
        SaveCoinData();
    }

    public static bool CanSpendCoins(int amount)
    {
        playerData.coins = PlayerPrefs.GetInt("Coins");
        return (playerData.coins >= amount);
    }

    public static void SpendCoins(int amount)
    {
        playerData.coins = PlayerPrefs.GetInt("Coins");
        playerData.coins -= amount;
        SaveCoinData();
    }

    public static void SaveCoinData()
    {
        PlayerPrefs.SetInt("Coins", playerData.coins);
    }

}
