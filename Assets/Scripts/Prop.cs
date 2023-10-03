using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    [Header("道具增益值")]
    [Range(1, 3)]
    public int GainValue = 1;
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
            collision.gameObject.GetComponent<Player>().Gain(GainValue);
            this.GetComponent<AudioSource>().Play();
            Invoke("DestroyMe", 0.15f);
        }
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }

}
