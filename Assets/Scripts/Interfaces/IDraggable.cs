
public interface IDraggable
{
    public bool IsDraggable { get; set; }
    public void OnDrop();
    public void OnDrag();
    
}
