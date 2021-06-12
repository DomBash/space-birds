using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(-Time.deltaTime * 5f, 0f, 0f, Space.World);

        if (transform.position.x <= -10)
        {
                Destroy(gameObject);
        }
    }
}
