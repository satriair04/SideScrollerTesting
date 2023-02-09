using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DialogTrigger : InteractableChecker, IFInteractable
{
    [Header("DialogTrigger property")]
    public TextAsset inkDialogJSON;
    public bool invokeExtraEvent = false;
    public UnityEvent extraEvent;
    
    // Update is called once per frame
    new private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void InitializeDialog()
    {
        //Pass inkJSON ke DialogManager untuk memainkan event percakapan
        if (inkDialogJSON == null)
        {
            return;
        }
        else
        {
            DialogManager.Instance.StartInkDialog(inkDialogJSON);
        }
    }

    
    public void Interact()
    {
        //IFInteractable contract. Biarkan kosong atau beri implementasi lain
        InitializeDialog();
    }
}
