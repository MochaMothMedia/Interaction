using Sirenix.OdinInspector;
using UnityEngine;

namespace MochaMoth.Interaction.Implementations
{
	[HideMonoScript]
	public class InteractableBehaviour : SerializedMonoBehaviour, IInteractable
	{
		public float Priority => _interactable.Priority;

		[SerializeField] IInteractable _interactable;

		public IInteractResult Interact(IInteractor interactor) => _interactable.Interact(interactor);
	}
}
