using UnityEngine;

public class GamePresenter
{
    private readonly GameModel _model;
    private readonly IGameView _view;

    public GamePresenter(GameModel model, IGameView view)
    {
        this._model = model;
        this._view = view;

        view.OnItemDragStart += HandleItemDragStart;
        view.OnItemDragEnd += HandleItemDragEnd;
        view.OnItemDrag += HandleItemDrag;
        view.OnScroll += HandleScroll;
    }

    private void HandleScroll(float horizontalAmount, float verticalAmount)
    {
        _view.ScrollScene(horizontalAmount, verticalAmount);
    }

    private void HandleItemDragStart(string id, Vector3 position)
    {
        var item = _model.GetItem(id);
        if (item != null)
        {
            item.IsGrabbed = true;
            _view.SetItemGrabbed(id, true);

            if (item.IsOnShelf)
            {
                _model.SetItemOnShelf(id, false);
                _view.SetItemOnShelf(id, false);
            }
        }
    }

    private void HandleItemDragEnd(string id, Vector3 position)
    {
        var item = _model.GetItem(id);
        if (item != null)
        {
            item.IsGrabbed = false;

            var shelf = _model.GetShelfAt(new Vector2(position.x, position.y));
            if (shelf != null)
            {
                Vector3 shelfPosition = position;
                shelfPosition.z = shelf.Depth;
                _model.UpdateItemPosition(id, shelfPosition);
                _model.SetItemOnShelf(id, true);

                _view.UpdateItemPosition(id, shelfPosition);
                _view.UpdateItemDepth(id, shelf.Depth);
                _view.SetItemOnShelf(id, true);
            }
            else
            {
                _model.UpdateItemPosition(id, position);
                _model.SetItemOnShelf(id, false);

                _view.UpdateItemPosition(id, position);
                _view.SetItemOnShelf(id, false);
            }

            _view.SetItemGrabbed(id, false);
        }
    }

    private void HandleItemDrag(string id, Vector3 position, float depth)
    {
        var item = _model.GetItem(id);
        if (item != null && item.IsGrabbed)
        {
            _model.UpdateItemPosition(id, position);
            _view.UpdateItemPosition(id, position);
            _view.UpdateItemDepth(id, depth);
        }
    }
}