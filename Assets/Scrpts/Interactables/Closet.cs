using System;
using UnityEngine;

namespace bananplayss {
	public class Closet : MonoBehaviour, IInteractable {
		

		[SerializeField] private PlayerInventory inventory;
		public event Action OnDoorFell;
		public event Action OnOpenedCloset;

		private Animator animator;
		private const string OPEN_CLOSET = "OpenCloset";
		private const string OPEN_LOCK = "OpenLock";

		private bool openedLock = false;
		private bool openedCloset = false;

		private void Start() {
			animator = GetComponent<Animator>();
		}

		public void Interact() {
			if (inventory.HasKey() && !openedLock) {
				animator.Play(OPEN_LOCK);
				openedLock = true;
			}else if (openedLock && !openedCloset) {
				OnOpenedCloset?.Invoke();
				openedCloset = true;
				inventory.UnequipItem();
				animator.Play(OPEN_CLOSET);
				enabled = false;
			} else if(!openedLock && !openedCloset){
				DisplayMessageUI.Instance.DisplayMessage("It needs a key.","Kulcs kell");
			}
		}

		public bool UsesIndicator() {
			return false;
		}
	}

}
