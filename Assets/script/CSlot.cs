using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    private CItem Item;
    [SerializeField] private Image ItemImage;

    private GameObject DraggingObj;

    [SerializeField] private GameObject ItemImageObj;

    private Transform CanvasTransform;

    private CHand Hand;

    void Start()
    {
        CanvasTransform = GameObject.Find("Canvas").transform;
        Hand = FindObjectOfType<CHand>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item == null)
        {
            return;
        }
        DraggingObj = Instantiate(ItemImageObj, CanvasTransform);

        DraggingObj.transform.SetAsLastSibling();

        ItemImage.color = Color.gray;

        Hand.SetGrabbingItem(Item);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Item == null)
        {
            return;
        }

        DraggingObj.transform.position = Hand.transform.position;

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!Hand.IsHavingItem())
        {
            return;
        }

        CItem get_item = Hand.GetGrabbingItem();

        Hand.SetGrabbingItem(Item);

        SetItem(get_item);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(DraggingObj);

        CItem get_item = Hand.GetGrabbingItem();

        SetItem(get_item);
    }

    public void SetItem(CItem item)
    {
        Item = item;
        if (item != null)
        {
            ItemImage.color = new Color(1,1,1,1);
            ItemImage.sprite = item.ItemImage;
        }
        else
        {
            ItemImage.color = new Color(0,0,0,0);
        }

    }
}
