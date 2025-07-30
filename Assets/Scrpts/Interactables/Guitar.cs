
using UnityEngine;

namespace bananplayss {
	public class Guitar : MonoBehaviour, IInteractable {
		[SerializeField] private PlayerInventory playerInventory;
		[SerializeField] private Material transparentMaterial;

		private Material originalMat;
		private MeshRenderer meshRenderer;
		private bool isPickedUp = false;

		private void Start() {
			meshRenderer = GetComponent<MeshRenderer>();
			originalMat = meshRenderer.sharedMaterial;
		}


		public void Interact() {
			PlayerStats.Interact();
			isPickedUp = !isPickedUp;
			if (isPickedUp) {
				if (playerInventory.CanBePickedUp()) {
					meshRenderer.sharedMaterial = transparentMaterial;
					playerInventory.EquipItem(PlayerInventory.ItemEnum.Guitar);
				}

			} else {
				meshRenderer.sharedMaterial = originalMat;
				playerInventory.UnequipItem();
			}

		}

		public bool UsesIndicator() {
			return false;
		}
	}
}


