using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public GameObject dialogPanel;

    private bool flagDialogActive;
    private bool flagDialogEnded;
    private Story currentInkyStory;
    private TextAsset inkDialogJSON;

    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        flagDialogActive = false;
        dialogPanel.SetActive(flagDialogActive);
    }

    private void Update()
    {
        if (!flagDialogActive)
        {
            return;
        }
        if (Input.GetKeyDown(InputManager.Instance.returnKey))
        {
            //Lanjut ke line berikutnya jika dialog sedang aktif 
            ContinueNextLine();
        }
    }

    public void StartInkDialog(TextAsset inkDialogJSON)
    {
        if (inkDialogJSON == null)
        {
            Debug.Log("Dialognya ga ada");
            return;
        }
        currentInkyStory = new Story(inkDialogJSON.text);
        flagDialogActive = true;
        dialogPanel.SetActive(flagDialogActive);
        ContinueNextLine();
        Debug.Log("Dialog dijalankan, Method StartInkDialog telah dieksekusi");
    }

    public void ContinueNextLine()
    {
        if (currentInkyStory.canContinue)
        {
            //Continue() untuk return string dari satu line aja
            dialogPanel.GetComponent<DialogUI>().SetDialogLinetext(currentInkyStory.Continue());
            //Cek jika ada choices
            if (currentInkyStory.currentChoices.Count > 0)
            {
                //Cetak tombol
            }
        }
        else
        {
            ExitInkDialog();
        }
    }

    public void ExitInkDialog()
    {
        flagDialogActive = false;
        dialogPanel.SetActive(flagDialogActive);
        dialogPanel.GetComponent<DialogUI>().SetDialogLinetext("");
        currentInkyStory = null;
    }

    private void CreateInstance()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
