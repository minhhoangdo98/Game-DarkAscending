using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverController : MonoBehaviour
{
    public void ChangeSceneToTitle()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
