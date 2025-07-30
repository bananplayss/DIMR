
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace bananplayss {
	public class SleepUI : MonoBehaviour {
		public event Action OnBait;
		public event Action OnFadeOut;

		[SerializeField] private CanvasGroup sleepImage;
		[SerializeField] private PlayerSleep playerSleep;
		[SerializeField] private CanvasGroup demonImage;
		[SerializeField] private CanvasGroup demonImageBig;

		private bool gotSleepEnding = false;

		private bool firstPart = false;


		private void Start() {
			CinematicManager.Instance.OnStartSleepCutscene += CinematicManager_OnStartSleepCutscene;
			playerSleep.OnWakeup += PlayerSleep_OnWakeup;
			playerSleep.OnGotSleepEnding += PlayerSleep_OnGotSleepEnding;
			playerSleep.OnBlackScreen += PlayerSleep_OnBlackScreen;
			CinematicManager.Instance.OnStartSleepEndingCutscene += CinematicManager_OnStartSleepEndingCutscene;
			EndGameScreenUI.Instance.OnJumpscare += Instance_OnJumpscare;

			EndGameManager.Instance.OnEndGame += EndGameManager_OnEndGame;
		}

		public void BlackScreen() {
			sleepImage.alpha = 1f;
		}

		private void PlayerSleep_OnBlackScreen() {
			sleepImage.alpha = 1f;
			Invoke(nameof(DelayFadeOut), 7f);

		}

		public void StartScene() {
			sleepImage.alpha = 1f;
			WakeUp();
		}

		private void DelayFadeOut() {
			LeanTween.alphaCanvas(sleepImage, 0, 2.7f);
			OnFadeOut?.Invoke();
			AudioManager.Instance.PlayClip(Sound.CreepyAmbient);
		}

		private void Instance_OnJumpscare() {
			LeanTween.alphaCanvas(demonImageBig, 1, .07f).setEaseLinear().setLoopPingPong();
			Invoke(nameof(DelayEndGameScreenOverrid), 4f);
			Invoke(nameof(DelayIsBaitEnd), 8f);
		}

		private void CinematicManager_OnStartSleepEndingCutscene() {
			Invoke(nameof(DelayShowingDemon), 8f);
		}

		private void DelayShowingDemon() {
			LeanTween.alphaCanvas(demonImage, 1, 1.1f).setLoopPingPong().setLoopCount(3);
			AudioManager.Instance.PlayClip(Sound.CreepyAmbient);
			Invoke(nameof(DelayEndGameScreenOverrid), 4f);
		}

		private void DelayIsBaitEnd() {
			EndGameManager.Instance.isBait = false;
		}

		private void PlayerSleep_OnGotSleepEnding() {
			gotSleepEnding = true;
		}

		private void EndGameManager_OnEndGame() {
			sleepImage.alpha = 1.0f;
			Invoke(nameof(DelayEndGameScreen), 3f);
		}

		private void PlayerSleep_OnWakeup() {
			WakeUp();
		}

		private void CinematicManager_OnStartSleepCutscene() {
			float animationLength = 12f;
			LeanTween.alphaCanvas(sleepImage, 1, 3f).setDelay(animationLength).setOnStart(HandleRoomAudio);
		}

		private void HandleRoomAudio() {
			RoomAudioManager.Instance.StartFadingSources(4);
		}

		public void WakeUp() {
			if (gotSleepEnding) return;
			LeanTween.alphaCanvas(sleepImage, 0, 2.5f);
		}

		private void DelayEndGameScreen() {
			if (gotSleepEnding) return;
			EndGameScreenUI.Instance.ShowEndGameScreen(EndGameManager.EndGameCase.CouchEnding);

		}

		private void DelayEndGameScreenOverrid() {
			demonImageBig.alpha = 0f;
			demonImage.alpha = 0f;
			EndGameScreenUI.Instance.ShowEndGameScreen(EndGameManager.EndGameCase.SleepEnding);
		}

		private void Update() {
			if (gotSleepEnding) {
				if (demonImage.alpha >= .2f && !firstPart) {
					LeanTween.alphaCanvas(demonImage, 0, .7f);
					Invoke(nameof(DelayBaiting), 7f);
					firstPart = true;
				}
			}
		}

		private void DelayBaiting() {
			OnBait?.Invoke();
		}
	}

}
