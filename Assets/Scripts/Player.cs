using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   
    [Header("心情值上限")]
    [Range(3,9)]
    public int MoodValueMAX = 6;

    [Header("初始心情值")]
    [Range(1, 9)]
    public int MoodValueInit = 6;

    [NonSerialized]
    public static int MoodValueCurrent;

    [NonSerialized]
    public static int singleValue;

    Rigidbody2D rigidbody;
    Transform transform;

    [Header("移动速度")]
    public int MoveSpeed = 2;

    NavMeshAgent2D navMeshAgent;

    [NonSerialized]
    public Transform target;

    [Header("是否开启白色隐藏")]
    public bool isHidden = false;

    [Header("是否开枪")]
    public bool isFire = false;

    [Header("子弹位置")]
    public Transform bulletSpawnPoint;

    [Header("子弹")]
    public GameObject Bullet;

    [NonSerialized]
    public GameObject enemy;
    Vector3 vec;

    [Header("发射子弹间隔")]
    [Range(0.1f,2f)]
    public float fireInterval = 1f;
    float fireCounter = 0f;
    bool canFire = false;

    Vector3 w;
    public Vector3 bulletVector=new Vector3(0,1,0);

    [NonSerialized]
    public  bool InRange=false;

    Image[] MoodValueIcons;

    [Header("当前关卡通过所需颜料数")]
    [Range(1,9)]
    public int pigmentsNum=4;


    [Header("是否开启镜像")]
    public bool isMirror = false;

    [Header("镜像角色")]
    public GameObject PlayerMirror;

    [Header("镜像子弹")]
    public GameObject MirrorBullet;

    [Header("UI")]
    public GameObject menu;
    public GameObject win;

    GameObject[] enemys;

    Vector3 source;
    bool left, right, up, down;
    bool gridMove = true;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        transform = this.GetComponent<Transform>();
        navMeshAgent=GetComponent<NavMeshAgent2D>();

        singleValue = MoodValueMAX / 3;
        MoodValueCurrent = MoodValueInit;
        

        GameObject gb = GameObject.FindGameObjectWithTag("MoodValue");
        MoodValueIcons = gb.GetComponentsInChildren<Image>();

        MoodAlter();

        //bulletSpawnPoint = this.gameObject.GetComponentsInChildren<Transform>()[1];
        enemys = GameObject.FindGameObjectsWithTag("Enemy");

        source = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!Menu.IsPause&&Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {

            w = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            canFire = true;
            //Fire(w);
            //navMeshAgent.destination = w;
        }
        if (isFire&&canFire && fireCounter == 0f)
            StartCoroutine(Attack(w));
        //if (canFire&&InRange)
        //StartCoroutine(Attack());
        if (MoodValueCurrent <= 0)
            Invoke("Die", 0.5f);

        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (isHidden)
        {
            if(MoodValueCurrent > 2 * singleValue)
                foreach (GameObject enemy in enemys)
                    enemy.GetComponent<Enemy>().isChase = false;
            else
                foreach (GameObject enemy in enemys)
                    enemy.GetComponent<Enemy>().isChase = true;
        }
        //else
        //{
        //    Debug.Log("怎么回事儿");
        //    foreach (GameObject enemy in enemys)
        //        enemy.GetComponent<Enemy>().isChase = true;
        //}


        MoveControl();
        Move();
        if(Vector3.Distance(transform.position, source) >= 2)
        {
            right=false; up = false; down = false; left = false;
            gridMove = true;
            Vec.PositionInt(transform);
            //PlayerMirror.GetComponent<MirrorPlayer>().isCollison = true;
        }



    }

    void MoveControl()
    {
        
        if (gridMove&&(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)))
        {
            if (isMirror)
            {
                PlayerMirror.GetComponent<MirrorPlayer>().source = PlayerMirror.transform.position;
                PlayerMirror.GetComponent<MirrorPlayer>().left = true;
                PlayerMirror.GetComponent<MirrorPlayer>().right = false;
                PlayerMirror.GetComponent<MirrorPlayer>().up = false;
                PlayerMirror.GetComponent<MirrorPlayer>().down = false;
                if (PlayerMirror.GetComponent<MirrorPlayer>().isCollison)
                    PlayerMirror.GetComponent<MirrorPlayer>().isCollison = false;
            }


            bulletVector = new Vector3(1, 0, 0);
            source = transform.position;
            gridMove = false;
            right = true;
            up = false;down = false;left = false;
        }
           
        if (gridMove && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            if (isMirror)
            {
                PlayerMirror.GetComponent<MirrorPlayer>().source = PlayerMirror.transform.position;
                PlayerMirror.GetComponent<MirrorPlayer>().right = true;
                PlayerMirror.GetComponent<MirrorPlayer>().left = false;
                PlayerMirror.GetComponent<MirrorPlayer>().up = false;
                PlayerMirror.GetComponent<MirrorPlayer>().down = false;
                if (PlayerMirror.GetComponent<MirrorPlayer>().isCollison)
                    PlayerMirror.GetComponent<MirrorPlayer>().isCollison = false;
            }


            bulletVector = new Vector3(-1, 0, 0);
            source = transform.position;
            gridMove = false;
            left = true;
            up = false; down = false; right = false;
        }
        if (gridMove && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if (isMirror)
            {
                PlayerMirror.GetComponent<MirrorPlayer>().source = PlayerMirror.transform.position;
                PlayerMirror.GetComponent<MirrorPlayer>().up = true;
                PlayerMirror.GetComponent<MirrorPlayer>().right = false;
                PlayerMirror.GetComponent<MirrorPlayer>().left = false;
                PlayerMirror.GetComponent<MirrorPlayer>().down = false;
                if (PlayerMirror.GetComponent<MirrorPlayer>().isCollison)
                    PlayerMirror.GetComponent<MirrorPlayer>().isCollison = false;
            }


            bulletVector = new Vector3(0, 1, 0);
            source = transform.position;
            gridMove = false;
            up = true;
            left = false; down = false; right = false;
        }
        if (gridMove && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            if (isMirror)
            {
                PlayerMirror.GetComponent<MirrorPlayer>().source = PlayerMirror.transform.position;
                PlayerMirror.GetComponent<MirrorPlayer>().down = true;
                PlayerMirror.GetComponent<MirrorPlayer>().up = false;
                PlayerMirror.GetComponent<MirrorPlayer>().right = false;
                PlayerMirror.GetComponent<MirrorPlayer>().left = false;
                if (PlayerMirror.GetComponent<MirrorPlayer>().isCollison)
                    PlayerMirror.GetComponent<MirrorPlayer>().isCollison = false;
            }


            bulletVector = new Vector3(0, -1, 0);
            source = transform.position;
            gridMove = false;
            down =true;
            left = false; up= false; right = false;
        }

    }
    void Move()
    {
       
        Vector3 pos = transform.position;

        if (up)
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x , pos.y + 2), MoveSpeed * Time.deltaTime);
        if (left)
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x-2, pos.y), MoveSpeed * Time.deltaTime);
        if (down)
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x, pos.y - 2), MoveSpeed * Time.deltaTime);
        if (right) {
            transform.position = Vector2.MoveTowards(pos, new Vector2(pos.x+2, pos.y), MoveSpeed * Time.deltaTime);
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
    public void Injury(int InjuryValue)
    {
        MoodValueCurrent -= InjuryValue;
        MoodAlter();
    }
    public void Gain(int GainValue)
    {
        MoodValueCurrent += GainValue;
        if (MoodValueCurrent > MoodValueMAX)
            MoodValueCurrent = MoodValueMAX;
        MoodAlter();
    }
    public void MoodAlter()
    {
        //Color color = MoodValueIcons[0].color;
        //if (MoodValueCurrent < singleValue) 
        //{
        //    MoodValueIcons[0].color = new Color(color.r, color.g, color.b, (float)MoodValueCurrent / (float)singleValue);
        //    MoodValueIcons[1].color = new Color(color.r, color.g, color.b, 0);
        //    MoodValueIcons[2].color = new Color(color.r, color.g, color.b, 0);
        //}

        //else if (MoodValueCurrent < 2 * singleValue)
        //{
        //    MoodValueIcons[0].color = new Color(color.r, color.g, color.b, 1);
        //    MoodValueIcons[1].color = new Color(color.r, color.g, color.b, (float)(MoodValueCurrent-singleValue) / (float)singleValue);
        //    MoodValueIcons[2].color = new Color(color.r, color.g, color.b, 0);
        //}
        //else if (MoodValueCurrent <= 3 * singleValue)
        //{
        //    MoodValueIcons[0].color = new Color(color.r, color.g, color.b, 1);
        //    MoodValueIcons[1].color = new Color(color.r, color.g, color.b, 1);
        //    MoodValueIcons[2].color = new Color(color.r, color.g, color.b, (float)(MoodValueCurrent-2*singleValue) / (float)singleValue);
        //}
        Debug.Log("Hello?");
        for (int i = 0; i < 6; i++)
        {
            if (i == MoodValueCurrent - 1)
            {
                MoodValueIcons[i].enabled = true;
                if(MoodValueIcons.Length>6)
                    MoodValueIcons[i + 6].enabled = true;
            }
            else
            {
                MoodValueIcons[i].enabled = false;
                if (MoodValueIcons.Length > 6)
                    MoodValueIcons[i + 6].enabled = false;
            }
        }

    }
    private IEnumerator Attack(Vector3 w)
    {
        canFire = false;
        Fire(w);
        while (fireCounter < fireInterval)
        {
            fireCounter += Time.deltaTime;
            yield return null;
        }
        //canFire = true;
        fireCounter = 0f;
    }
    private void Fire(Vector3 w)
    {
        
        GameObject b = Instantiate(Bullet, this.transform.position, this.transform.rotation) as GameObject;

        Debug.Log(w);
        Debug.Log(bulletSpawnPoint);
        vec = (bulletSpawnPoint.position - w).normalized;
        b.transform.right = -bulletVector;
        //Vec.LookAt(b.transform, w);
        float bulletSpeed = Bullet.GetComponent<Bullet>().Speed;
        b.GetComponent<Rigidbody2D>().AddForce(bulletVector * bulletSpeed*100);
        Debug.Log("6789211221");
        Debug.Log(bulletVector);
        MoodValueCurrent -= Bullet.GetComponent<Bullet>().MoodBullet;
        MoodAlter();

        if (isMirror)
        {
            GameObject m = Instantiate(MirrorBullet, PlayerMirror.transform.position, this.transform.rotation) as GameObject;
            if(bulletVector.x!=0)
                m.transform.right = bulletVector;
            else
                m.transform.right = -bulletVector;
            m.GetComponent<MirrorBullet>().truthBullet = b;
        }
            
    }
    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Win()
    {
        //Menu.IsPause = true;
        //menu.SetActive(true);
        //win.SetActive(true);
        //Time.timeScale = 0;
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gridMove = true;
        right = false; up = false; down = false; left = false;
        if(isMirror)
            PlayerMirror.GetComponent<MirrorPlayer>().isCollison = true;
        Vec.PositionInt(transform);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        gridMove = true;
        right = false; up = false; down = false; left = false;
        if(isMirror)
            PlayerMirror.GetComponent<MirrorPlayer>().isCollison = true;
        Vec.PositionInt(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //PlayerMirror.GetComponent<MirrorPlayer>().isCollison = false;
    }
}
