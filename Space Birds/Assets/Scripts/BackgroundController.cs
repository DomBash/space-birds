using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float speed = 5f;
    public bool loop = false;
    public float killZ = -1.75f;
    public float zDist = 150f;

    public SystemController system;

    private Vector3 defaultPos;

    void Start()
    {
        system = FindObjectOfType<SystemController>();
        defaultPos = transform.position;
    }

    void Update()
    {
        if (system.isInGame)
        {
            transform.Translate(-Time.deltaTime * speed, 0f, 0f, Space.World);

            if (transform.position.x <= killZ)
            {
                if (loop)
                {
                    Instantiate(gameObject, new Vector3(19.5f, 0f, zDist), Quaternion.identity);
                    killZ = -19.5f;
                    loop = false;
                }
                else
                    gameObject.SetActive(false);
            }
        }

    }

    public void Reset()
    {
        transform.position = defaultPos;
        gameObject.SetActive(true);
    }
}
