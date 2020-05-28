using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyItem : MonoBehaviour
{
    [SerializeField]
    private GameObject textAction;
    private GameObject buttonAction;
    [SerializeField]
    private bool interacable = true;
    private GameController3D gc3d;
    [SerializeField]
    private UnityEvent keyEvent;

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
            keyEvent.Invoke();
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
