using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController3D : MonoBehaviour
{
    public int ngay, star;
    public string languages = "en", result = "win";
    public GameObject fadeOutBlack, fadeInBlack, fadeOutRed, redScreen;

    private void Start()
    {
        StartCoroutine(FadeInOutScreen(fadeInBlack));
        ngay = PlayerPrefs.GetInt("ngay");
        star = PlayerPrefs.GetInt("star");
        if (ngay >= 2)
        {
            result = PlayerPrefs.GetString("result");
        }
    }

    public IEnumerator FadeInOutScreen(GameObject screen)
    {
        screen.SetActive(true);
        yield return new WaitForSeconds(1);
        screen.SetActive(false);
    }

    public IEnumerator ToGameover()
    {
        yield return new WaitForSeconds(1.5f);
        fadeOutRed.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeOutBlack.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(5);
    }

}
