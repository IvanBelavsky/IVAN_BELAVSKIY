using UnityEngine;

public class ShelfModel
{
    public Rect Bounds { get; private set; }
    public float Depth { get; private set; }
        
    public ShelfModel(Rect bounds, float depth)
    {
        Bounds = bounds;
        Depth = depth;
    }
}