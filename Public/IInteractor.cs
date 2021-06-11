using UnityEngine;

namespace FedoraDev.Interaction
{
    public interface IInteractor
    {
        IInteractable Interactable { get; }
        GameObject GameObject { get; }
    }
}
