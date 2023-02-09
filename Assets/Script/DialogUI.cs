using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUI : MonoBehaviour
{
    [Header("Property")]
    public Transform speakerNameTMP;
    public Transform dialogLineTMP;
    public Button nextLineButton;
    public Button logButton;    //belum
    public Button HideButton;
    public Button skipButton;   //Belum. Close button + dialog finished = true
    public Button closeButton;  //Tutup paksa dialog, percakapan tersebut diangggap belum selesai.
    [Header("Parent n Child for Tombol Choices")]
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
}
