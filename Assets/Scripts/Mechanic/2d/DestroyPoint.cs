using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPoint : MonoBehaviourPun
{
    [SerializeField]
    private GameController2d gc2d;
    private void Start()
    {
        gc2d = GameObject.FindGameObjectWithTag("GameController2d").GetComponent<GameController2d>();
    }
    //Khi cham vao se huy object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dat"))
        {
            Destroy(collision.gameObject);
            gc2d.star++;
            gc2d.starNum.text = gc2d.star.ToString();
        }

        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController2d>().death = true;
            collision.GetComponent<PlayerController2d>().diChuyen = false;
            collision.GetComponent<SpriteRenderer>().color = Color.red;
            Destroy(collision.gameObject, 1f);
            StartCoroutine(CheckGameover());
        }
    }

    IEnumerator CheckGameover()
    {
        yield return new WaitForSeconds(1.2f);
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        if (gc2d.enableMoveCam && player.Length == 0)
        {
            gc2d.enableMoveCam = false;
            gc2d.GetComponent<AudioSource>().Stop();
            gc2d.ngay++;
            PlayerPrefs.SetInt("ngay", gc2d.ngay);
            if (gc2d.survialTime <= 0)
                gc2d.result = "win";
            else
                gc2d.result = "lose";
            gc2d.star = gc2d.star + PlayerPrefs.GetInt("star");
            PlayerPrefs.SetInt("star", gc2d.star);
            PlayerPrefs.SetString("result", gc2d.result);
            Debug.Log("Leave scene");
            StartCoroutine(gc2d.To3D());
        }
    }

}
