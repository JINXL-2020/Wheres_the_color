using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorEnemy : MonoBehaviour
{
    public Transform enmey;
    public GameObject mirrorPlayer;

    public SpriteRenderer sp;

    GameObject player;

    int mid;
    GameObject copy;

    public static bool ishidden = false;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mid = (int)(player.transform.position.x + mirrorPlayer.transform.position.x) / 2;
        color = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(2 * mid - enmey.position.x, enmey.position.y, enmey.position.z);
        //if(ishidden)
        //    this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
        //else
        //    this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Mirror")
        {
            ishidden = false;
            //isCollison = true;
            sp.sortingOrder = 3;
            //this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mirror")
        {
            Debug.Log("在外面");
            //isCollison = false;
            //ishidden = true;
            sp.sortingOrder = -3;
            //this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mirror")
            sp.sortingOrder = 3;
            //this.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
    }
}
