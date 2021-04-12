using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CSlot : MonoBehaviour //IBeginDragHandler, IDragHandler
{
    private CItem Item;
    [SerializeField] private Image ItemImage;

    public void SetItem(CItem item)
    {
        Item = item;
        if (item != null)
        {
            ItemImage.sprite = item.ItemImage;
        }

    }
   
}
