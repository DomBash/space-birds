using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{

    public Bird thisBird;

    public Sprite locked;
    public Sprite birdSprite;
    public Transform selectedSprite;

    public Text priceText;

    private Button buttonComponent;

    public GameObject expensive;
    public GameObject lockedText;

    public SystemController system;

    public ShopController shop;

    private bool started = false;

    void Start()
    {
        birdSprite = GetComponent<Image>().sprite;
        priceText = GetComponentInChildren<Text>();
        buttonComponent = GetComponent<Button>();


        buttonComponent.onClick.AddListener(BuyBird);

        priceText.text = thisBird.price.ToString();

        started = true;

        LoadButton();
    }

    void OnEnable()
    {
        if(started)
            LoadButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadButton()
    {
        
        if (!thisBird.owned)
        {
            if (!thisBird.visible)
            {
                GetComponent<Image>().sprite = locked;
                priceText.gameObject.SetActive(false);
            }
            else
            {
                //Faded color
                ButtonFade(0.5f);
            }

        }
        else
            priceText.gameObject.SetActive(false);
    }

    public void BuyBird()
    {
        if (!thisBird.owned)
        {
            if (thisBird.visible)
            {
                if (system.GetCoins() >= thisBird.price)
                {
                    system.SetCoins(system.GetCoins() - thisBird.price);

                    SetCurrBird();
                    GetComponent<Image>().sprite = birdSprite;
                    priceText.gameObject.SetActive(false);
                    thisBird.owned = true;
                    

                    //Full color
                    ButtonFade(1f);
                    shop.SetBirdOwned(thisBird.index);

                    if (thisBird.tier == 1)
                    {
                        shop.SetNextSkinVisible(thisBird.index);
                    }

                    if (thisBird.tier == 0 )//&& thisBird.name != "HummingBase")
                    {
                        shop.SetNextBirdVisible(thisBird.index);
                    }

                }
                else
                {
                    StartCoroutine(Expensive());
                }
            }
            else
            {
                StartCoroutine(Locked());
            }
        }
        else
            SetCurrBird();
    }

    public void SetVisible()
    {
        thisBird.visible = true;
        GetComponent<Image>().sprite = birdSprite;
        ButtonFade(0.5f);
        priceText.gameObject.SetActive(true);
    }

    void ButtonFade(float num)
    {
        Color currColor = GetComponent<Image>().color;
        currColor = Color.HSVToRGB(0f, 0f, num);
        currColor.a = num;
        GetComponent<Image>().color = currColor;
    }

    void SetCurrBird()
    {
        system.SetCurrentSkin(thisBird.name);
        selectedSprite.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
        //PlayerPrefs.SetString("CurrBird", thisBird.name);
    }

    IEnumerator Expensive()
    {
        expensive.SetActive(true);
        yield return new WaitForSeconds(1f);
        expensive.SetActive(false);
    }

    IEnumerator Locked()
    {
        lockedText.SetActive(true);
        yield return new WaitForSeconds(1f);
        lockedText.SetActive(false);
    }
}
