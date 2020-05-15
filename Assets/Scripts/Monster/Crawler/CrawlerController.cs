using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : MonoBehaviour
{
    public bool idle, walk, run, attack;
    public float speed = 10f;
    private Animator anim;
    public GameObject player;
    public GameController3D gc3d;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        gc3d = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController3D>();
        idle = true;
    }

    private void Update()
    {
        anim.SetBool("Idle", idle);
        anim.SetBool("Walk", walk);
        anim.SetBool("Run", run);
        anim.SetBool("Attack", attack);
    }

    private void FixedUpdate()
    {
        if (walk || run || attack)
        {
            //gameObject.GetComponent<Rigidbody>().MovePosition(transform.position - Vector3.right * speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
    }
}
