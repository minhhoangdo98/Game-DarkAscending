using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class DoorTo2D : MonoBehaviour
{
    private GameObject buttonAction;
    [SerializeField]
    private bool interacable = true;
    public bool locked = false;
    private GameController3D gc3d;
    public QuickStartLobbyController lobby;
    public GameObject modePanel;

    private void Start()
    {
        buttonAction = GameObject.FindGameObjectWithTag("ButtonAction");
        gc3d = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController3D>();
        
    }

    private void ActionPerform()
    {
        if (interacable && !locked)
        {
            modePanel.SetActive(true);
        }         
    }

    private void ChangeText(Text actionText)
    {
        if (gc3d.result == "win")
            switch (gc3d.ngay)
            {
                case 0:
                case 1:
                case 2:

                    break;
                default:
                    locked = false;
                    break;
            }
        if (!locked)
        {
            switch (gc3d.languages)
            {
                case "en":
                    actionText.text = "To the show";
                    break;
                case "vi":
                    actionText.text = "Đi diễn";
                    break;
            }
        }
        else
            switch (gc3d.languages)
            {
                case "en":
                    actionText.text = "Locked (I need to check the letter first!)";
                    break;
                case "vi":
                    actionText.text = "Bị khóa (Tôi cần phải đọc lá thư trước!)";
                    break;
            }
    }

    public void ButtonPlay()
    {
        StartCoroutine(ToPlay2D());
    }

    IEnumerator ToPlay2D()
    {
        interacable = false;
        switch (gc3d.ngay)
        {
            case 1:
                gc3d.fadeOutBlack.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                if (!lobby.online)
                {
                    PhotonNetwork.Disconnect();
                    yield return new WaitForSeconds(0.5f);
                    PhotonNetwork.OfflineMode = true;
                }
                lobby.QuickStart();
                break;
            default:
                gc3d.fadeOutBlack.SetActive(true);
                yield return new WaitForSeconds(1.5f);
                if (!lobby.online)
                {
                    PhotonNetwork.Disconnect();
                    yield return new WaitForSeconds(0.5f);
                    PhotonNetwork.OfflineMode = true;
                }
                lobby.QuickStart();
                break;
        }
        yield return new WaitForSeconds(1);
        interacable = true;
    }
}
