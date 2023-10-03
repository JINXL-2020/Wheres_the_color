using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    //public GameObject UI;
    Slider BGMSlider;
    public AudioSource BGM;
    GameObject player;
    public static  bool IsPause = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        BGMSlider = this.GetComponentInChildren<Slider>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

                   //if(IsPause == false)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        this.gameObject.SetActive(true);
        //        IsPause = true;
        //        Time.timeScale = 0f;
        //    }
        //}
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        this.gameObject.SetActive(false);
        //        IsPause = false;
        //        Time.timeScale = 1f;
        //    }
        //}
    }
    public void Pause()
    {
        
        this.gameObject.SetActive(true);
        IsPause = true;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        IsPause = false;
        this.gameObject.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        Pigment.pigNum = 0;
        Player.MoodValueCurrent = player.GetComponent<Player>().MoodValueInit;
        IsPause = false;
 
    }
    public void Quit()
    {
        Application.Quit();
        //SceneManager.LoadScene(0);
    }
    public void SetVolume()
    {
        BGM.volume = BGMSlider.value;
    }
}
