
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace bananplayss {
	public class CinematicManager : MonoBehaviour {
		public event Action OnStartGameCutscene;
		public event Action OnStartGame;
		public event Action OnStartCutscene;
		public event Action OnStartSleepCutscene;
		public event Action OnStartSitCutscene;
		public event Action OnStartStandupCutscene;
		public event Action OnStartTelevisionEndingCutscene;
		public event Action OnStartCouchEndingCutscene;
		public event Action OnStartNotebookEndingCutscene;
		public event Action OnStartSleepEndingCutscene;
		public event Action<int> OnStartClimbingCutscene;

		[SerializeField] private PlayerSleep playerSleep;
		[SerializeField] private PlayerNavigation playerNavigation;
		[SerializeField] private SleepUI sleepUI;
		[SerializeField] private Plant plant;
		private Animator playerAnim;

		private const string SLEEP_CUTSCENE = "SleepCutscene";
		private const string WAKEUP_CUTSCENE = "WakeupCutscene";
		private const string SIT_CUTSCENE = "SitCutscene";
		private const string STANDUP_CUTSCENE = "StandupCutscene";
		private const string TV_ENDING_CUTSCENE = "TvEndingCutscene";
		private const string NOTEBOOK_ENDING_CUTSCENE = "NotebookEndingCutscene";
		private const string SUFFOCATION_CUTSCENE = "SuffocationCutscene";
		private const string MIRROR_CUTSCENE = "MirrorCutscene";
		private const string CLIMBING_CUTSCENE = "ClimbingCutscene";

		[SerializeField] private PlayableDirector couchEndingCutscene;

		private bool canPlayCouchCinematic = false;

		public static CinematicManager Instance { get; private set; }

		private bool gotSleepEnding = false;

		private void Start() {
			EndGameManager.Instance.OnShookTimesCompleted += EndGameManager_OnShookTimesCompleted;
			playerAnim = playerSleep.GetAnim();
			playerSleep.OnWakeup += PlayerSleep_OnWakeup;
			playerSleep.OnGotSleepEnding += PlayerSleep_OnGotSleepEnding;
			playerNavigation.OnStartSleepCutscene += PlayerNavigation_OnStartSleepCutscene;
			plant.OnTriggerPlant += Plant_OnTriggerPlant;

			sleepUI.BlackScreen();
			Invoke(nameof(StartGameCinematic), .02f);

		}

		private void Plant_OnTriggerPlant() {
			canPlayCouchCinematic = true;
		}

		private void StartGameCinematic() {
			OnStartGameCutscene?.Invoke();
			sleepUI.StartScene();
			playerAnim.applyRootMotion = false;
			playerAnim.enabled = true;
			playerAnim.Play(WAKEUP_CUTSCENE);
			Invoke(nameof(DelayDisplayMessage), 9f);
		}

		private void PlayerSleep_OnGotSleepEnding() {
			gotSleepEnding = true;
		}

		private void DelayDisplayMessage() {
			DisplayMessageUI.Instance.DisplayMessage("The air is so dense all of a sudden\nWhat is happening","Fura a levego itt bent\nMi tortenik");
			OnStartGame?.Invoke();

		}

		private void EndGameManager_OnShookTimesCompleted() {
			canPlayCouchCinematic = true;
		}

		private void PlayerNavigation_OnStartSleepCutscene() {
			//StartSleepCutscene();
		}

		private void PlayerSleep_OnWakeup() {
			if (gotSleepEnding) return;
			playerAnim.Play(WAKEUP_CUTSCENE);
		}

		private void Awake() {
			Instance = this;
		}

		private void HandleAnimator(bool applyRootMotion, bool enableAnimator) {

		}

		public void StartSleepCutscenePublic() {
			OnStartSleepCutscene?.Invoke();
			StartSleepCutscene();
		}

		public void StartSitCutscenePublic() {
			OnStartCutscene?.Invoke();

			OnStartSitCutscene?.Invoke();
			StartSitCutscene();
		}

		public void StartStandupCutscenePublic() {
			OnStartCutscene?.Invoke();

			OnStartStandupCutscene?.Invoke();
			StartStandupCutscene();
		}

		private void StartStandupCutscene() {
			HandleAnimator(false, true);
			playerAnim.Play(STANDUP_CUTSCENE);
		}

		private void StartSitCutscene() {
			HandleAnimator(false, true);
			playerAnim.Play(SIT_CUTSCENE);
		}

		private void StartSleepCutscene() {
			HandleAnimator(false, true);
			playerAnim.Play(SLEEP_CUTSCENE);
		}

		public void StartTelevisionEndingCutscene() {
			OnStartCutscene?.Invoke();

			OnStartTelevisionEndingCutscene?.Invoke();
			HandleAnimator(false, true);
			playerAnim.Play(TV_ENDING_CUTSCENE);
		}

		public void StartCouchEndingCinematic() {
			OnStartCutscene?.Invoke();

			OnStartCouchEndingCutscene?.Invoke();

			HandleAnimator(false, true);
			couchEndingCutscene.Play();
			Invoke(nameof(DelayEndGame), (float)couchEndingCutscene.duration);

		}

		public void StartNotebookEndingCutscene() {
			OnStartCutscene?.Invoke();

			OnStartNotebookEndingCutscene?.Invoke();
			HandleAnimator(true, true);
			playerAnim.Play(NOTEBOOK_ENDING_CUTSCENE);
		}

		public void StartSleepEndingCutscene() {
			OnStartCutscene?.Invoke();

			OnStartSleepEndingCutscene?.Invoke();
			HandleAnimator(false, true);
			Invoke(nameof(DelayEndGame), 15);
		}

		public void StartMirrorCutscene() {
			OnStartCutscene?.Invoke();
			HandleAnimator(false, true);
			playerAnim.Play(MIRROR_CUTSCENE);
			AudioManager.Instance.PlayClip(Sound.Void,.9f);
		}

		private void DelayEndGame() {
			EndGameManager.Instance.EndGame();
		}

		public void StartSuffocateCutscene() {
			OnStartCutscene?.Invoke();
			HandleAnimator(true, true);

			playerAnim.Play(SUFFOCATION_CUTSCENE);
		}

		public void StartClimbingCutscene() {
			OnStartCutscene?.Invoke();
			int animationLength = 6;
			OnStartClimbingCutscene?.Invoke(animationLength+2);
			HandleAnimator(false, true);

			playerAnim.Play(CLIMBING_CUTSCENE);
		}
	}

}
