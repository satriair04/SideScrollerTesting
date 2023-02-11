using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public GameObject dialogPanel;          //Tinggal GetComponent<DialogUI> kalau butuh DialogUI

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

    //INITIALIZATION : DialogTrigger akan panggil ini untuk memulai dialog
    public void StartInkDialog(TextAsset inkDialogJSON)
    {
        if (inkDialogJSON == null)
        {
            //Debug.Log("Dialognya ga ada");
            return;
        }
        //PENTING : Set data & aktifkan panel UI-nya
        currentInkyStory = new Story(inkDialogJSON.text);   //PENTING : Convert inkJSON format ke data Story
        flagDialogActive = true;
        dialogPanel.SetActive(flagDialogActive);

        //PENTING : Untuk munculkan 1st line - dan seterusnya
        ContinueNextLine();
    }

    public void ContinueNextLine()
    {
        //PENTING : Cek dulu apakah masih bisa continue
        if (currentInkyStory.canContinue)
        {
            //Continue() untuk return string dari satu line aja
            dialogPanel.GetComponent<DialogUI>().SetDialogLinetext(currentInkyStory.Continue());

            //PENTING 50-50 :Cek jika ada implementasi choices pada file inkJSON.
            if (currentInkyStory.currentChoices.Count > 0)
            {
                //Aktifkan choice panel
                dialogPanel.GetComponent<DialogUI>().ActivateChoicePanel(true);
                //Set choices, bikin button, beri onClickListener pada button tadi 
                for (int i = 0; i < currentInkyStory.currentChoices.Count; i++)
                {
                    Choice choice = currentInkyStory.currentChoices[i];
                    dialogPanel.GetComponent<DialogUI>().CreateChoiceButton(choice.text.Trim(), choice);
                }
            }
            else
            {
                dialogPanel.GetComponent<DialogUI>().ActivateChoicePanel(false);
            }
        }
        else
        {
            ExitInkDialog();
        }
    }

    //Bakal dipanggil dari DialogUI
    public void SelectChoice(Choice choice)
    {
        if(currentInkyStory.currentChoices.Count <= 0 || currentInkyStory == null)
        {
            return;     //Error-catcher. Hentikan eksekusi
        }
        //PENTING : Membuat agar currentInkyStory melanjutkan cerita sesuai dengan choice yang dipilih.
        currentInkyStory.ChooseChoiceIndex(choice.index);
        ContinueNextLine();
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
