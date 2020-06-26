using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class Bird
{
    public string name;
    public int price;
    public bool owned;
    public bool visible;
    public int tier;
    public int index;
}

[Serializable]
public class BirdList
{
    public Bird[] birdList;
}

public class ShopController : MonoBehaviour
{
    public BirdList birds;
    public Transform contentHolder;
    private string pp = "ShopSave";
    private string ds = "DefaultShopSave";
    // Start is called before the first frame update
    private void Awake()
    {
        LoadPlayerValues(pp);
        for (int i = 0; i < birds.birdList.Length; i++)
        {
            GameObject birdButton = contentHolder.GetChild(i).gameObject;
            ShopButton buttonScript = birdButton.GetComponent<ShopButton>();
            buttonScript.thisBird = birds.birdList[i];
        }
    }

    void Start()
    {       
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            SetShop();
        if (Input.GetKeyDown(KeyCode.S))
            SavePlayerValues(pp);
        if (Input.GetKeyDown(KeyCode.D))
            SavePlayerValues(ds);
        if (Input.GetKeyDown(KeyCode.F))
            LoadPlayerValues(ds);
    }

    public void SetNextSkinVisible(int index)
    {
        index += 1;
        birds.birdList[index].visible = true;
        contentHolder.GetChild(index).GetComponent<ShopButton>().SetVisible();

        SavePlayerValues(pp);
    }

    public void SetNextBirdVisible(int index)
    {
        index += 1;
        birds.birdList[index].visible = true;
        contentHolder.GetChild(index).GetComponent<ShopButton>().SetVisible();

        index += 2;
        birds.birdList[index].visible = true;
        contentHolder.GetChild(index).GetComponent<ShopButton>().SetVisible();

        SavePlayerValues(pp);
    }

    public void SetBirdOwned(int index)
    {
        birds.birdList[index].owned = true;

        SavePlayerValues(pp);
    }

    void SetShop()
    {

        birds.birdList = new Bird[30];
        var count = 0;
        var cost = 0;
        for(int i = 0; i < birds.birdList.Length; i++)
        {


            Bird newBird = new Bird
            {
                name = contentHolder.GetChild(i).gameObject.name,
                price = 1,
                owned = false,
                visible = false,
                tier = count,
                index = i
            };

            if (newBird.tier != 0)
                cost += 25;
            newBird.price = cost;
            if (count == 2)
                count = 0;
            else
                count += 1;


            birds.birdList[i] = newBird;
        }

        birds.birdList[0].visible = true;
        birds.birdList[0].owned = true;

        birds.birdList[1].visible = true;
        birds.birdList[3].visible = true;

        //SetBirdPrices();


        SavePlayerValues(pp);
        print("Base Shop Created...");
    }

    void LoadPlayerValues(string saveName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BirdList));
        string text = PlayerPrefs.GetString(saveName);
        if (text.Length == 0)
        {
            SetShop();
        }
        else
        {
            using (var reader = new System.IO.StringReader(text))
            {
                birds = serializer.Deserialize(reader) as BirdList;
            }
        }
        print("Loaded player values...");
    }

    void SavePlayerValues(string saveName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BirdList));
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, birds);
            PlayerPrefs.SetString(saveName, sw.ToString());
        }
        print("Saved player values...");
    }
}
