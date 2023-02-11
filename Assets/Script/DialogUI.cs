using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
public class DialogUI : MonoBehaviour
{
    [Header("Property")]
    public Transform speakerNameTMP;
    public Transform dialogLineTMP;
    public Button nextLineButton;
    public Button logButton;    //belum. Butuh panel toogle-able untuk menyimpan List dialog yang sudah lewat
    public Button HideButton;   //Belum. Butuh event untuk deteksi input lain untuk un-hide
    public Button skipButton;   //Belum. Close button + dialog finished = true
    public Button closeButton;  //Tutup paksa dialog, percakapan tersebut diangggap belum selesai.

    [Header("Parent n Child for Tombol Choices")]
    [SerializeField] private Transform choicesRectTransformWrapper;
    [SerializeField] private Transform choicesButtonParentSpawner;
    [SerializeField] private Transform choicesButtonChildTemplate;

    private bool isActiveState;
    
    private void Start()
    {
        SetButtonListener();
        isActiveState = false;
        gameObject.SetActive(isActiveState);
    }

    public void SetDialogLinetext(string text)
    {
        dialogLineTMP.GetComponent<TextMeshProUGUI>().text = text;
    }

    

    public void ToogleUI()
    {
        isActiveState = !isActiveState;
        gameObject.SetActive(isActiveState);
    }

    private void SetButtonListener()
    {
        nextLineButton.onClick.AddListener(() => {
            NextButtonListener();
        });

        closeButton.onClick.AddListener(() => {
            CloseButtonListener();
        });
    }

    private void NextButtonListener()
    {
        if (DialogManager.Instance == null)
        {
            return;
        }
        else
        {
            DialogManager.Instance.ContinueNextLine();
        }
    }

    private void CloseButtonListener()
    {
        //Selalu menghasilkan false, tenang saja
        if (DialogManager.Instance == null)
        {
            return;
        }
        else
        {
            DialogManager.Instance.ExitInkDialog();
        }
    }

    public void ActivateChoicePanel(bool status)
    {
        choicesRectTransformWrapper.gameObject.SetActive(status);
        ClearAllChoiceButton();
    }

    public Button CreateChoiceButton(string text, Choice choice)
    {
        //Ciptakan choice button beserta event onClick-nya
        Transform choiceButton = Instantiate(choicesButtonChildTemplate, choicesButtonParentSpawner);
        choiceButton.gameObject.SetActive(true);
        choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
        choiceButton.GetComponent<Button>().onClick.AddListener(() => { 
            ChoiceButtonListener(choice);
        });
        //Balikin referensi tombolnya agar bisa diberi onClick.AddListener
        return choiceButton.GetComponent<Button>();
    }

    private void ClearAllChoiceButton()
    {
        foreach (Transform child in choicesButtonParentSpawner)
        {
            Destroy(child.gameObject);
        }
    }

    private void ChoiceButtonListener(Choice selectedChoice)
    {
        //Selalu menghasilkan false, tenang saja
        if (DialogManager.Instance == null)
        {
            return;
        }
        else
        {
            //Kirim choice kembali ke DialogManager
            DialogManager.Instance.SelectChoice(selectedChoice);
        }
    }
}
