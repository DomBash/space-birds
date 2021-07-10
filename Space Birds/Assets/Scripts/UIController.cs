using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject gameOverMenu;
    public GameObject mainMenu;
    public GameObject shopMenu;
    public GameObject tapPlay;
    public GameObject cashMenu;

    public SystemController system;
    public PlayerMovement player;
    public AdManager ads;

    public Text hsText;
    public Text coinsText;
    public Text coinsShopText;
    public Text hsOverText;
    public Text coinsCashShopText;

    void Start()
    {
        OpenMainMenu();
        tapPlay.SetActive(false);
    }

    void Update()
    {
        
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        hsOverText.text = "High Score: " + system.GetHS();
    }

    public void ReplayButton()
    {
        system.Play();
        tapPlay.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void PlayButton()
    {
        system.Play();
        tapPlay.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        gameOverMenu.SetActive(false);
        shopMenu.SetActive(false);
        OpenMainMenu();
    }

    public void TapPlay()
    {
        tapPlay.SetActive(false);
        player.TapToPlay();
        system.TapToPlay();
    }

    void OpenMainMenu()
    {

        coinsText.text = "Coins: " + system.GetCoins();
        hsText.text = "High Score: " + system.GetHS();
        
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void ShopButton()
    {
        coinsShopText.text = "Coins: " + system.GetCoins();
        shopMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CashButton()
    {
        coinsCashShopText.text = "Coins: " + system.GetCoins();
        cashMenu.SetActive(true);
    }

    public void CloseCashShop()
    {
        cashMenu.SetActive(false);
        coinsShopText.text = "Coins: " + system.GetCoins();
    }

    public void PlayRewardedAd()
    {
        int reward = system.newCoins;
        ads.PlayRewardedAd(reward);
    }
}
