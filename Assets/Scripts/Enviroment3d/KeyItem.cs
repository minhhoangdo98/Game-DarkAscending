using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyItem : MonoBehaviour
{
    [SerializeField]
    private GameObject objectUnlock, textAction;
    private GameObject buttonAction;
    [SerializeField]
    private bool interacable = true;
    private GameController3D gc3d;

    private void Start()
    {
        buttonAction = GameObject.FindGameObjectWithTag("ButtonAction");
        gc3d = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController3D>();
    }

    private void ActionPerform()
    {
        if (interacable)
        {
            textAction.GetComponent<Text>().text = "";
            gameObject.SetActive(false);
            objectUnlock.GetComponent<OpenIronDoor>().locked = false;
            textAction.SetActive(false);
        }
    }

    private void ChangeText(Text actionText)
    {
        switch (gc3d.languages)
        {
            case "en":
                actionText.text = "Take";
                break;
            case "vi":
                actionText.text = "Lấy";
                break;
        }
    }

}
