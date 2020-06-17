using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{

    private bool owned = false;
    private string birdName;
    public int price = 0;

    public GameObject expensive;

    public SystemController system;
    
    // Start is called before the first frame update
    void Start()
    {
        birdName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyBird()
    {
        if(!owned)
        {
            if (system.GetCoins() >= price)
            {
                system.SetCoins(system.GetCoins() - price);
                owned = true;
            }
            else
            {
                StartCoroutine(Expensive());
            }
        }
    }

    IEnumerator Expensive()
    {
        print("1");
        expensive.SetActive(true);

        yield return new WaitForSeconds(.1f);
        print("here");
        expensive.SetActive(false);
    }
}
