using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorProp : MonoBehaviour
{
    // Start is called before the first frame update
    Color color;
    void Start()
    {
        color = this.gameObject.GetComponent<SpriteRenderer>().color;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mirror")
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

    }
}
