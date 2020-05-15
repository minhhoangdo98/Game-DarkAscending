using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerRange : MonoBehaviour
{
    [SerializeField]
    private string range = "walk";
    [SerializeField]
    private GameObject crawler;
    private bool performTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerTrigger") && performTrigger)
        {
            performTrigger = false;
            switch (range)
            {
                case "walk":
                    StartCoroutine(PerformWalkToRun());
                    break;
                case "attack":
                    StartCoroutine(PerformRunToAttack());
                    break;
                case "trigger":
                    StartCoroutine(RedToNextScene());
                    break;
            }
        }

    }

    private IEnumerator PerformWalkToRun()
    {
        crawler.GetComponent<CrawlerController>().idle = false;
        crawler.GetComponent<CrawlerController>().walk = true;
        crawler.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);
        crawler.GetComponent<CrawlerController>().walk = false;
        crawler.GetComponent<CrawlerController>().run = true;
    }

    private IEnumerator PerformRunToAttack()
    {
        crawler.GetComponent<CrawlerController>().run = false;
        crawler.GetComponent<CrawlerController>().attack = true;
        yield return new WaitForSeconds(1);
        crawler.GetComponent<CrawlerController>().attack = false;
        crawler.GetComponent<CrawlerController>().idle = true;
    }

    private IEnumerator RedToNextScene()
    {
        crawler.GetComponent<CrawlerController>().gc3d.fadeOutRed.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        crawler.GetComponent<CrawlerController>().player.transform.Rotate(90, 0, 0, Space.Self);
        yield return new WaitForSeconds(0.5f);
        crawler.GetComponent<CrawlerController>().gc3d.redScreen.SetActive(true);
        crawler.GetComponent<CrawlerController>().gc3d.fadeOutRed.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        crawler.GetComponent<CrawlerController>().gc3d.fadeOutBlack.SetActive(true);
        crawler.GetComponent<CrawlerController>().gc3d.redScreen.SetActive(false);
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("ngay", 1);
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
