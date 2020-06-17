using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Bird
{
    public string name;
    public int price;
    public bool owned;
}

[Serializable]
public class BirdList
{
    public Bird[] birdList;
}

public class ShopController : MonoBehaviour
{
    public BirdList birds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetShop()
    {
        birds.birdList = new Bird[30];

        Bird birdBase = new Bird
        {
            name = "BirdBase",
            price = 0,
            owned = true
        };


        print("reset Shop");
    }

    private void LoadPlayerValues()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BirdList));
        string text = PlayerPrefs.GetString("ShopSave");
        if (text.Length == 0)
        {
            birds = new BirdList();
        }
        else
        {
            using (var reader = new System.IO.StringReader(text))
            {
                birds = serializer.Deserialize(reader) as BirdList;
            }
        }

    }

    private void SavePlayerValues()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BirdList));
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, birds);
            PlayerPrefs.SetString("ShopSave", sw.ToString());
        }

    }
}
