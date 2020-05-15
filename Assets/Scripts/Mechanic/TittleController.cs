using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleController : MonoBehaviour
{
    public GameObject blackScreen;

    public void ButtonPlay()
    {
        StartCoroutine(StartGame());
    }

    public void ButtonContinue()
    {
        int ngay = PlayerPrefs.GetInt("ngay");
        string result = PlayerPrefs.GetString("result");
        if (ngay > 0)
        {
            if (result == "lose")
            {
                ngay--;
                PlayerPrefs.SetInt("ngay", ngay);
                result = "win";
                PlayerPrefs.SetString("result", result);
            }
            StartCoroutine(ContinueGame());
        }
    }

    public void ButtonExit()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        PlayerPrefs.SetInt("ngay", 0);
        PlayerPrefs.SetInt("star", 0);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    IEnumerator ContinueGame()
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
