
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplayss {
	public class PlayerInteract : MonoBehaviour {
		public event Action OnInteractHover;
		public event Action OnInteractUnhover;

		private float interactDistance = 2.5f;
		private float interactCooldown = 1f;
		private float timer;
		private bool onCooldown = false;
		private bool disableRaycasting = false;
		private bool isBusy = false;

		GameObject outlined;

		private PlayerNavigation playerNavigation;

		private bool isHairDryerOn;

		private List<IBlowable> blowables = new List<IBlowable>();
		[SerializeField] private InventoryItem hairDryer;

		private void Start() {
			CinematicManager.Instance.OnStartSleepCutscene += CinematicManager_OnStartSleepCutscene;
			CinematicManager.Instance.OnStartGameCutscene += Instance_OnStartGameCutscene;

			CinematicManager.Instance.OnStartCutscene += Instance_OnStartCutscene;
			CinematicManager.Instance.OnStartGame += Instance_OnStartGame;
			CinematicManager.Instance.OnStartStandupCutscene += Instance_OnStartStandupCutscene;
			playerNavigation = GetComponent<PlayerNavigation>();
			hairDryer.OnTurnOnHairDryer += HairDryer_OnTurnOnHairDryer;
			hairDryer.OnTurnOffHairDryer += HairDryer_OnTurnOffHairDryer;
		}

		private void Instance_OnStartStandupCutscene() {
			isBusy = false;
		}

		private void Instance_OnStartGame() {
			disableRaycasting = false;
		}

		private void Instance_OnStartGameCutscene() {
			disableRaycasting = true;
		}

		private void HairDryer_OnTurnOffHairDryer() {
			isHairDryerOn = false;
		}

		private void HairDryer_OnTurnOnHairDryer() {
			isHairDryerOn = true;
		}

		private void Instance_OnStartCutscene() {
			isBusy = true;
		}

		private void CinematicManager_OnStartSleepCutscene() {
			disableRaycasting = true;
		}

		public void EnableRaycasting() {
			disableRaycasting = false;
		}

		private void Update() {
			Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);

			if (outlined != null) outlined.layer = LayerMask.NameToLayer("Default");
			if (disableRaycasting || isBusy) OnInteractUnhover?.Invoke();
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactDistance) && !disableRaycasting && !isBusy) {
				if (isHairDryerOn) {
					if (hit.collider.TryGetComponent<IBlowable>(out IBlowable blowable)) {
						if (!blowables.Contains(blowable)) {
							blowables.Add(blowable);
						}
						blowable.Blow();
					} else {
						foreach (var _blowable in blowables) {
							_blowable.StopBlow();
						}
					}
				} else {
					if (hit.collider.TryGetComponent<IBlowable>(out IBlowable blowable)) {
						blowable.StopBlow();
					}
				}

				if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable)) {
					if (outlined != null) {
						if (outlined != null) outlined.layer = LayerMask.NameToLayer("Default");
					}
					outlined = hit.collider.gameObject;
					if (outlined != null) outlined.layer = LayerMask.NameToLayer("Outlined");

					if (Input.GetKeyDown(Interact.interactKey)) {
						if (!onCooldown) {
							if (!interactable.UsesIndicator()) interactable.Interact();
							else if (ActivityIndicatorManager.Instance.CanUseIndicator()) {
								interactable.Interact();
								ActivityIndicatorManager.Instance.UseIndicator();
							} else {
								DisplayMessageUI.Instance.DisplayMessage("Daily activity limit reached\n go to sleep.","Napi aktivitas limit elerve\nmenj aludni");
							}

						}
						onCooldown = true;
					}
					OnInteractHover?.Invoke();
				} else {
					
					OnInteractUnhover?.Invoke();
				}
			}

			if (onCooldown) {
				timer += Time.deltaTime;
				if (timer > interactCooldown) {
					onCooldown = false;
					timer = 0f;
				}
			}
		}
	}

}
