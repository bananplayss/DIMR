using UnityEngine;

namespace bananplayss {
	public class WashingMachine : MonoBehaviour, IInteractable {

		private Animator animator;
		private const string START_WASHING_MACHINE = "StartWashingMachine";
		private bool interacted = false;
		[SerializeField] private AudioSource washineMachineSFX;
		[SerializeField] private PlayerSleep playerSleep;

		private void Start() {
			playerSleep.OnBlackScreen += PlayerSleep_OnBlackScreen;
			animator = GetComponent<Animator>();
		}

		private void PlayerSleep_OnBlackScreen() {
			PlayIdle();
		}

		public void PlayIdle() {
			washineMachineSFX.Stop();
			animator.CrossFade("Idle", .5f);
		}

		public void Interact() {
			if (interacted) return;
			PlayerStats.Interact();
			washineMachineSFX.Play();
			interacted = true;
			animator.Play(START_WASHING_MACHINE);
		}

		public bool UsesIndicator() {
			return false;
		}
	}

}
