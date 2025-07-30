
using UnityEngine;

namespace bananplayss {
	public class Lamp : MonoBehaviour, IInteractable {
		[SerializeField] private Light lampLight;
		[SerializeField] private AudioSource lampSwitchSFX;

		private bool lampOn = true;

		public void Interact() {
			lampSwitchSFX.Play();
			PlayerStats.Interact();
			lampOn = !lampOn;
			lampLight.enabled = lampOn;
		}

		public bool UsesIndicator() {
			return true;
		}

		public bool IsLampOn() {
			return lampOn;
		}
	}

}
