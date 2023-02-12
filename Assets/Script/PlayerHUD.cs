using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    //[SerializeField] private GameObject ExclamationMark;
    //[SerializeField] private GameObject QuestionMark;

    [SerializeField] private List<EmojiReference> listEmoji;
    private PlayerHUDEnum currentMark;
    private PlayerHUDEnum defaultMark = PlayerHUDEnum.HAMPA;
    private bool isActive = false;

    private void Start()
    {
        isActive = false;
        currentMark = defaultMark;
        DeactiveAll();
    }

    private void Update()
    {
        //TO DO : PlayerHUD selalu diupdate juga.
        AutoPlayMark();
    }

    public void AutoPlayMark()
    {
        if (isActive)
        {
            SwitchEmoji(currentMark);
        }
        else
        {
            SwitchEmoji(defaultMark);
        }
    }

    public void ChangeMark(PlayerHUDEnum markType, bool markerStatus)
    {
        SetBoolean(markerStatus);
        SetMark(markType);
    }

    private void SetBoolean(bool value)
    {
        isActive = value;
    }

    private void SetMark(PlayerHUDEnum type)
    {
        currentMark = type;
    }

    private void SwitchEmoji(PlayerHUDEnum emojiCode)
    {
        DeactiveAll();
        EmojiReference target = listEmoji.Find(targetSearch => targetSearch.emojiCode == emojiCode);
        target.emojiReference.SetActive(true);
    }

    /*
    private void SwitchCaseEmoji(PlayerHUDEnum markType)
    {
        DeactiveAll();
        switch (markType)
        {
            case PlayerHUDEnum.HAMPA:
                {
                    //HAMPA selalu false, lewati saja
                    break;
                }
            case PlayerHUDEnum.SERU:
                {
                    //Tanda seru
                    ExclamationMark.SetActive(isActive);    //Active
                    break;
                }
            case PlayerHUDEnum.TANYA:
                {
                    //Tanda tanya
                    QuestionMark.SetActive(isActive);       //Active
                    break;
                }
        }
    }
    */
    private void DeactiveAll()
    {
        foreach (EmojiReference emoji in listEmoji)
        {
            emoji.emojiReference.SetActive(false);
        }
    }

}

public enum PlayerHUDEnum
{
    HAMPA = 0,
    SERU = 1,
    TANYA = 2
}

[System.Serializable]
public class EmojiReference
{
    public PlayerHUDEnum emojiCode;
    public GameObject emojiReference;

    public EmojiReference SearchByCode(PlayerHUDEnum code)
    {
        return this;
    }
}
