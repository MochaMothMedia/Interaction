using UnityEngine;

namespace MochaMoth.Interaction
{
    public interface IInteractor
    {
        IInteractable Interactable { get; }
        GameObject GameObject { get; }
    }
}
