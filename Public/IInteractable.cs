namespace MochaMoth.Interaction
{
    public interface IInteractable
    {
        float Priority { get; }
        IInteractResult Interact(IInteractor interactor);
    }
}
