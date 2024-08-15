using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItemSlot : MonoBehaviour
{
    public Image itemImage;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();   
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
