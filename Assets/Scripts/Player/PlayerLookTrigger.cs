using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLookTrigger : MonoBehaviour
{
    public Text actionText;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractObject"))
        {
            actionText.gameObject.SetActive(true);
            other.transform.SendMessage("ChangeText", actionText, SendMessageOptions.DontRequireReceiver);
            if (Input.GetButtonDown("Action") || TCKInput.GetAction("ActionButton", EActionEvent.Press))
            {
                other.transform.SendMessage("ActionPerform", SendMessageOptions.DontRequireReceiver);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractObject"))
        {
            actionText.text = "";
            actionText.gameObject.SetActive(false);
        }

    }
}
