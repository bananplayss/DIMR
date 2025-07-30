
using UnityEngine;

namespace bananplayss {
	public class Notebook : MonoBehaviour, IInteractable {

		[SerializeField] private PlayerInventory playerInventory;
		[SerializeField] private Closet closet;
		[SerializeField] private Material transparentMaterial;

		private Material originalMat;
		private MeshRenderer meshRenderer;
		private bool isPickedUp = false;


		private void Start() {
			GetComponent<Collider>().enabled = false;
			meshRenderer = GetComponent<MeshRenderer>();
			originalMat = meshRenderer.sharedMaterial;
			closet.OnOpenedCloset += Closet_OnOpenedCloset;
		}

		private void Closet_OnOpenedCloset() {
			GetComponent<Collider>().enabled = true;
		}

		public void Interact() {
			PlayerStats.Interact();
			isPickedUp = !isPickedUp;
			if (isPickedUp) {
				if (playerInventory.CanBePickedUp()) {
					meshRenderer.sharedMaterial = transparentMaterial;
					playerInventory.EquipItem(PlayerInventory.ItemEnum.Book);
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
