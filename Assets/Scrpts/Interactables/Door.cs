
using UnityEngine;

namespace bananplayss {
	public class Door : MonoBehaviour, IInteractable {

		private Animator animator;
		[SerializeField] private AudioSource doorOpenSFX;
		[SerializeField] private AudioSource doorCloseSFX;

		private void Start() {
			animator = GetComponent<Animator>();
		}
		public void Interact() {
			if (PlayerStats.endingsUnlocked >= 3) {
				PlayerStats.Interact();
				animator.Play("DoorOpen");
				doorOpenSFX.Play();
				GetComponent<Collider>().enabled = false;
			} else {
				DisplayMessageUI.Instance.DisplayMessage("Unlock three endings in this room out of four first","Legalabb harom befejezes feloldasa szukseges ehhez");
			}
		}

		public void Close() {
			animator.Play("DoorClose");
			doorCloseSFX.Play();
		}
		public bool UsesIndicator() {
			return false;
		}
	}

}
