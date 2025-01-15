using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModel
{
    private Dictionary<string, ItemModel> _items;
    private List<ShelfModel> _shelves;

    public GameModel()
    {
        _items = new Dictionary<string, ItemModel>();
        _shelves = new List<ShelfModel>();
    }

    public void AddItem(string id, string type, Vector3 position)
    {
        _items[id] = new ItemModel(id, type, position);
    }

    public void AddShelf(Rect bounds, float depth)
    {
        _shelves.Add(new ShelfModel(bounds, depth));
    }

    public ItemModel GetItem(string id)
    {
        return _items.ContainsKey(id) ? _items[id] : null;
    }

    public ShelfModel GetShelfAt(Vector2 position)
    {
        return _shelves.FirstOrDefault(shelf => shelf.Bounds.Contains(position));
    }

    public ShelfModel GetShelfBelow(Vector2 position)
    {
        return _shelves
            .Where(shelf => shelf.Bounds.x <= position.x &&
                            shelf.Bounds.x + shelf.Bounds.width >= position.x &&
                            shelf.Bounds.y < position.y)
            .OrderByDescending(shelf => shelf.Bounds.y)
            .FirstOrDefault();
    }

    public void UpdateItemPosition(string id, Vector3 position)
    {
        if (_items.ContainsKey(id))
        {
            _items[id].Position = position;
            _items[id].Depth = position.z;
        }
    }

    public void SetItemOnShelf(string id, bool isOnShelf)
    {
        if (_items.ContainsKey(id))
        {
            _items[id].IsOnShelf = isOnShelf;
        }
    }
}