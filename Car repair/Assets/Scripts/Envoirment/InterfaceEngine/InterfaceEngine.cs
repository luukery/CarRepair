using UnityEngine;

public interface IEngine
{
    public void Start();
    public void Update();
    public void Hover(RaycastHit hit);
    public void Interact();
}
