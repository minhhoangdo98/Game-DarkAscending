using Photon.Pun;
using UnityEngine;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex; //Số cho chỉ mục xây dựng cho cảnh nhiều người
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnJoinedRoom() //Chức năng gọi lại khi chúng tôi tạo hoặc tham gia phòng thành công.
    {
        Debug.Log("Joined Room");
        StartGame();
    }
    private void StartGame() //Chức năng tải vào cảnh nhiều người chơi.
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex); //vì AutoSyncScene, tất cả người chơi tham gia phòng cũng sẽ được tải vào cảnh nhiều người chơi.
        }
    }
}