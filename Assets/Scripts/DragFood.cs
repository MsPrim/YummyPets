using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragFood : MonoBehaviour
{
    public GameObject foodPrefab; // Prefab to spawn in world

    private Canvas canvas;  //Need the canvas to the dragging works
    private RectTransform rectTransform;   //The transform used for UI elements
    private Vector2 originalPosition;   //Put the icon back in the end (after it is dragged) 
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    //Rememeber where the icon originally was when the player starts dragging 
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    //Moves UI icon with your finger 
    public void OnDrag(PointerEventData eventData)  //how far the finger moved since last frame
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //fixes movement speed if UI is scaled
    }

    //When the player releases the food: convert screen space to world space to spawn food in the scene
    public void OnEndDrag(PointerEventData eventData)
    {
        // Spawn food in world
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0; // Keep on 2D plane
        Instantiate(foodPrefab, worldPos, Quaternion.identity);

        // Return icon to original position
        rectTransform.anchoredPosition = originalPosition;
    }
}
