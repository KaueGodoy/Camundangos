public interface IInteractable
{
    public void InitiateInteraction();
    public void DisableInteraction();
    public void EnableInteraction();
    public string Name { get; set; }
    public string SetName(string name);
    public bool CanInteract { get; set; }
}
