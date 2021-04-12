using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    [SerializeField] private GameObject SlotPrefab;

    private int SlotNumber = 20;

    [SerializeField] private CItem[] ALLItem;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SlotNumber; i++)
        {
            GameObject slot_obj = Instantiate(SlotPrefab, transform);

            CSlot slot = slot_obj.GetComponent<CSlot>();

            if (i < ALLItem.Length)
            {
                slot.SetItem(ALLItem[i]);
            }
            else
            {
                slot.SetItem(null);
            }
        }
    }
}
