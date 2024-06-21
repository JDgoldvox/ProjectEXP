using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    Image itemImage;
    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag.gameObject;
        obj.transform.SetParent(transform);
        obj.transform.position = itemImage.transform.position;
    }
}
