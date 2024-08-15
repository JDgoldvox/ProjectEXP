using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Image itemImage;
    private void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag.gameObject;
        obj.transform.SetParent(transform);
        obj.transform.position = itemImage.transform.position;

        //updates items in hotbar however, happens if any inventory slot is changed
        Hotbar.Instance.UpdateHotbarImagesFromInventoryHotbar();
    }
}
