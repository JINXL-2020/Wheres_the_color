using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [NonSerialized]
    public NavMeshAgent2D navMeshAgent;
    GameObject player;

    [Header("生命值")]
    [Range(1, 5)]
    public float life = 3;

    [Header("移动速度")]
    [Range(1, 5)]
    public int speed = 3;

    [Header("是否存在背面双倍伤害")]
    public bool isBackDouble = false;

    [Header("是否追踪")]
    public bool isChase = true;

    [Header("自动巡逻范围row*col")]
    [Range(0, 6)]
    public int row = 3;
    [Range(0, 6)]
    public int col = 3;

    [Header("敌人是否探测角色范围(默认自动跟随)")]
    public bool isProbe = false;
    [Header("敌人探测角色范围")]
    public int ProbeDistance = 10;

    Transform bulletSpawnPoint;

    [Header("是否发射子弹")]
    public bool isBullet = true;

    [Header("子弹")]
    public GameObject Bullet;
    Vector3 vec;
    [Header("发射间隔")]
    public float fireInterval = 10f;
    float fireCounter = 0f;
    bool canFire = true;

    GameObject prop;
    GameObject door;
    CameraShake camerashake;
    ChangeColor changeColor;

    const string path = "Prefabs/Objects/ColorBorder";

    float vspeed, hspeed;
    int vh = 0;

    bool left, right, up, down;

    [NonSerialized]
    public bool gridMove = true;

    [NonSerialized]
    public bool leftCol, rightCol, upCol, downCol;

    int xdelta, ydelta;

    bool fixeds = false;
    bool flag = true;

    Vector3 source;

    int temp = 0;
    // Start is called before the first frame update

    public Vector3 bulletVector = new Vector3(0, -1, 0);

    public Animator animator;
    public Animator animator2;

    void Start()
    {
        bulletSpawnPoint = this.gameObject.GetComponentsInChildren<Transform>()[1];


        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = this.GetComponent<NavMeshAgent2D>();
        prop = (GameObject)Resources.Load(path);
        camerashake = Camera.main.GetComponent<CameraShake>();
        door = GameObject.FindGameObjectWithTag("Door");
        changeColor = Camera.main.GetComponent<ChangeColor>();


        vspeed = 0;
        hspeed = speed;

        xdelta = 2;
        ydelta = 0;

        source = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = player.transform.position;
        float distance = Vector3.Distance(position, transform.position);



        //Move();

        Move();
        //if (Vector3.Distance(transform.position, source) >= 2)
        //{
        //    Debug.Log("起作业");
        //    right = false; up = false; down = false; left = false;
        //    gridMove = false;
        //    Vec.PositionInt(transform);
        //}


        if (isChase)
        {
            this.GetComponent<NavMeshAgent2D>().enabled = true;
            if (isProbe)
            {
                if (distance < (ProbeDistance / 2) * Math.Sqrt(2) * 2)
                {
                    if (canFire && isBullet)
                        StartCoroutine(Attack());
                }

            }
            else
            {
                if (canFire && isBullet)
                    StartCoroutine(Attack());
            }
        }
        else
        {
            if (canFire && isBullet && !player.GetComponent<Player>().isHidden)
                StartCoroutine(Attack());
            if (!player.GetComponent<Player>().isHidden)
            {
                this.GetComponent<NavMeshAgent2D>().enabled = false;
                Patrol();
            }

        }


        if (life <= 0 & flag)
        {
            flag = false;
            StartCoroutine(camerashake.Shake(.25f, .4f));
            StartCoroutine(changeColor.Colored());
            Invoke("Die", .3f);
        }

    }

    void Patrol()
    {
        if (col == 0 && row == 0)
            return;
        else
        {
            //this.transform.Translate(vspeed * Time.deltaTime, hspeed * Time.deltaTime, 0);
            this.transform.Translate(0, speed * Time.deltaTime, 0);
        }


    }
    private IEnumerator Attack()
    {
        canFire = false;
        Fire();
        while (fireCounter < fireInterval)
        {
            fireCounter += Time.deltaTime;
            yield return null;
        }
        canFire = true;
        fireCounter = 0f;
    }
    private void Fire()
    {
        GameObject b = Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as GameObject;
        vec = (bulletSpawnPoint.position - player.transform.position).normalized;
        b.transform.right = -bulletVector;
        float bulletSpeed = Bullet.GetComponent<Bullet>().Speed;
        b.GetComponent<Rigidbody2D>().AddForce(bulletVector * bulletSpeed * 100);
    }
    public void Injury(float InjuryValue)
    {
        life -= InjuryValue;

    }

    void Die()
    {
        //Instantiate(prop, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

    public void Chase()
    {
        Vector3 pos = player.transform.position;
        float distance = Vector3.Distance(transform.position, pos);

        if (isChase)
        {
            if (pos.x - transform.position.x < -.01f)
            {

                left = true; right = false;
            }

            else if (pos.x - transform.position.x > .01f)
            {
                left = false; right = true;
            }
            else
            {
                Debug.Log("横坐标相等");
                left = false; right = false;
            }
            if (pos.y - transform.position.y < -.01f)
            {
                Debug.Log("距离内1");
                up = false; down = true;
            }
            else if (pos.y - transform.position.y > .01f)
            {
                Debug.Log("距离内2");
                up = true; down = false;
            }
            else
            {
                Debug.Log("距离内3");
                up = false; down = false;
            }
        }

    }

    public void Move()
    {
        Vector3 pos = transform.position;

        if (gridMove && up && !upCol)
        {
            Debug.Log("应该向上");
            Debug.Log(upCol);
            //transform.position = new Vector3(pos.x, pos.y + 2, pos.z);
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x, (float)Math.Round(pos.y) + 2), speed * Time.deltaTime);
        }

        if (gridMove && left && !leftCol)
            //transform.position = new Vector3(pos.x-2, pos.y, pos.z);
            transform.position = Vector2.MoveTowards(pos, new Vector2((float)Math.Round(pos.x) - 2, pos.y), speed * Time.deltaTime);
        if (gridMove && down && !downCol)
        {
            Debug.Log("应该向下");
            //transform.position = new Vector3(pos.x, pos.y - 2, pos.z);
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x, (float)Math.Round(pos.y) - 2), speed * Time.deltaTime);
        }

        if (gridMove && right && !rightCol)
        {
            //transform.position = new Vector3(pos.x + Time.deltaTime * speed, pos.y, 0);
            //transform.position = new Vector3(pos.x+2, pos.y, pos.z);
            transform.position = Vector2.MoveTowards(pos, new Vector2((float)Math.Round(pos.x) + 2, pos.y), speed * Time.deltaTime);
        }
    }

    public void Freeze()
    {
        right = false; up = false; down = false; left = false;
        gridMove = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("canFight", true);
            Player.MoodValueCurrent = 0;
            player.GetComponent<Player>().MoodAlter();
        }
        else if(collision.gameObject.tag == "MirrorPlayer")
        {
            animator2.SetBool("canFight", true);
        }
        else if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Door")
        {
            if (!isChase)
                speed *= -1;
            //up = true;
        }
    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Door")
    //    {
    //        //up = true;
    //    }
    //}
}
