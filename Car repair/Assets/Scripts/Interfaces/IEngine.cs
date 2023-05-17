using UnityEngine;

public interface IEngine
{
    public void Hover(RaycastHit hit);
    public void Interact();
    public void CheckPart();

    public void OnEnable();
    public void OnDisable();

}
