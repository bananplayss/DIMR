using System;
using UnityEngine;

namespace bananplayss {
	public class WashBasin : MonoBehaviour, IInteractable {
		public event Action OnOpenedWashBasinDoor;

		private bool opened = false;
		private const string WASHBASIN_DOOR_OPEN = "Washbasin_Door_Open";
		private const string WASHBASIN_DOOR_CLOSE = "Washbasin_Door_Close";

		private Animator animator;

		private void Start() {
			animator = GetComponent<Animator>();
		}

		public void Interact() {
			PlayerStats.Interact();
			GetComponent<Collider>().enabled = false;
			OnOpenedWashBasinDoor?.Invoke();
			animator.Play(WASHBASIN_DOOR_OPEN);
		}

		public bool UsesIndicator() {
			return false;
		}

	}

}
