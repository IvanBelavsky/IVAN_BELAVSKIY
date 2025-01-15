using System;
using UnityEngine;

public interface IGameView
{
    event Action<string, Vector3> OnItemDragStart;
    event Action<string, Vector3> OnItemDragEnd;
    event Action<string, Vector3, float> OnItemDrag;
    event Action<float, float> OnScroll; 
    
    void AddDraggableItem(string id, DraggableItem item);
    void UpdateItemPosition(string id, Vector3 position);
    void UpdateItemDepth(string id, float depth);
    void SetItemGrabbed(string id, bool isGrabbed);
    void SetItemOnShelf(string id, bool isOnShelf);
    void ScrollScene(float horizontalAmount, float verticalAmount); 
}