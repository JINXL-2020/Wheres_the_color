using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Background : MonoBehaviour
{
    Tilemap tileMap;


    //public Material white;
    //public Material black;

    [NonSerialized]
    public static Color backgroundColorCurrent;
    Color defaultColor;
    //Material defaultMaterial;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = this.GetComponent<Tilemap>();
        defaultColor = tileMap.color;
        //defaultMaterial = tileMap.material;
    }

    // Update is called once per frame
    void Update()
    {
        updateBackgroundColor();
    }

    public void updateBackgroundColor()
    {
        if (Player.MoodValueCurrent <= Player.singleValue)
            tileMap.color = new Color(0, 0, 0);
        //tileMap.material= black;
        else if (Player.MoodValueCurrent > 2 * Player.singleValue)
            tileMap.color = new Color(1, 1, 1);
        else
            tileMap.color = new Color(126 / 255f, 127 / 255f, 126 / 255f, 255 / 255f);
    }
}
