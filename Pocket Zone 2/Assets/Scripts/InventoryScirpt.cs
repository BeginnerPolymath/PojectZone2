using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryScirpt : MonoBehaviour
{
    public RectTransform ItemInfoPanel;
    public TMP_Text ItemName;

    public CanvasGroup CanvasGroup;

    public SlotScript TargetSlot;

    public void OpenItemInfoPanel ()
    {
        ItemInfoPanel.gameObject.SetActive(!ItemInfoPanel.gameObject.activeSelf);
        ItemInfoPanel.anchoredPosition = Input.mousePosition;
    }

    public void RemoveItem ()
    {
        TargetSlot.ClearSlot();

        TargetSlot = null;

        ItemInfoPanel.gameObject.SetActive(false);
    }

    public void OpenCloseInventory ()
    {
        WorldScript.SwitchCanvasGroup(CanvasGroup);
    }
}
