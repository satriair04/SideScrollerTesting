using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

///<summary>
///Class dasar untuk berinteraksi dengan suatu gameObject dengan memicu UnityEvent
///</summary>
public class InteractableChecker : MonoBehaviour
{
    [Header("Base property")]
    public bool isInteractable = true;                      //Default
    public PlayerHUDEnum markType = PlayerHUDEnum.HAMPA;    //Default
    public bool invokeNextEvent = true;                     //Default
    public UnityEvent nextEvent;                            //Event yang akan di-Invoke ketika kondisi memenuhi

    public bool isPlayerTouching = false;                   //Default. Flag cek kolisi bersentuhan
    protected virtual void FixedUpdate()
    {
        InteractionCheck();
    }

    public void SetInteractible(bool value)
    {
        isInteractable = value;
    }

    ///<summary>
    ///Pengecekkan kondisi interaksi. Jika memenuhi dan tombol ditekan maka event bakal di-Invoke.
    ///</summary>
    protected void InteractionCheck()
    {
        //TO DO : Perbaiki bagian ini agar DialogTrigger ga perlu ditekan sebanyak dua kali.
        if (!isInteractable)
        {
            return;
        }
        if (!isPlayerTouching)
        {
            return;
        }
        //GetKeyDown memastikan block if ini hanya tereksekusi sekali
        if (Input.GetKey(InputManager.Instance.interactKey))
        {
            if(nextEvent != null)
            {
                nextEvent.Invoke();
            }
        }
    }
    
    //Collider trigger pada Base/Turunan dari objek ini dihandle oleh Player collider
    //Ubah boolean isPlayerTouching ketika Enter, Stay ataupun Exit

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerTouching = true;
        //PlayerDetection(collision, isPlayerTouching);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        isPlayerTouching = true;
        //PlayerDetection(collision, isPlayerTouching);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerTouching = false;
        //PlayerDetection(collision, isPlayerTouching);

    }
    
    /*
    ///<summary>
    ///Deteksi Class Player. Kayaknya mending pake collision.tag daripada cek Komponen.
    ///Jika Class player terdeteksi, mainkan HUD diatas kepala karakter.
    ///</summary>
    protected void PlayerDetection(Collider2D collision, bool markerStatus)
    {
        Collider2D temp = collision;
        Player checkPlayer = temp.GetComponent<Player>();
        if (checkPlayer != null)
        {
            //Properti playerHUD adalah GameObject pada class Player
            //checkPlayer.playerHUD.GetComponent<PlayerHUD>().ChangeMark(markType, markerStatus);
            Debug.Log("UPDATE HUD: GameObject = " + gameObject.name + " markType = " + markType + " markerStatus = " + markerStatus);
            PlayerHUD.Instance.ChangeMark(markType, markerStatus);
        }
    }

    */
}
