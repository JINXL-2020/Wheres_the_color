using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCopy : MonoBehaviour
{
    public GameObject mirrorPlayer;
    GameObject player;

    int mid;
    GameObject copy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mid = (int)(player.transform.position.x + mirrorPlayer.transform.position.x) / 2;
        copy = Instantiate(this.gameObject, new Vector3((int)(2 * mid - transform.position.x), transform.position.y, transform.position.z), this.transform.rotation) as GameObject;
        copy.transform.localScale = new Vector3(-copy.transform.localScale.x, copy.transform.localScale.y, copy.transform.localScale.z);
        copy.transform.localRotation = new Quaternion(copy.transform.localRotation.x, copy.transform.localRotation.y, copy.transform.localRotation.z, -copy.transform.localRotation.w);
        copy.GetComponent<MirrorCopy>().enabled = false;
        copy.AddComponent<MirrorProp>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Destroy(copy);
        }
    }
}
