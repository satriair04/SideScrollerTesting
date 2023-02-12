using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Penghubung ItemData agar tersimpan ke Instance InventoryManager
public class ItemTrigger : InteractableChecker, IFInteractable
{
    [Header("ItemTrigger property")]
    public ItemData targetItem;
    public bool destroyAfterTaking = true;  //Untuk kasus item tunggal
    public bool invokeExtraEvent = false;
    public UnityEvent extraEvent;

    new private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void TakeItem()
    {
        if (targetItem == null && invokeExtraEvent == true)
        {
            //Mainkan event tambahan. Cek dahulu item sudah diambil & boolean flag-nya.
            extraEvent?.Invoke();
            Debug.Log("Extra event dijalankan");
            return;
        }
        if (targetItem == null)
        {
            //Menangkap situasi dimana invokeExtraEvent == false tetapi targetItem == null
            //Keluar dari method. Cegah null ditambah ke InventoryManager.
            return;
        }
        //Masukan item & hapus referensi item
        InventoryManager.Instance.AddToInventory(targetItem);
        targetItem = null;

        ConditionalChange();

        //Jika object cuma aktif sekali pada scene
        if (destroyAfterTaking)
        {
            Destroy(this.gameObject);
        }
    }

    private void ConditionalChange()
    {
        //Update PlayerHUD
        this.markType = PlayerHUDEnum.HAMPA;
        //Jalankan methodnya sekali
        FindObjectOfType<PlayerHUD>().ChangeMark(this.markType, false);
        FindObjectOfType<PlayerHUD>().AutoPlayMark();
        //PlayerHUD.Instance.ChangeMark(this.markType, isPlayerTouching);
    }

    

    public void Interact()
    {
        //IFInteractable contract. Biarkan kosong atau beri implementasi lain
        TakeItem();
    }

}
