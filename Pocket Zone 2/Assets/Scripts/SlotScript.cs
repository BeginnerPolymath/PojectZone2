using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotScript : MonoBehaviour
{
    public ItemSO Item;
    public Image ImgItem;

    public TMP_Text TextCount;

    public int Count;

    public InventoryScirpt Inventory;

    public void OpenItemInfoPanel (SlotScript slot)
    {
        if(Item != null)
        {
            Inventory.OpenItemInfoPanel();
            Inventory.TargetSlot = slot;
        }
    }

    public void AddCount (int value)
    {
        Count += value;
        TextCount.text = Count.ToString();
    }

    public void SetCount (int value)
    {
        Count = value;
        TextCount.text = Count.ToString();
    }


    public void ClearSlot ()
    {
        ImgItem.sprite = null;
        Item = null;
        Count = 0;
        
        TextCount.gameObject.SetActive(false);
        ImgItem.gameObject.SetActive(false);
    }

    public void SetItemSlot (ItemSO item, int count)
    {
        ImgItem.sprite = item.Image;
        ImgItem.gameObject.SetActive(true);
        SetCount(count);
        Item = item;

        if(item.MaxStack > 1)
        {
            TextCount.gameObject.SetActive(true);
            TextCount.text = Count.ToString();
        }
    }
}
