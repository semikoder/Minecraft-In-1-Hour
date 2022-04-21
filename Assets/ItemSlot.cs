using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image itemIconImage;
    public TMP_Text itemCountText;

    public KeyCode slotKeyCode;

    private Item holdingItem;

    public void UpdateSlot (Item itemInfo, int count)
    {
        itemIconImage.color = Color.white;
        itemIconImage.sprite = itemInfo.itemIcon;
        itemCountText.text = count.ToString();

        holdingItem = itemInfo;
    }

    public void ClearSlot ()
    {
        itemIconImage.color = Color.clear;
        itemCountText.text = "";
    }

    public Item SelectSlot ()
    {
        return holdingItem;
    }
}
