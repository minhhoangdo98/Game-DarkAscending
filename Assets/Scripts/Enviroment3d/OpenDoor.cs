using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    private GameObject buttonAction;
    [SerializeField]
    private bool open = false, interacable = true;
    [SerializeField]
    private AudioClip openSound, closeSound;
    public bool locked = false;
    private GameController3D gc3d;

    private void Start()
    {
        buttonAction = GameObject.FindGameObjectWithTag("ButtonAction");
        gc3d = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController3D>();
    }

    private void ActionPerform()
    {
        if (interacable && !locked)
        {
            StartCoroutine(OpenCloseDoor());
        }         
    }

    private void ChangeText(Text actionText)
    {
        if (!locked)
        {
            if (!open)
                switch (gc3d.languages)
                {
                    case "en":
                        actionText.text = "Open";
                        break;
                    case "vi":
                        actionText.text = "Mở";
                        break;
                }
            else
                switch (gc3d.languages)
                {
                    case "en":
                        actionText.text = "Close";
                        break;
                    case "vi":
                        actionText.text = "Đóng";
                        break;
                }
        }
        else
            switch (gc3d.languages)
            {
                case "en":
                    actionText.text = "Locked";
                    break;
                case "vi":
                    actionText.text = "Bị khóa";
                    break;
            }
    }

    //Hand dong mo dong cua
    IEnumerator OpenCloseDoor()
    {
        interacable = false;
        if (!open)
        {
            gameObject.GetComponent<AudioSource>().clip = openSound;
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animation>().Play("DoorOpen");
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = closeSound;
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animation>().Play("DoorClose");
        }
        yield return new WaitForSeconds(1);
        open = !open;
        interacable = true;
    }

}
