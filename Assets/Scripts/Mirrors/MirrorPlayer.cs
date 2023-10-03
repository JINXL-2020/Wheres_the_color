using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlayer : MonoBehaviour
{
    GameObject player;

    int MoveSpeed = 2;

    [NonSerialized]
    public Vector3 source;

    [NonSerialized]
    public bool left;
    [NonSerialized]
    public bool right;
    [NonSerialized]
    public bool up;
    [NonSerialized]
    public bool down;

    bool gridMove = true;

    [NonSerialized]
    public bool isCollison = false;

    Vector3 bulletVector = new Vector3(0, 1, 0);

    Color color;
    float mid;

    public static bool ishidden;

    public SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        source = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        mid = (int)(player.transform.position.x +transform.position.x) / 2;
        //color = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(2 * mid - player.transform.position.x, player.transform.position.y, player.transform.position.z);
        //Move();
        //if (Vector3.Distance(transform.position, source) >= 2)
        //{
        //    right = false; up = false; down = false; left = false;
        //    gridMove = true;
        //    Vec.PositionInt(transform);
        //}
    }
    void MoveControl()
    {

        if (gridMove && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            bulletVector = new Vector3(1, 0, 0);
            source = transform.position;
            gridMove = false;
            right = true;
            up = false; down = false; left = false;
        }

        if (gridMove && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            bulletVector = new Vector3(-1, 0, 0);
            source = transform.position;
            gridMove = false;
            left = true;
            up = false; down = false; right = false;
        }
        if (gridMove && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            bulletVector = new Vector3(0, 1, 0);
            source = transform.position;
            gridMove = false;
            up = true;
            left = false; down = false; right = false;
        }
        if (gridMove && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            bulletVector = new Vector3(0, -1, 0);
            source = transform.position;
            gridMove = false;
            down = true;
            left = false; up = false; right = false;
        }

    }
    void Move()
    {

        Vector3 pos = transform.position;

        if (!isCollison&&up)
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x, pos.y + 2), MoveSpeed * Time.deltaTime);
        if (!isCollison&&left)
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x - 2, pos.y), MoveSpeed * Time.deltaTime);
        if (!isCollison&&down)
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x, pos.y - 2), MoveSpeed * Time.deltaTime);
        if (!isCollison&&right)
        {
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x + 2, pos.y), MoveSpeed * Time.deltaTime);
        }
        //if (Input.GetAxisRaw("Horizontal") > 0)
        //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 2, transform.position.y), 2);
        //transform.position = new Vector3(pos.x + MoveSpeed * Time.deltaTime, pos.y, pos.z);
        //if (Input.GetAxisRaw("Horizontal") < 0)
        //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 2, transform.position.y), 2);
        //transform.position = new Vector3(pos.x - MoveSpeed * Time.deltaTime, pos.y, pos.z);
        //if (Input.GetAxisRaw("Vertical") > 0)
        //    transform.Translate(new Vector3(2, 0, 0));
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 2), 2);
        //transform.position = new Vector3(pos.x, pos.y + MoveSpeed * Time.deltaTime, pos.z);
        //if (Input.GetAxisRaw("Vertical") < 0)
        //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 2), 2);
        //transform.position = new Vector3(pos.x, pos.y - MoveSpeed * Time.deltaTime, pos.z);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Mirror")
        {
            ishidden = false;
            //isCollison = true;
            //this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
            sp.sortingOrder = 3;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mirror")
        {
            ishidden = false;
            //isCollison = true;
            sp.sortingOrder = 3;
            //this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mirror")
        {
            ishidden = true;
            //isCollison = false;
            sp.sortingOrder = -3;
            //this.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -2;
        }
          
    }
}
