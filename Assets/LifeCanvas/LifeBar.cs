using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image[] lifeBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Player.MoodValueCurrent);
        for (int i = 0; i < 6; i++)
        {
            if (i+1 == Player.MoodValueCurrent)
                lifeBar[i].enabled = true;
            else
                lifeBar[i].enabled = false;
        }
    }
}
