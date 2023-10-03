using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    
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
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().isFire = true;
            this.GetComponent<AudioSource>().Play();
            Invoke("DestroyThis", 0.3f);
        }
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
 }
