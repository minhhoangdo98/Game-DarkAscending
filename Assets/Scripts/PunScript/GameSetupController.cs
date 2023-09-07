using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    public Transform[] spawnPoints;
    private int spawnPicker;
    // Script này sẽ được thêm vào bất kỳ cảnh nhiều người chơi (chơi online)
    void Start()
    {
        spawnPicker = Random.Range(0, spawnPoints.Length);
        CreatePlayer(); //Tạo một đối tượng người chơi được nối mạng cho mỗi người chơi tải vào các cảnh nhiều người chơi
    }
    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[spawnPicker].position, Quaternion.identity);
    }
}