using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace bananplayss {
	public class EndGameScreenUI : MonoBehaviour {
		public event Action OnGameEnded;
		public event Action OnJumpscare;

		public static EndGameScreenUI Instance { get; private set; }

		[SerializeField] private CanvasGroup endGameScreen;
		[SerializeField] private CanvasGroup thxForPlaying;
		[SerializeField] private SleepUI sleepUI;
		[SerializeField] private TextMeshProUGUI endingName;

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			sleepUI.OnBait += SleepUI_OnBait;
			CinematicManager.Instance.OnStartClimbingCutscene += CinematicManager_OnStartClimbingCutscene;
		}

		private void CinematicManager_OnStartClimbingCutscene(int obj) {
			Invoke(nameof(DelayShowingEndGameScreenLadderEnding), obj);
		}

		private void SleepUI_OnBait() {
			Invoke(nameof(DelayShowingEndGameScreenSleepEnding), 2f);
			Invoke(nameof(DelayJumpscare), 5f);
		}

		private void DelayShowingEndGameScreenLadderEnding() {
			ShowEndGameScreen(EndGameManager.EndGameCase.LadderEnding);
		}

		private void DelayShowingEndGameScreenSleepEnding() {
			ShowEndGameScreen(EndGameManager.EndGameCase.SleepEnding, true);
		}

		public void ShowEndGameScreen(EndGameManager.EndGameCase endGameCase, bool isBait = false) {
			PlayerStats.CheckUnlockNewEnding(endGameCase);
			if(PlayerStats.endingsUnlocked < GameManager.allEndings) {
				LeanTween.alphaCanvas(endGameScreen, 1, 2f);
			} else {
				LeanTween.alphaCanvas(thxForPlaying, 1, 2f);
			}
			
			endingName.text = EndGameManager.Instance.ReturnEndGameName((int)endGameCase);
			if (!isBait) {
				OnGameEnded?.Invoke();
			}
		}

		public void HideEndGameScreen() {
			endGameScreen.alpha = 0f; 
			thxForPlaying.alpha = 0f;

		}

		private void DelayJumpscare() {
			OnJumpscare?.Invoke();
			AudioManager.Instance.PlayClip(Sound.Jumpscare,.25f);
			HideEndGameScreen();
		}
	}

}
