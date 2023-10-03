using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mirror : MonoBehaviour
{
    public Tilemap truth;
    SpriteRenderer[] spriteRenderers;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = truth.color;
        spriteRenderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        for (int i=0;i<spriteRenderers.Length;i++)
            spriteRenderers[i].color = new Color(1 - color.r, 1 - color.g, 1 - color.b);
    }
}
