
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace bananplayss {
	public class PlayerSleep : MonoBehaviour {
		public event Action OnBlackScreen;
		public event Action OnGotSleepEnding;

		private Animator anim;

		private bool isSleeping = false;
		private float sleepTimer = 4f;
		private float timer;
		private bool adjustChromAb = false;
		private bool startedChromAb = false;

		private bool adjustVignette = false;
		private bool startedVignette = false;

		public event Action OnWakeup;
		public event Action OnSleep;

		CharacterController cController;

		[SerializeField] Volume volume;
		[SerializeField] DemonJumpscare dj;
		private float originalChromAbIntensity;
		private float originalVignetteIntensity;
		private ChromaticAberration chromAb;
		private LensDistortion lensDist;
		private Vignette vignette;

		private int sleepCount = 0;
		private int sleepCountRequiredForHint = 2;

		private void Awake() {
			anim = GetComponent<Animator>();
			cController = GetComponent<CharacterController>();
		}


		public void Lense() {
			LeanTween.value(gameObject, lensDist.intensity.value, .7f, 11f).setOnUpdate((float val) => { lensDist.intensity.value = val; }).setOnComplete(CompleteCutscene);

		}

		private void CompleteCutscene() {
			EndGameScreenUI.Instance.ShowEndGameScreen(EndGameManager.EndGameCase.TelevisionEnding);
		}

		public void Vignette() {
			adjustVignette = true;

		}

		private void Start() {
			volume.profile.TryGet(out chromAb);
			volume.profile.TryGet(out lensDist);
			volume.profile.TryGet(out vignette);
			originalChromAbIntensity = chromAb.intensity.value;
			originalVignetteIntensity = vignette.intensity.value;
			CinematicManager.Instance.OnStartTelevisionEndingCutscene += CinematicManager_OnStartTelevisionEndingCutscene;
			dj.OnDemonBlackScreen += Dj_OnBlackScreen;
		}

		private void Dj_OnBlackScreen() {
			BlackScreen();
			Invoke(nameof(DelayEndGameScreen), 4f);
		}

		private void CinematicManager_OnStartTelevisionEndingCutscene() {
			Lense();
		}

		public bool IsSleeping() {
			return isSleeping;
		}

		public Animator GetAnim() {
			return anim;
		}

		public void WokeUp() {
			PlayerStats.SleptDay();
			anim.applyRootMotion = true;
			anim.enabled = false;
			cController.enabled = true;
			sleepCount++;
			if (sleepCount == sleepCountRequiredForHint || sleepCount == sleepCountRequiredForHint+1) {
				DisplayMessageUI.Instance.DisplayMessage("I feel really numb \nI should go back to sleep","Rosszul erzem magam\nVissza kene aludjak");
			}
		}

		public void Sleep() {
			cController.enabled = false;
			isSleeping = true;

			bool gotBedEnding = ActivityIndicatorManager.Instance.HasAllIndicators();
			if (gotBedEnding) {
				OnGotSleepEnding?.Invoke();
				CinematicManager.Instance.StartSleepEndingCutscene();
				EndGameManager.Instance.CheckForEndGame(EndGameManager.EndGameCase.SleepEnding);
			}

			OnSleep?.Invoke();
		}

		private void AdjustChromaticAberration(float desiredValue) {
			if (!startedChromAb) {
				chromAb.intensity.value = desiredValue;
				startedChromAb = true;
			}

			chromAb.intensity.value = Mathf.MoveTowards(chromAb.intensity.value, originalChromAbIntensity, Time.deltaTime / 4f);
			if (chromAb.intensity.value == originalChromAbIntensity) {
				adjustChromAb = false;
				startedChromAb = false;
			}
		}

		private void AdjustVignette(float desiredValue) {
			if (!startedVignette) {
				vignette.intensity.value = desiredValue;
				startedVignette = true;
			}

			vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, desiredValue, Time.deltaTime / 3f);
			if (vignette.intensity.value == originalVignetteIntensity) {
				adjustVignette = false;
				startedVignette = false;
			}
		}

		public void BlackScreen() {
			OnBlackScreen?.Invoke();
		}

		private void Update() {
			if (isSleeping) {
				timer += Time.deltaTime;
				if (timer >= sleepTimer) {
					OnWakeup?.Invoke();
					isSleeping = false;
					adjustChromAb = true;
					timer = 0;
				}
			}

			if (adjustChromAb) {
				AdjustChromaticAberration(1);
			}
			if (adjustVignette) {
				AdjustVignette(.7f);
			}
		}

		private void DelayEndGameScreen() {
			EndGameScreenUI.Instance.ShowEndGameScreen(EndGameManager.EndGameCase.DemonEnding);
		}
	}

}
