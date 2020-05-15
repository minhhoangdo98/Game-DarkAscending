using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //Script duoc dung boi Main
    public PlayerController2d player;


    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponent<PlayerController2d>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("Dat"))
            player.grounded = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("Dat"))
            player.grounded = false;
    }
}
