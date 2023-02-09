using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDesc;
    public Sprite itemThumbnail;
    public Sprite itemFullSprite;
    [SerializeField] private string memo;   //Sekedar catatan untuk asset SO
}
