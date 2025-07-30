using System;
using UnityEngine;

namespace bananplayss {
	public class HairDryer : MonoBehaviour, IInteractable {

		[SerializeField] private PlayerInventory playerInventory;
		[SerializeField] private Material transparentMaterial;
		[SerializeField] private WashBasin washBasin;
		[SerializeField] private WaterBlock waterBlock;

		private Material originalMat;
		private MeshRenderer meshRenderer;
		private bool isPickedUp = false;

		private void Start() {
			meshRenderer = GetComponent<MeshRenderer>();
			originalMat = meshRenderer.sharedMaterial;
			washBasin.OnOpenedWashBasinDoor += WashBasin_OnOpenedWashBasinDoor;
			waterBlock.OnTriggerHead += WaterBlock_OnTriggerHead;

		}

		private void WaterBlock_OnTriggerHead() {
			isPickedUp = false;
			playerInventory.UnequipItem();
		}

		private void WashBasin_OnOpenedWashBasinDoor() {
			GetComponent<Collider>().enabled = true;
		}

		public void Interact() {
			PlayerStats.Interact();
			isPickedUp = !isPickedUp;
			if (isPickedUp) {
				if (playerInventory.CanBePickedUp()) {
					meshRenderer.sharedMaterial = transparentMaterial;
					playerInventory.EquipItem(PlayerInventory.ItemEnum.HairDryer);
					
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
