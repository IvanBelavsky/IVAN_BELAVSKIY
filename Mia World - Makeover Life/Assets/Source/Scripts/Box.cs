using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Box : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _layerObject;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out DraggableItem draggableItem))
        {
            draggableItem.GetComponent<SpriteRenderer>().renderingLayerMask =
                gameObject.GetComponent<SpriteRenderer>().renderingLayerMask - 1;
        }
    }
}