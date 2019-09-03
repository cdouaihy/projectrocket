using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    static int lives = 3;
    Text lifeCount;
    // Start is called before the first frame update
    void Start()
    {
        lifeCount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeCount.text = lives.ToString();
    }

    public int getLife()
    {
        return lives;
    }
    public void AddLife()
    {
        lives = lives + 1;
    }

    public void ResetLife()
    {
        lives = 3;
    }

    public void RemoveLife()
    {
        lives = lives - 1;
    }

}
