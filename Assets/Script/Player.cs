using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour { 

    public float moveSpeed = 15f;
    public float constantVelocity = 50f;
    //public KeyCode moveJump = KeyCode.Space;    //Belum diimplementasikan
    //public KeyCode interactKey = KeyCode.F;

    [SerializeField] private bool usingFixedVelocity = false;
    //[SerializeField] public PlayerHUD playerHUD;
    private Rigidbody2D rb2d;
    private Vector2 playerDirection;
    private bool isFacingRight = true;
    

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerDirection = Vector2.zero;
    }

    void Update()
    {
        InputDetection();   //Deteksi input, Gerakin di FixedUpdate
        FlipCharacter();    //Deteksi arah untuk ubah posisi kiri/kanan karakter
    }

    private void FixedUpdate()
    {
        PlayerMovement(playerDirection);
    }

    /*DISABLE
    private void OnTriggerStay2D(Collider2D collision)
    {
        Collider2D tempCollision = collision;
        IFInteractable interactable = tempCollision.GetComponent<IFInteractable>();
        if (interactable == null)
        {
            return;
        }
        if (tempCollision.GetComponent<ItemTrigger>().targetItem != null)
        {
            playerHUD.MarkActive();
        }
        else
        {
            playerHUD.MarkDeactive();
        }
        Debug.Log("Menyentuh: " + collision.name);
        if (Input.GetKey(interactKey))
        {
            interactable.Interact();
            Debug.Log("Interaksi berhasil!");
        }
    }
    END*/

    //Custom private Methods/function :
    private void InputDetection()
    {
        if (Input.GetKey(InputManager.Instance.playerMoveLeft))
        {
            playerDirection = Vector2.left;
            //Debug.Log("Tombol " + moveLeft + " ditekan");
        }
        else if (Input.GetKey(InputManager.Instance.playerMoveRight))
        {
            playerDirection = Vector2.right;
            //Debug.Log("Tombol " + moveRight + " ditekan");
        }
        else
        {
            playerDirection = Vector2.zero;
        }
    }
    private void PlayerMovement(Vector2 direction)
    {
        if (usingFixedVelocity)
        {
            rb2d.velocity = new Vector2(direction.x * moveSpeed, rb2d.velocity.y);
            return;
        }
        //Memberi efek jalan licin
        rb2d.AddForce(direction * Time.deltaTime * moveSpeed, ForceMode2D.Impulse);
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, constantVelocity);
    }
    private void FlipCharacter()
    {
        if(playerDirection.x > 0 && !isFacingRight)
        {
            //Kekanan tapi ga hadap kanan
            FlipPlayer();
        }
        else if(playerDirection.x < 0 && isFacingRight)
        {
            //Kekiri tapi ga hadap kiri
            FlipPlayer();
        }
    }
    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        //x, y, z rotasi kepala
        transform.Rotate(0f, 180f, 0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableDetection(collision, true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        InteractableDetection(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {;
        InteractableDetection(collision, false);

    }


    ///<summary>
    ///Deteksi Class Player. Kayaknya mending pake collision.tag daripada cek Komponen.
    ///Jika Class player terdeteksi, mainkan HUD diatas kepala karakter.
    ///</summary>
    protected void InteractableDetection(Collider2D collision, bool markerStatus)
    {
        Collider2D temp = collision;
        InteractableChecker check = temp.GetComponent<InteractableChecker>();
        if (check != null)
        {
            //Properti playerHUD adalah GameObject pada class Player
            //checkPlayer.playerHUD.GetComponent<PlayerHUD>().ChangeMark(markType, markerStatus);
            Debug.Log("UPDATE HUD: GameObject = " + gameObject.name + " markType = " + check.markType + " markerStatus = " + markerStatus);
            PlayerHUD.Instance.ChangeMark(check.markType, markerStatus);
        }
    }
}
