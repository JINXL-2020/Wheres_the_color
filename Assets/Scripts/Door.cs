using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameObject Player;
    public static bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Player.GetComponent<Player>().Win();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            isOpen = false;
            Debug.Log("怎恶魔回事");
            Player.GetComponent<Player>().Win();
            //Player.GetComponent<Player>().Menu.SetActive(true);
        }
            
    }

}
