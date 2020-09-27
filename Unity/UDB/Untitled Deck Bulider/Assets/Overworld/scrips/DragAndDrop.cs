using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject card;
    Vector3 startPostion;
    public Transform startParent;
    public GameObject Mask;
    // Start is called before the first frame update
    public void OnBeginDrag(PointerEventData eventData)
    {
        card = gameObject;
        startPostion = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        Mask.GetComponent<Mask>().enabled = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        card = null;
        Mask.GetComponent<Mask>().enabled = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPostion;
        }
    }

}
