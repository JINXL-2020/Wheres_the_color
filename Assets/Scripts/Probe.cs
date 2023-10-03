using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Probe : MonoBehaviour
{
    public AudioSource audio;
    public Animator animator;
    public Animator animator2;
    public GameObject enemy;
    GameObject player;
    bool isInrange = false;
    bool isContinue = false;
    bool left, right, up, down;
    bool vok, hok;
    Vector3 source;

    float speed;
    Vector3 target;
    bool isTrigger;

    float truthdis;
    float miroordis;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        source = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
        speed = enemy.GetComponent<Enemy>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetComponent<Enemy>().isChase == false)
        {
            animator.SetBool("Sleep", true);
            animator2.SetBool("Sleep", true);
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
        }
        else
        {
            animator.SetBool("Sleep", false);
            animator2.SetBool("Sleep", false);
            if (isTrigger)
                this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(100, 100);
            else
                this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5, 5);
        }

        if (Math.Abs(enemy.transform.position.y - player.transform.position.y) < .001f)
        {
            vok = true;
            int temp = (int)Math.Round(enemy.transform.position.y);
            enemy.transform.position = new Vector3(enemy.transform.position.x, temp % 2 == 0 ? temp + 1 : temp, enemy.transform.position.z);
        }
        else
            vok = false;

        if (Math.Abs(enemy.transform.position.x - player.transform.position.x) < .001f)
        {
            hok = true;
            int temp = (int)Math.Round(enemy.transform.position.x);
            enemy.transform.position = new Vector3(temp % 2 == 0 ? temp + 1 : temp, enemy.transform.position.y, enemy.transform.position.z);
        }
        else
            hok = false;

        //Debug.Log(Vector3.Distance(enemy.transform.position, source));
        if (Math.Abs(Vector3.Distance(enemy.transform.position, target)) < .01f)
        {

            Vec.PositionInt(enemy.transform);
            //Debug.Log("完成");
            isContinue = false;
            left = false; right = false; up = false; down = false;
        }

        if (isContinue)
        {
            //Debug.Log(target);
            enemy.GetComponent<Enemy>().navMeshAgent.SetDestination(target);
            animator.SetFloat("Speed", 1);
            animator2.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator2.SetFloat("Speed", 0);
        }
        //怪物朝向改变
        if (left)
        {
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("LastMoveX", -1);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("LastMoveY", 0);
            animator2.SetFloat("Horizontal", -1);
            animator2.SetFloat("LastMoveX", -1);
            animator2.SetFloat("Vertical", 0);
            animator2.SetFloat("LastMoveY", 0);
        }
        else if (right)
        {
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("LastMoveX", 1);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("LastMoveY", 0);
            animator2.SetFloat("Horizontal", 1);
            animator2.SetFloat("LastMoveX", 1);
            animator2.SetFloat("Vertical", 0);
            animator2.SetFloat("LastMoveY", 0);
        }
        else if (up)
        {
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("LastMoveY", 1);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("LastMoveX", 0);
            animator2.SetFloat("Vertical", 1);
            animator2.SetFloat("LastMoveY", 1);
            animator2.SetFloat("Horizontal", 0);
            animator2.SetFloat("LastMoveX", 0);
        }
        else if (down)
        {
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("LastMoveY", -1);
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("LastMoveX", 0);
            animator2.SetFloat("Vertical", -1);
            animator2.SetFloat("LastMoveY", -1);
            animator2.SetFloat("Horizontal", 0);
            animator2.SetFloat("LastMoveX", 0);
        }

        //Debug.Log("左撞：");
        //Debug.Log(!enemy.GetComponent<Enemy>().leftCol);
    }
    public void Chase()
    {
        audio.Play();
        Debug.Log(player);
        Vector3 pos = player.transform.position;
        Vector3 trans = enemy.transform.position;
        float distance = Vector3.Distance(enemy.transform.position, pos);
        if (pos.x - enemy.transform.position.x < -.01f && !hok && !enemy.GetComponent<Enemy>().leftCol)
        {
            enemy.GetComponent<Enemy>().bulletVector = new Vector3(-1, 0, 0);
            target = new Vector3((int)Math.Round(trans.x - 2), trans.y, trans.z);
            left = true; right = false;
        }

        else if (pos.x - enemy.transform.position.x > .01f && !hok && !enemy.GetComponent<Enemy>().rightCol)
        {
            enemy.GetComponent<Enemy>().bulletVector = new Vector3(1, 0, 0);
            target = new Vector3((int)Math.Round(trans.x + 2), trans.y, trans.z);
            left = false; right = true;
        }
        else if (-.01f < pos.x - enemy.transform.position.x && pos.x - enemy.transform.position.x < .01f)
        {
            target = new Vector3(trans.x, trans.y, trans.z);
            left = false; right = false;
        }

        if (pos.y - enemy.transform.position.y < -.01f && !vok && !enemy.GetComponent<Enemy>().downCol)
        {
            enemy.GetComponent<Enemy>().bulletVector = new Vector3(0, -1, 0);
            target = new Vector3(trans.x, (int)Math.Round(trans.y - 2), trans.z);
            up = false; down = true;
        }
        else if (pos.y - enemy.transform.position.y > .01f && !vok && !enemy.GetComponent<Enemy>().upCol)
        {
            enemy.GetComponent<Enemy>().bulletVector = new Vector3(0, 1, 0);
            target = new Vector3(trans.x, (int)Math.Round(trans.y + 2), trans.z);
            up = true; down = false;
        }
        else if (-.01f < pos.y - enemy.transform.position.y && pos.y - enemy.transform.position.y < .01f)
        {
            //target = new Vector3(trans.x, trans.y, trans.z);
            Debug.Log("距离内3");
            up = false; down = false;
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "MirrorPlayer")
        {

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isMirror)
            {
                if (collision.tag == "MirrorPlayer" && MirrorPlayer.ishidden == false)
                {
                    miroordis = Vector2.Distance(enemy.transform.position, GameObject.FindGameObjectWithTag("MirrorPlayer").transform.position);
                }
                else
                {
                    truthdis = Vector2.Distance(enemy.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
                }

                if (truthdis >= miroordis && !MirrorPlayer.ishidden)
                    player = GameObject.FindGameObjectWithTag("MirrorPlayer");
                else
                    player = GameObject.FindGameObjectWithTag("Player");
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            isTrigger = true;
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(100, 100);
            source = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            Chase();

            isContinue = true;
            //enemy.GetComponent<Enemy>().Chase();
            isInrange = true;
            //enemy.GetComponent<Enemy>().gridMove = true;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInrange = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "MirrorPlayer")
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isMirror)
            {
                if (collision.tag == "MirrorPlayer" && MirrorPlayer.ishidden == false)
                {
                    miroordis = Vector2.Distance(enemy.transform.position, GameObject.FindGameObjectWithTag("MirrorPlayer").transform.position);
                }
                else
                {
                    truthdis = Vector2.Distance(enemy.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
                }

                if (truthdis >= miroordis && !MirrorPlayer.ishidden)
                    player = GameObject.FindGameObjectWithTag("MirrorPlayer");
                else
                    player = GameObject.FindGameObjectWithTag("Player");
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            float dis = Vector2.Distance(enemy.transform.position, player.transform.position);

            if (!isContinue)
            {
                source = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
                Chase();
                //Debug.Log(vok);
                // Debug.Log(hok);
                isContinue = true;
            }
            //if (dis > .001f && vok )
            //{
            //    source = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            //    Chase();
            //    isContinue = true;
            //    Debug.Log("怎么啦");
            //}
        }
    }
}
