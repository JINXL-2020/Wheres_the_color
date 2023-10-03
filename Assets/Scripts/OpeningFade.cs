using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningFade : MonoBehaviour
{
    public GameObject startMenu;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InstantiateStart", 6.0f);
        //Destroy(this.gameObject, 10.0f);
    }

    void InstantiateStart()
    {
        Instantiate(startMenu);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
