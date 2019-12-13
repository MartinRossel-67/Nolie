using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Kigurumi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameController gameController;
    SpriteRenderer spriteRenderer;

    [SerializeField] private int index;

    [HideInInspector] public Vector3 startPosition;
    [SerializeField] private Sprite sprite;

    void Start()
    {
        gameController = FindObjectOfType<GameController>().GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;

        //spriteRenderer.sprite = sprite;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        GetComponentInParent<GraphicRaycaster>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        GetComponentInParent<GraphicRaycaster>().enabled = true;

        
        if (eventData.pointerEnter != null)
        {
            if (eventData.pointerEnter.gameObject.tag == "Player")
            {
                Debug.Log("Player");
                gameController.NextNode(index);
                gameObject.GetComponentInParent<Canvas>().enabled = false;
            }
        }
    }
}