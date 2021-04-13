using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHand : MonoBehaviour
{
    private CItem GrabbingItem;

    public bool IsHavingItem()
    {
        return GrabbingItem != null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public CItem GetGrabbingItem()
    {
        CItem old_item = GrabbingItem;
        GrabbingItem = null;
        return old_item;
    }

    public void SetGrabbingItem(CItem item)
    {
        GrabbingItem = item;
    }

}
