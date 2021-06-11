using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace FedoraDev.Interaction.Implementations
{
	[RequireComponent(typeof(SphereCollider))]
	[RequireComponent(typeof(Rigidbody))]
	[HideMonoScript]
	public class InteractorBehaviour : MonoBehaviour, IInteractor
	{
		[ShowInInspector, ReadOnly, HideLabel, BoxGroup("Interactable")] public IInteractable Interactable { get; private set; }
		public GameObject GameObject => gameObject;

		[SerializeField] float _radius = 5f;
		[SerializeField, Range(0, 1)] float _distanceWeight = 0.33f;
		[SerializeField, Range(0, 1)] float _facingWeight = 0.33f;
		[SerializeField, Range(0, 1)] float _priorityWeight = 0.33f;
		[SerializeField, Range(0, 1)] float _facingRange = 0.5f;
		[ShowInInspector, ReadOnly, HideLabel, BoxGroup("Interactables")] List<IInteractable> _interactables = new List<IInteractable>();

		float _distanceMultiplierBehind = 0.33f;
		float _facingMultiplierBehind = 0.33f;
		float _priorityMultiplierBehind = 0.33f;

		private void Update()
		{
			PruneInteractables();
			SetInteractable();
		}

		private void OnTriggerEnter(Collider other)
		{
			IInteractable interactable = other.GetComponent<IInteractable>();
			if (interactable != null && !_interactables.Contains(interactable))
				_interactables.Add(interactable);
		}

		private void OnTriggerExit(Collider other)
		{
			IInteractable interactable = other.GetComponent<IInteractable>();
			if (interactable != null && _interactables.Contains(interactable))
				_interactables.Remove(interactable);
		}

		private void OnValidate()
		{
			float total = 1f - (_distanceWeight + _facingWeight + _priorityWeight);
			if (total != 1f)
			{
				total /= 2f;

				if (_distanceWeight != _distanceMultiplierBehind)
				{
					_facingWeight += total;
					_priorityWeight += total;
				}
				else if (_facingWeight != _facingMultiplierBehind)
				{
					_distanceWeight += total;
					_priorityWeight += total;
				}
				else if (_priorityWeight != _priorityMultiplierBehind)
				{
					_distanceWeight += total;
					_facingWeight += total;
				}
			}

			_distanceMultiplierBehind = _distanceWeight;
			_facingMultiplierBehind = _facingWeight;
			_priorityMultiplierBehind = _priorityWeight;

			GetComponent<SphereCollider>().radius = _radius;
		}

		[Button("Reset Weights")]
		void ResetWeights()
		{
			_distanceWeight = 0.33f;
			_facingWeight = 0.33f;
			_priorityWeight = 0.33f;
		}

		[Button("Interact")]
		public void Interact() => Interactable?.Interact(this);

		public void PruneInteractables()
		{
			for (int i = 0; i < _interactables.Count; i++)
			{
				if (_interactables[i].GameObject == null)
					_interactables.RemoveAt(i--);
			}
		}

		public void SetInteractable()
		{
			IInteractable interactable = null;
			float highestValue = float.MinValue;

			for (int i = 0; i < _interactables.Count; i++)
			{
				float facingValue = Vector3.Dot(transform.forward, Vector3.Normalize(_interactables[i].GameObject.transform.position - transform.position));
				if (facingValue < _facingRange)
					continue;

				float distanceValue = Mathf.Max(1f, Vector3.Distance(transform.position, _interactables[i].GameObject.transform.position) / _radius);
				float finalValue = (facingValue * _facingWeight) + (distanceValue * _distanceWeight) + (_interactables[i].Priority * _priorityWeight);

				if (interactable == null || finalValue > highestValue)
				{
					interactable = _interactables[i];
					highestValue = finalValue;
				}
			}

			Interactable = interactable;
		}
	}
}
