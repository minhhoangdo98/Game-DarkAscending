using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractObject : MonoBehaviour
{
    private GameController3D gc3d;
    private GameObject buttonAction;
    [SerializeField]
    private bool open = false, interacable = true;
    [SerializeField]
    private bool locked = false;
    [Header("Sound")]
    [SerializeField]
    private AudioClip openSound;
    [SerializeField]
    private AudioClip closeSound;
    [Header("Text String")]
    [SerializeField]
    private string enLockText = "Locked";
    [SerializeField]
    private string viLockText = "Bị khóa", enOpenText = "Open", enCloseText = "Close", viOpenText = "Mở", viCloseText = "Đóng";
    [Header("Animation String")]
    [SerializeField]
    private string animationOpen;
    [SerializeField]
    private string animationClose;
    [Header("Action perform event")]
    [SerializeField]
    private UnityEvent EventPerform;

    private void Start()
    {
        buttonAction = GameObject.FindGameObjectWithTag("ButtonAction");
        gc3d = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController3D>();
    }

    private void ActionPerform()
    {
        if (interacable && !locked)
        {
            EventPerform.Invoke();
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
                        actionText.text = enOpenText;
                        break;
                    case "vi":
                        actionText.text = viOpenText;
                        break;
                }
            else
                switch (gc3d.languages)
                {
                    case "en":
                        actionText.text = enCloseText;
                        break;
                    case "vi":
                        actionText.text = viCloseText;
                        break;
                }
        }
        else
            switch (gc3d.languages)
            {
                case "en":
                    actionText.text = enLockText;
                    break;
                case "vi":
                    actionText.text = viLockText;
                    break;
            }
    }

    public void UnlockObject()
    {
        locked = false;
    }

    public void LockObject()
    {
        locked = true;
    }

    public void OpenCloseObject()
    {
        StartCoroutine(OpenCloseObj());
    }

    //Hand dong mo/dong cua
    IEnumerator OpenCloseObj()
    {
        interacable = false;
        if (!open)
        {
            gameObject.GetComponent<AudioSource>().clip = openSound;
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animation>().Play(animationOpen);
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = closeSound;
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animation>().Play(animationClose);
        }
        yield return new WaitForSeconds(1);
        open = !open;
        interacable = true;
    }
}
