using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    public static QuickStartLobbyController lobby;
    [SerializeField]
    private int RoomSize; //Hướng dẫn đặt số lượng người chơi trong phòng cùng một lúc.
    public Button buttonOnline;
    public bool online = false;

    private void Awake()
    {
        lobby = this;
    }

    // Bắt đầu được gọi trước khi cập nhật khung đầu tiên
    void Start()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();//Kết nối với máy chủ Photon chính
    }


    public override void OnConnectedToMaster() //Chức năng gọi lại khi kết nối đầu tiên được thiết lập thành công.
    {
        Debug.Log("OnConnectedToMaster");
        buttonOnline.interactable = true;
        online = true;
        PhotonNetwork.AutomaticallySyncScene = true; //Làm cho nó bất cứ cảnh nào mà máy khách chính đã tải là cảnh tất cả các máy khách khác sẽ tải
    }

    public void QuickStart() //Ghép nối với nút Bắt đầu nhanh
    {
        PhotonNetwork.JoinRandomRoom(); //Đầu tiên cố gắng tham gia một phòng hiện có
        Debug.Log("Quick start");
    }
    public override void OnJoinRandomFailed(short returnCode, string message) //Chức năng gọi lại nếu chúng ta không tham gia phòng
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }
    void CreateRoom() //cố gắng tạo phòng riêng của bạn
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 10000); //tạo một tên ngẫu nhiên cho căn phòng
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); //cố gắng tạo một căn phòng mới
        Debug.Log(randomRoomNumber);
    }
    public override void OnCreateRoomFailed(short returnCode, string message) //chức năng gọi lại nếu chúng ta không tạo được một căn phòng. Rất có thể thất bại vì tên phòng đã được thực hiện.
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom(); //Đang thử lại để tạo một căn phòng mới với một tên khác.
    }
    public void QuickCancel() //Ghép nối với nút hủy. Được sử dụng để ngừng tìm phòng để tham gia.
    {
        PhotonNetwork.LeaveRoom();
    }
}