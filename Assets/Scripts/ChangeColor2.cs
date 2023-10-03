using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class ChangeColor2 : MonoBehaviour
{
    GameObject border;

    public Color borderColor;
    public Color cameraColor;
    private void Start()
    {
        border = GameObject.Find("border");
    }
    public IEnumerator Colored()
    {
        Camera.main.backgroundColor = cameraColor;
        border.GetComponent<Tilemap>().SetTileFlags(new Vector3Int(1, 0, 0), TileFlags.None);
        border.GetComponent<Tilemap>().SetColor(new Vector3Int(0, 0, 0), borderColor);
        yield return null;
    }
}
