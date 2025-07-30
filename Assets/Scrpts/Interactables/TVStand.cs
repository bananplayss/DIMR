
using UnityEngine;

namespace bananplayss {
	public class TVStand : MonoBehaviour, IInteractable {
		private Animator anim;
		private bool opened = false;

		private const string TVSTAND_DOOR_OPEN = "TVStand_Door_Open";
		private const string TVSTAND_DOOR_CLOSE = "TVStand_Door_Close";

		[SerializeField] private AudioSource cabinetOpenSFX;
		[SerializeField] private AudioSource cabinetCloseSFX;


		private void Start() {
			anim = GetComponent<Animator>();
		}

		public void Interact() {
			PlayerStats.Interact();
			opened = !opened;
			AudioSource sfx = opened == true ? cabinetOpenSFX : cabinetCloseSFX;
			sfx.Play();
			string animationString = opened == true ? TVSTAND_DOOR_OPEN : TVSTAND_DOOR_CLOSE;
			anim.Play(animationString);
		}

		public bool UsesIndicator() {
			return !opened;
		}
	}
}

