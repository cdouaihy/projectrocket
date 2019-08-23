using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void OnClicked(GameObject button)
    {
        SceneManager.LoadScene(button.name);
    }
}
