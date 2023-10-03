using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBullet : MonoBehaviour
{
    public GameObject truthBullet;
    GameObject player;
    Color color;
    float mid;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mid = (int)(player.transform.position.x + transform.position.x) / 2;
        color = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (truthBullet)
            transform.position = new Vector3(2 * mid - truthBullet.transform.position.x, truthBullet.transform.position.y, truthBullet.transform.position.z);
        else
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Mirror")
        {
            //isCollison = true;
            Debug.Log("怎么回事");
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
        }
        //if ((collision.tag == "Environment" || collision.tag == "Door"))
        //{
        //    Destroy(this.gameObject);
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mirror")
        {
            //isCollison = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mirror")
        {
            //isCollison = false;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
        }

    }
}
