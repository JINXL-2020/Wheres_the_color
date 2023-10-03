using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProbe : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Door" || collision.gameObject.tag == "Mirror" || collision.gameObject.tag == "Broken")
        {
            if (gameObject.name == "leftProbe")
            {
                enemy.GetComponent<Enemy>().leftCol = true;
              //  Debug.Log("左");
            }

            if (gameObject.name == "rightProbe")
            {
                enemy.GetComponent<Enemy>().rightCol = true;
                ///Debug.Log("右");
            }

            if (gameObject.name == "upProbe")
            {
                enemy.GetComponent<Enemy>().upCol = true;
               // Debug.Log("上");
            }

            if (gameObject.name == "downProbe")
            {
                enemy.GetComponent<Enemy>().downCol = true;
               // Debug.Log("下");
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Door" || collision.gameObject.tag == "Mirror" || collision.gameObject.tag == "Broken")
        {
            if (gameObject.name == "leftProbe")
            {
                enemy.GetComponent<Enemy>().leftCol = true;
               // Debug.Log("左");
            }

            if (gameObject.name == "rightProbe")
            {
                enemy.GetComponent<Enemy>().rightCol = true;
               // Debug.Log("右");
            }

            if (gameObject.name == "upProbe")
            {
                enemy.GetComponent<Enemy>().upCol = true;
               // Debug.Log("上");
            }

            if (gameObject.name == "downProbe")
            {
                enemy.GetComponent<Enemy>().downCol = true;
               // Debug.Log("下");
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Door" || collision.gameObject.tag == "Mirror" || collision.gameObject.tag == "Broken")
        {
            if (gameObject.name == "leftProbe")
                enemy.GetComponent<Enemy>().leftCol = false;
            if (gameObject.name == "rightProbe")
                enemy.GetComponent<Enemy>().rightCol = false;
            if (gameObject.name == "upProbe")
                enemy.GetComponent<Enemy>().upCol = false;
            if (gameObject.name == "downProbe")
                enemy.GetComponent<Enemy>().downCol = false;
        }
    }
}
