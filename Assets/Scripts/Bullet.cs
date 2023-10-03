using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("是否穿墙")]
    public bool isThroughWall = false;

    [Header("子弹伤害值")]
    [Range(1,3)]
    public int InjuryValue = 1;

    [Header("子弹速度")]
    [Range(3, 20)]
    public float Speed = 5;

    [Header("子弹耗费心情值")]
    [Range(0, 9)]
    public int MoodBullet = 1;
    public enum targetTag
    {
        Player,
        Enemy,
    }
    [Header("子弹目标")]
    public targetTag target;
    public GameObject explosion;
    GameObject targetObjects;

    GameObject player;
    GameObject door;

    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        targetObjects = GameObject.FindGameObjectWithTag(target.ToString());
        //Debug.Log(player);
        door = GameObject.FindGameObjectWithTag("Door");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(Player.transform);
        
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.tag == target.ToString())
        {
            //if(collison.tag=="Player")
            //    collison.gameObject.GetComponent<Player>().Injury(InjuryValue);
            if (collison.tag == "Enemy")
            {
                if ((player.GetComponent<Player>().bulletVector.x == collison.GetComponent<Enemy>().bulletVector.x &&
                    player.GetComponent<Player>().bulletVector.y == collison.GetComponent<Enemy>().bulletVector.y) &&
                    collison.gameObject.GetComponent<Enemy>().isBackDouble)
                {
                    Debug.Log(player.GetComponent<Player>().bulletVector);
                    Debug.Log(collison.GetComponent<Enemy>().bulletVector);
                    collison.gameObject.GetComponent<Enemy>().Injury(InjuryValue * 4);
                    Debug.Log("双倍伤害");
                }

                else
                {
                    Debug.Log(player.GetComponent<Player>().bulletVector);
                    Debug.Log(collison.GetComponent<Enemy>().bulletVector);
                    Debug.Log("单倍伤害");
                    collison.gameObject.GetComponent<Enemy>().Injury(InjuryValue);
                }

            }
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));
            GameObject expl=Instantiate(explosion,transform.position,randomRotation);
            Destroy(this.gameObject);
        }

        if (target.ToString() == "Enemy" && collison.tag == "Broken")
        {
            Destroy(this.gameObject);
            Destroy(collison.gameObject);
        }

        if (!isThroughWall && (collison.tag == "Environment"|| collison.tag == "Door")) 
        {
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));
            GameObject expl = Instantiate(explosion, transform.position, randomRotation);
            if (target.ToString() == "Enemy" &&!Door.isOpen&& Pigment.pigNum ==player.GetComponent<Player>().pigmentsNum)
            {
                door.SetActive(false);
                //door.GetComponent<Animation>().Play("DoorOpen1");
                Door.isOpen = true;
                   
            }
            Destroy(this.gameObject);
        }
        if (collison.tag == "Mirror")
        {
            Destroy(collison.gameObject);
            Destroy(this.gameObject);
        }
    }



}
