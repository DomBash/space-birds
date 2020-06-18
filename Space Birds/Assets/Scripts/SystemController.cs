using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemController : MonoBehaviour
{

    public GameObject meteorPrefab;
    public GameObject meteorMiddlePrefab;
    public GameObject coinPrefab;

    public Transform meteorHolder;
    public Transform coinHolder;

    public Text shopCoinText;

    private float meteorRate = 0.5f;
    private float nextMeteorTime = 0f;

    private float nextCoinTime = 0f;

    private List<GameObject> middleMeteors = new List<GameObject>();
    private List<GameObject> coinsL = new List<GameObject>();

    public bool isInGame = false;

    private int score = 0;
    private int coins = 0;

    public Text scoreText;
    public Text scoreOverText;

    public UIController ui;
    public PlayerMovement player;
    public AnimationController anims;

    void Start()
    {
        nextCoinTime = Time.time + 1f / Random.Range(0.2f, 0.05f);
        for (int i = 0; i < 15; i++)
        {
            var meteor = Instantiate(meteorPrefab, new Vector2( -8f + (i * 1.5f), 4.5f), Quaternion.identity);
            meteor.transform.SetParent(meteorHolder);
        }
       
        for (int i = 0; i < 15; i++)
        {
            var meteor = Instantiate(meteorPrefab, new Vector2(-8f + (i * 1.5f), -4.5f), Quaternion.identity);
            meteor.transform.SetParent(meteorHolder);
        }      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            PrintPlayerPrefs();

        if (Input.GetKeyDown(KeyCode.R))
            ResetPlayerPrefs();

            if (Time.time >= nextMeteorTime && isInGame)
        {
            SpawnMeteor();
            nextMeteorTime = Time.time + 1f / meteorRate;
        }

        if (Time.time >= nextCoinTime && isInGame)
        {
            SpawnCoin();
            nextCoinTime = Time.time + 1f / Random.Range(0.2f, 0.05f);
        }
    }

    void SpawnMeteor()
    {
        var meteor = Instantiate(meteorMiddlePrefab, new Vector2(12f, Random.Range(-3.5f, 3.5f)), Quaternion.identity);
        middleMeteors.Add(meteor);
        meteor.transform.SetParent(meteorHolder);
    }
    void SpawnCoin()
    {
        var coin = Instantiate(coinPrefab, new Vector2(12f, Random.Range(-3.5f, 3.5f)), Quaternion.identity);
        coinsL.Add(coin);
        coin.transform.SetParent(coinHolder);
    }


    public void GameOver()
    {
        scoreOverText.text = "Score: " + score;
        ui.GameOverMenu();
        isInGame = false;        
    }

    public void Play()
    {
        player.Play();
        score = 0;
        scoreText.text = score.ToString();

        foreach (GameObject meteor in middleMeteors)
        {
            Destroy(meteor);
        }
        middleMeteors = new List<GameObject>();

        foreach (GameObject coin in coinsL)
        {
            Destroy(coin);
        }
        coinsL = new List<GameObject>();

    }

    public void TapToPlay()
    {
        isInGame = true;
    }

    public void AddScore()
    {
        score += 1;

        if (score > GetHS())
            SetHS(score);

        scoreText.text = score.ToString();
    }

    public void AddCoin()
    {
        coins += 1;
        Add1Coin();
    }

    void SetHS(int num)
    {
        PlayerPrefs.SetInt("HS", num);
    }

    void Add1Coin()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 10);
        print("soudl add 1 coin");
    }

    public void SetCoins(int num)
    {
        shopCoinText.text = "Coins: " + num;
        PlayerPrefs.SetInt("Coins", num);
    }

    public int GetHS()
    {
        return PlayerPrefs.GetInt("HS", 0);
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public void ResetPlayerPrefs()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("HS", 0);
        PlayerPrefs.SetString("CurrBird", "BirdBase");
        print("PlayerPrefs Reset...");
    }

    public void PrintPlayerPrefs()
    {
        print("Coins: " + PlayerPrefs.GetInt("Coins") + " HS: " + PlayerPrefs.GetInt("HS") + " Bird: " + PlayerPrefs.GetString("CurrBird"));
        Add1Coin();

    }

    public void SetCurrentSkin(string skinName)
    {
        PlayerPrefs.SetString("CurrBird", skinName);
        anims.SetSkin(skinName);
    }
}
