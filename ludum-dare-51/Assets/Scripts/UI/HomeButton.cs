using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public void OnClick2()
    {
        Debug.Log("GO HOME");
        SceneManager.LoadScene("MainMenu");
    }
}
