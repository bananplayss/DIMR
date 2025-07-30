using UnityEngine;

namespace bananplayss {
	public class Nightstand : MonoBehaviour, IInteractable {
		private Animator animator;
		private bool opened = false;

		[SerializeField] private AudioSource cabinetOpenSFX;
		[SerializeField] private AudioSource cabinetCloseSFX;
		[SerializeField] private Collider paperCollider;

		private const string NIGHSTAND_OPEN = "NightStand_Door_Open", NIGHTSTAND_CLOSE = "NightStand_Door_Close";

		private void Start() {
			animator = GetComponent<Animator>();
		}

		public void Interact() {
			PlayerStats.Interact();
			opened = !opened;
			string animationClipName = opened == true ? NIGHSTAND_OPEN : NIGHTSTAND_CLOSE;
			AudioSource sfx = opened == true ? cabinetOpenSFX : cabinetCloseSFX;
			paperCollider.enabled = opened;
			sfx.Play();
			animator.Play(animationClipName);
		}

		public bool UsesIndicator() {
			return !opened;
		}
	}

}
