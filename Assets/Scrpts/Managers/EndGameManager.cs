using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace bananplayss {
	public class EndGameManager : MonoBehaviour {

		public static EndGameManager Instance { get; private set; }

		public event Action OnStartTelevisionEnding;
		public event Action OnSwitchShow;
		public event Action OnEndGame;
		public event Action OnShookTimesCompleted;
		public event Action OnMusicPlayedCompleted;


		public int tvInteractCounter = 0, tvInteractCounterNeeded = 8;
		[Header("TelevisionEnding")]
		[SerializeField] private Lamp lamp;
		public bool isTvOn = true;

		[Header("CouchEnding")]
		[SerializeField] private Painting painting;
		private int shookTimesNeeded = 2;

		[Header("NotebookEnding")]
		[SerializeField] private AudioPlayer audioPlayer;
		[SerializeField] private InventoryItem guitarItem;
		private int numTimesPlayedRequired = 5;

		[Header("WaterEnding")]
		[SerializeField] private Mixer mixer;
		private bool gameEnded = false;
		public bool isBait = false;

		[SerializeField] private string[] endGameNames;

		public enum EndGameCase {
			TelevisionEnding,
			CouchEnding,
			NotebookEnding,
			SleepEnding,
			SuffocateEnding,
			DemonEnding,
			LadderEnding,
		}

		private void Awake() {
			Instance = this;
		}

		public string ReturnEndGameName(int index) {
			return endGameNames[index];
		}
		private void Start() {
			ActivityIndicatorUI.Instance.OnRefillIndicators += ActivityIndicatorUI_OnRefillIndicators;
			EndGameScreenUI.Instance.OnGameEnded += EndGameScreenUI_OnGameEnded;
			CinematicManager.Instance.OnStartSleepEndingCutscene += CinematicManager_OnStartSleepEndingCutscene;
		}


		private void CinematicManager_OnStartSleepEndingCutscene() {
			isBait = true;
		}

		private void EndGameScreenUI_OnGameEnded() {
			gameEnded = true;
		}

		private void ActivityIndicatorUI_OnRefillIndicators() {
			ResetTvInteractCounter();
		}

		public void ResetTvInteractCounter() {
			tvInteractCounter = 0;
		}

		private void Update() {

			if (gameEnded && Input.GetKeyDown(Interact.restartKey) && !isBait) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}

		public void CheckForEndGame(EndGameCase endGameCase) {
			switch (endGameCase) {
				case EndGameCase.TelevisionEnding:
					if (tvInteractCounter >= tvInteractCounterNeeded && isTvOn) {
						StartTelevisionEnding();
					}else if(tvInteractCounter>= tvInteractCounterNeeded / 2 && isTvOn) {
						OnSwitchShow?.Invoke();
					}
					break;
				case EndGameCase.CouchEnding:
					if (painting.GetShookTimes() >= shookTimesNeeded) {
						OnShookTimesCompleted?.Invoke();
					}
					break;
				case EndGameCase.NotebookEnding:
					if (audioPlayer.IsPlaying()) {
						if (guitarItem.GetNumTimesPlayed() >= numTimesPlayedRequired) {
							OnMusicPlayedCompleted?.Invoke();
						}
					}
					break;

			}
		}

		public void StartTelevisionEnding() {
			OnStartTelevisionEnding?.Invoke();
			CinematicManager.Instance.StartTelevisionEndingCutscene();
		}

		public void EndGame() {
			OnEndGame?.Invoke();
		}
	}

}
