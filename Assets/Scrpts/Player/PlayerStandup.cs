using System;
using UnityEngine;

namespace bananplayss {
	public class PlayerStandup : MonoBehaviour {
		public event Action OnCanStandUp;
		public event Action OnCantStandUp;

		[SerializeField] private PlayerInventory playerInventory;
		[SerializeField] private Plant plant;

		private bool canStandUp = false;
		private bool canPlayCinematic = false;

		private void Start() {
			CinematicManager.Instance.OnStartSitCutscene += CinematicManager_OnStartSitCutscene;
			EndGameManager.Instance.OnShookTimesCompleted += EndGameManager_OnShookTimesCompleted;
			plant.OnTriggerPlant += Plant_OnTriggerPlant;
		}

		private void Plant_OnTriggerPlant() {
			canPlayCinematic = true;
		}

		private void EndGameManager_OnShookTimesCompleted() {
			//canPlayCinematic = true;
		}

		private void CinematicManager_OnStartSitCutscene() {
			GetComponent<CharacterController>().enabled = false;
			Invoke(nameof(DelayCanStandup), 8f);
			if (canPlayCinematic && playerInventory.IsGuitarInHand()) {
				Invoke(nameof(DelayPlayCouchCinematic), 6.4f);
			}
		}

		private void DelayCanStandup() {
			canStandUp = true;
		}

		public void DelayPlayCouchCinematic() {
			Invoke(nameof(PlayCouchCinematic), 3f);
		}

		private void PlayCouchCinematic() {
			CinematicManager.Instance.StartCouchEndingCinematic();
		}

		private void Update() {
			if (canPlayCinematic) {
				canStandUp = false;
			}
			if (canStandUp) {
				if (Input.GetKeyDown(Interact.standupKey)) {
					canStandUp = false;
					CinematicManager.Instance.StartStandupCutscenePublic();
				}
				OnCanStandUp?.Invoke();
			} else {
				OnCantStandUp?.Invoke();
			}
		}
	}

}
