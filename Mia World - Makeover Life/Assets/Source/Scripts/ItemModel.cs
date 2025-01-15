using UnityEngine;

public class ItemModel
{
    public string Id { get; private set; }
    public string Type { get; set; }       
    public Vector3 Position { get; set; }   
    public float Depth { get; set; }        
    public bool IsGrabbed { get; set; }     
    public bool IsOnShelf { get; set; }   
        
    public ItemModel(string id, string type, Vector3 position)
    {
        Id = id;
        Type = type;
        Position = position;
        Depth = position.z;
        IsGrabbed = false;
        IsOnShelf = false;
    }
}