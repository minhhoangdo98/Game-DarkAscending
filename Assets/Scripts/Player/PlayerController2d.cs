using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2d : MonoBehaviourPun
{
    //script duoc dung boi  main
    public float speed = 150f, maxspeed = 1, jumpPow = 350f, defaultSpeed;
    public bool grounded = true, faceright = false, doubleJump = false, death = false;
    public Rigidbody2D r2;
    public Animator anim;
    public bool diChuyen, traiPhai, dieuKhien = true;


    void Start()
    {
        SetValuesStart();
    }

    private void SetValuesStart()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
        r2 = gameObject.GetComponent<Rigidbody2D>();//Lay nhan vat
        anim = gameObject.GetComponent<Animator>();//Bien chua animation cho Player
        diChuyen = true;//co the di chuyen
        traiPhai = true;
        defaultSpeed = speed;//speed ban dau
        if (photonView.IsMine)
        {
            GameObject arrow = Instantiate(Resources.Load("Prefabs/PlayerArrow"), gameObject.transform.position, Quaternion.identity) as GameObject;
            arrow.GetComponent<PlayerArrow>().targetPlayer = gameObject;
        }
    }

    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (photonView.IsMine)
        {

            SetAnimatiorAndValuesUpdate();

            JumpAndRun();
        }

    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
            DiChuyenNhanVat();
    }

    private void SetAnimatiorAndValuesUpdate()
    {
        anim.SetBool("Grounded", grounded);//animation khi dung yen tren mat dat (grounded = true)
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x)); // Mathf.abs: tra ve gia tri duong ; r2.velocity.x: toc do hien tai, animation khi chay
        anim.SetBool("Death", death);//animation khi death

        //if (!tHGameController.thBattle || tHGameController.isGameOver || tHGameController.isWin)//Kiem tra de cho phep dieu khien nhan vat
        //{
        //    dieuKhien = false;
        //}
        //if (tHGameController.thBattle)
        //{
        //    dieuKhien = true;
        //}
    }

    private void JumpAndRun()
    {
        if (dieuKhien)
        {
            if (Input.GetButtonDown("Jump") && diChuyen) // neu nut an xuong cua nguoi choi la Space va dang cho phep di chuyen (diChuyen = true)
            {
                gameObject.GetComponent<GroundCheck>();//Goi ham kiem tra xem Player co dang dung tren mat dat hay khong
                if (grounded)//neu dang dung tren mat dat
                {
                    grounded = false;//cho grounded = false tuc la nguoi choi se nhay len khong
                    doubleJump = true;//co the nhay tiep lan 2
                    r2.AddForce(Vector2.up * jumpPow);//thay doi vi tri nhan vat len tren dua vao jumpPow
                }
                else//Nguoc lai, Neu khong dung tren mat dat
                {
                    if (doubleJump)//neu chua nhay lan 2
                    {
                        doubleJump = false;//nhay tiep lan 2 va khong the nhay them nua
                        r2.velocity = new Vector2(r2.velocity.x, 0);
                        r2.AddForce(Vector2.up * jumpPow * 0.8f);
                    }
                }
            }
        }
    }

    private void DiChuyenNhanVat()
    {
        if (diChuyen && dieuKhien)//neu cho phep di chuyen (diChuyen = true)
        {
            float h = Input.GetAxis("Horizontal");//Lay thong tin nut bam la nut mui ten (Phai: 1, Trai: -1)
            r2.AddForce(Vector2.right * speed * h);//Thay doi vi tri nhan vat dua vao speed va h

            //Ham gioi han toc do di chuyen
            if (r2.velocity.x > maxspeed) //Gioi han toc do di ve ben phai
                r2.velocity = new Vector2(maxspeed, r2.velocity.y);
            if (r2.velocity.x < -maxspeed)// Gioi han toc do di ve ben trai
                r2.velocity = new Vector2(-maxspeed, r2.velocity.y);

            if (h > 0 && !faceright)//Neu h > 0 tuc la ben phai va player dang quay ve ben trai va chua trong trang thai tan cong
            {
                Flip();//Goi ham dao chieu 
            }
            if (h < 0 && faceright)//Neu h < 0 tuc la ben trai va player dang quay ve ben phai va chua trong trang thai tan cong
            {
                Flip();
            }

            if (grounded)
            {
                r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
            }

            if (!grounded)
            {
                r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
            }

        }
    }

    public void Flip() // Chuyen huong nhan vat
    {
        faceright = !faceright;
        gameObject.GetComponent<Transform>().localScale = new Vector3(-gameObject.GetComponent<Transform>().localScale.x, 1, 1);
    }

}
