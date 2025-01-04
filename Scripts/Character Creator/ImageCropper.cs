using Godot;
using System;
using Godot.Collections;

public partial class ImageCropper : Node
{
    [Export] private Sprite2D spriteToEdit;
    [Export] private Sprite2D secondSprite;
    [Export] private TextureRect cropBox;
    [Export] private TextureRect cropCover;
    [Export] private float minCropSize = 10f;
    [Export] private Camera2D cam;

    private bool textureNoLongerNull = false;
    private bool cropped = false;
    private bool updatedCropPosition = false;

    public Vector2 cropStart { get; set; }
    public Vector2 cropEnd { get; set; }

    public Dictionary<int, Vector2> touches { get; private set; }


    public override void _Ready()
    {
        touches = new Dictionary<int, Vector2>();
        cropCover.Visible = false;
    }


    public override void _Process(double delta)
    {
        UpdateTextureStatus();

    }

    private void UpdateTextureStatus()
    {
        var texture = spriteToEdit.Texture;
        
        if(texture == null && textureNoLongerNull)
        {
            textureNoLongerNull = false;
            //hide ui
        }else if(texture != null && !textureNoLongerNull)
        {
            textureNoLongerNull = true;
            //reveal UI
        }
    }

    /***************
     * Touch Event
     * *************/

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventScreenTouch)
        {
            var touchEvent = (InputEventScreenTouch)@event;
            UpdateTouchesOnScreen(touchEvent);
        }

        if(@event is InputEventScreenDrag)
        {
            var dragEvent = (InputEventScreenDrag)@event;
            UpdateTouchPositions(dragEvent);
        }
    }

    private void UpdateTouchesOnScreen(InputEventScreenTouch touchEvent)
    {
        bool pressed = touchEvent.Pressed;
        int index = touchEvent.Index;
        var position = touchEvent.Position;
        
        if (pressed)
        {
            touches[index] = position;
        }
        else
        {
            touches.Remove(index);
        }
    }

    private void UpdateTouchPositions(InputEventScreenDrag dragEvent)
    {
        int index = dragEvent.Index;
        var position = dragEvent.Position;

        if (touches.ContainsKey(index))
        {
            touches[index] = position;
        }
    }

    /**************
     * Crop Box
     * ************/

    public void ActivateCropBox()
    {
        cropCover.Visible = true;
    }

    public void SetCropAreaEnd(Vector2 endPos)
    {
        var newPos = PositionToCameraPosition(endPos);
        var tempSize = (newPos - cropStart).X;
        tempSize = MathF.Max(minCropSize, tempSize);
        var newSize = new Vector2(tempSize, tempSize);
        cropBox.Size = newSize;
        cropEnd = cropStart + newSize;
    }

    public void SetCropAreaStart(Vector2 startPos)
    {
        var newPos = PositionToCameraPosition(startPos);
        cropStart = newPos;
        cropBox.GlobalPosition = newPos;
        MoveSecondSpriteToSprite();
    }

    public void MoveCropArea(Vector2 moveVector)
    {
        cropStart += moveVector;
        cropEnd += moveVector;
        cropBox.GlobalPosition = cropStart;
        MoveSecondSpriteToSprite();
    }

    public void MoveCropAreaToPosition(Vector2 newPosition)
    {
        var newPos = PositionToCameraPosition(newPosition);
        var size = cropEnd - cropStart;
        cropStart = newPos - size/2;
        cropEnd = newPos + size/2;
        cropBox.GlobalPosition = cropStart;
        cropBox.Position = cropBox.Position;
        MoveSecondSpriteToSprite();
    }

    public Vector2 PositionToCameraPosition(Vector2 position)
    {
        var camTransform = cam.GetViewport().CanvasTransform;
        var inverse = camTransform.AffineInverse();
        return inverse * position;
    }

    private void MoveSecondSpriteToSprite()
    {
        secondSprite.GlobalPosition = spriteToEdit.GlobalPosition;
    }

    /*******************
     * Crop Image
     * ****************/

    private void InitialCrop(InputEventScreenTouch mouseEvent)
    {
        /*bool mousePressed = mouseEvent.IsActionPressed("click");

        if (mousePressed)
        {
            if (mouseClicked) //draggin mouse
            {
                OnDrag(mouseEvent);
            }
            else // just clicked
            {
                OnClick(mouseEvent);
                mouseClicked = true;
            }
        }
        else
        {
            mouseClicked = false;
            cropped = true;
        }*/
    }

    private void DragCorner(InputEventScreenTouch mouseEvent)
    {

    }

    private void OnClick(InputEventScreenTouch mouseEvent)
    {
        var position = mouseEvent.Position;
        CropStart(position);
    }

    private void OnDrag(InputEventScreenTouch mouseEvent)
    {
        var mousePos = mouseEvent.Position;
        DragCropSize(mousePos);
    }

    private void CropStart(Vector2 startPosition)
    {
        cropStart = startPosition;
        cropBox.GlobalPosition = startPosition;
    }

    private void DragCropSize(Vector2 endPosition)
    {
        /*float x = endPosition.X;
        float y = endPosition.Y;

        float startX = cropStart.X;
        float startY = cropStart.Y;

        float width = x - startX;
        float height = y - startY;

        float minSize = Mathf.Min(width, height);
        if(minSize > minCropSize)
        {
            cropSize = new Vector2(minSize, minSize);
        }
        else
        {
            cropSize = new Vector2(minCropSize, minCropSize);
        }

        cropBox.Size = cropSize;*/
    }
}
