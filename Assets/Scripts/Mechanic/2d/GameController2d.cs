using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController2d : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int soLuongPlatform, cNVLienTiep, lienTiepToiDa = 2;
    public GameObject cam, respawnPoint, diaHinh, readyText, readyPanel, flash, tutorialCanvas, timeText, buttonReady;
    public GameObject[] platform, player, readyObj;
    public GameObject fadeOutBlack, fadeInBlack, fadeOutRed, redScreen;
    public bool enableSpawn = true, enableIncreseSpeed = true, enableMoveCam = false, enableDemThoiGian = false, counting = false, ready = false;
    public float camSpeedStart = 0.5f, camSpeedIncrease = 0.5f, playerSpeedIncrease = 50, delayTime = 10, delaySpawn = 2.5f;
    public int survialTime, ngay, star = 0;
    public string result = "lose";
    public Text starNum;
    public float minRange = -6.5f, maxRange = 6.5f;

    private void Start()
    {
        soLuongPlatform = platform.Length;
        StartCoroutine(ReadyStart());
        timeText.GetComponent<Text>().text = survialTime.ToString();
        ngay = PlayerPrefs.GetInt("ngay");
    }

    private void Update()
    {
        //Kiem tra cam move de bat dau game
        if (enableMoveCam)
        {
            cam.transform.Translate(new Vector3(0, -1 * Time.deltaTime * camSpeedStart));
            if (enableIncreseSpeed)
            {
                StartCoroutine(TangTocDo());
            }

            if (enableSpawn && photonView.IsMine)
            {
                StartCoroutine(SpawnPlatform());
            }

            if (enableDemThoiGian && !counting && survialTime > 0)
                StartCoroutine(DemThoiGian());
        }

        if (ready)
        {
            //Kiem tra so luong readyObject bang voi so luong nguoi choi
            readyObj = GameObject.FindGameObjectsWithTag("ReadyObject");
            player = GameObject.FindGameObjectsWithTag("Player");
            if (readyObj.Length == player.Length)
            {
                ready = false;
                StartCoroutine(ReadyToGame());
            }
        }

    }

    //Tang toc do theo thoi gian
    IEnumerator TangTocDo()
    {
        enableIncreseSpeed = false;
        camSpeedStart += camSpeedIncrease;
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in player)
            pl.GetComponent<PlayerController2d>().speed += playerSpeedIncrease;
        delaySpawn -= 0.3f;
        if (delaySpawn < 0.5f)
            delaySpawn = 0.5f;
        gameObject.GetComponent<AudioSource>().pitch += 0.05f;
        if (gameObject.GetComponent<AudioSource>().pitch > 2)
            gameObject.GetComponent<AudioSource>().pitch = 2;
        yield return new WaitForSeconds(delayTime);
        enableIncreseSpeed = true;
    }

    //Tao platform tu ben duoi
    IEnumerator SpawnPlatform()
    {
        enableSpawn = false;
        int r = Random.Range(0, soLuongPlatform);
        //Kiem tra chuong ngai vat lien tiep neu vuot qua so lan cho phep thi random lai cho den khi khong phai chuong ngai vat va reset lai so lan lien tiep
        if (platform[r].CompareTag("ChuongNgaiVat"))
        {
            if (cNVLienTiep < lienTiepToiDa)
                cNVLienTiep++;
            else
            {
                cNVLienTiep = 0;
                do
                {
                    r = Random.Range(0, soLuongPlatform);
                } while (platform[r].tag == "ChuongNgaiVat");
            }
        }
        else
            cNVLienTiep = 0;//Neu khong phai chuong ngai vat thi reset lai so lan lien tiep
        Vector2 pos = new Vector2(Random.Range(minRange, maxRange), respawnPoint.transform.position.y);
        GameObject plat = PhotonNetwork.Instantiate(Path.Combine("Prefabs", platform[r].name), pos, Quaternion.identity);
        yield return new WaitForSeconds(delaySpawn);
        enableSpawn = true;
    }

    IEnumerator ReadyStart()
    {
        fadeInBlack.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadeInBlack.SetActive(false);
        readyPanel.SetActive(true);
       
    }

    public void ButtonReady()
    {
        //Khi bam nut ready se tao ra mot ReadyObject
        if (!ready)
        {
            ready = true;
            buttonReady.SetActive(false);
            GameObject readyO = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "ReadyObject"), gameObject.transform.position, Quaternion.identity) as GameObject;
        }           
    }

    //Bat dau game sau khi bam ready
    private IEnumerator ReadyToGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        readyText.GetComponent<Text>().text = "Go!";
        yield return new WaitForSeconds(0.5f);
        flash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
        readyPanel.SetActive(false);
        tutorialCanvas.SetActive(false);
        gameObject.GetComponent<AudioSource>().Play();
        enableDemThoiGian = true;
        enableMoveCam = true;
    }

    IEnumerator DemThoiGian()
    {
        counting = true;
        survialTime--;
        timeText.GetComponent<Text>().text = survialTime.ToString();
        yield return new WaitForSeconds(1f);
        counting = false;
    }

    //Tro choi ket thuc va quay tro lai che do 2d
    public IEnumerator To3D()
    {
        fadeOutBlack.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(3);
        PhotonNetwork.LeaveRoom();
    }
}
