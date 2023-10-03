using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pigment : MonoBehaviour
{
    GameObject pigmentBox;
    Image[] pigmentImage;
    Player player;
    public static int pigNum=0;
    const string path = "Prefabs/Objects/Bullets/BulletPlayer";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pigmentBox = GameObject.FindGameObjectWithTag("PigmentBox");
        pigmentImage = pigmentBox.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            pigmentImage[pigNum].sprite = this.GetComponent<SpriteRenderer>().sprite;
            pigmentImage[pigNum++].color = new Color(1, 1, 1, 1);
            player.Bullet= (GameObject)Resources.Load(path + this.name[7]);
            
            Destroy(this.gameObject);
        }
    }
}
