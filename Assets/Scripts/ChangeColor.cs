using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;



public class ChangeColor : MonoBehaviour
{
    GameObject border;
    public GameObject colorDoor;
    public GameObject colorDoor2;

    public GameObject bg;

    public Color borderColor;
    //public Color cameraColor1 = new Color(231, 103, 192);
    //public Color cameraColor2 = new Color(248, 183, 102);
    //public Color cameraColor3 = new Color(107, 238, 238);
    //public Color cameraColor4 = new Color(140, 248, 116);

    bool first = true;
    private void Start()
    {
        border = GameObject.Find("border");
    }
    public IEnumerator Colored()
    {
        bg.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            border.GetComponent<Tilemap>().SetTileFlags(new Vector3Int(0, 0, 0), TileFlags.None);
            border.GetComponent<Tilemap>().SetColor(new Vector3Int(0, 0, 0), borderColor);
            //Camera.main.backgroundColor = cameraColor1;
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            border.GetComponent<Tilemap>().SetTileFlags(new Vector3Int(0, 4, 0), TileFlags.None);
            border.GetComponent<Tilemap>().SetColor(new Vector3Int(0, 4, 0), borderColor);
            //Camera.main.backgroundColor = cameraColor2;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            border.GetComponent<Tilemap>().SetTileFlags(new Vector3Int(-4, 4, 0), TileFlags.None);
            border.GetComponent<Tilemap>().SetColor(new Vector3Int(-4, 4, 0), borderColor);
            //Camera.main.backgroundColor = cameraColor3;
            if (first)
            {
                first = false;
                //Camera.main.backgroundColor = cameraColor3;
            }
            else
            {
                //Debug.Log("Why");
                colorDoor.SetActive(true);
            }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            border.GetComponent<Tilemap>().SetTileFlags(new Vector3Int(2, 3, 0), TileFlags.None);
            border.GetComponent<Tilemap>().SetColor(new Vector3Int(2, 3, 0), borderColor);
            //Camera.main.backgroundColor = cameraColor4;
            //Debug.Log("Why");
            colorDoor2.SetActive(true);
        }
        //border.GetComponent<Tilemap>().SetColor(new Vector3Int(0, 0, 0), borderColor);
        yield return null;
    }
}
