using UnityEngine;

namespace FedoraDev.Interaction
{
    public interface IInteractable
    {
        float Priority { get; }
        GameObject GameObject { get; }
        IInteractResult Interact(IInteractor interactor);
    }
}
