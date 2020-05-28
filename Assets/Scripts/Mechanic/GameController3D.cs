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
        //Khoi tao cac gia tri
        StartCoroutine(FadeInOutScreen(fadeInBlack));
        ngay = PlayerPrefs.GetInt("ngay");//ngay de xac dinh cot truyen
        star = PlayerPrefs.GetInt("star");//so diem dat duoc o vong choi 2d
        if (ngay >= 2)
        {
            result = PlayerPrefs.GetString("result");//ket qua win hoac lose sau khi hoan thanh man choi 2d
        }
    }

    //Lam screen toi/sang dan
    public IEnumerator FadeInOutScreen(GameObject screen)
    {
        screen.SetActive(true);
        yield return new WaitForSeconds(1);
        screen.SetActive(false);
    }

    //Chuyen sand man hinh Gameover
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
