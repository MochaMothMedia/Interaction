using UnityEngine;

namespace FedoraDev.Interaction.Implementations
{
	public class EchoInteractable : IInteractable
	{
		public float Priority => _priority;
		public GameObject GameObject => null;

		[SerializeField] float _priority = 1f;

		public IInteractResult Interact(IInteractor interactor)
		{
			SimpleResult result = new SimpleResult();

			Debug.Log($"Interacted with by {interactor.GameObject.name}");

			result.IsGoodResult = true;
			return result;
		}
	}
}
