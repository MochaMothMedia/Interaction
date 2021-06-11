using UnityEngine;

namespace FedoraDev.Interaction
{
    public interface IInteractable
    {
        float Priority { get; }
        IInteractResult Interact(IInteractor interactor);
    }
}
