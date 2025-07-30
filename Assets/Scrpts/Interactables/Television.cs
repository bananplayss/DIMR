using UnityEngine;
using UnityEngine.Video;

namespace bananplayss {
	public class Television : MonoBehaviour, IInteractable {
		private bool tvOn = true;
		[SerializeField] private VideoPlayer tvPanel;
		[SerializeField] private VideoPlayer endingPanel;
		[SerializeField] private VideoPlayer jumpscareVideo;
		[SerializeField] private MeshRenderer panelRenderer;
		[SerializeField] private Material endingMaterial;
		[SerializeField] private AudioSource audioSource;

		private void Start() {
			EndGameManager.Instance.OnStartTelevisionEnding += EndGameManager_OnStartTelevisionEnding;
			EndGameManager.Instance.OnSwitchShow += EndGameManager_OnSwitchShow;
		}

		private void EndGameManager_OnSwitchShow() {
			DisplayMessageUI.Instance.DisplayMessage("Hm \nThis tv is interesting.","Erdekes ez a tv");
		}

		private void EndGameManager_OnStartTelevisionEnding() {
			tvPanel.gameObject.SetActive(false);
			endingPanel.gameObject.SetActive(true);
			audioSource.Stop();

			Invoke(nameof(DelayTelevisionJumpscare), 8f);
		}

		private void DelayTelevisionJumpscare() {
			endingPanel.gameObject.SetActive(false);

			jumpscareVideo.gameObject.SetActive(true);
		}

		public void Interact() {
			tvOn = !tvOn;
			audioSource.mute = !tvOn;
			PlayerStats.Interact();
			EndGameManager.Instance.isTvOn = tvOn;
			EndGameManager.Instance.tvInteractCounter++;
			EndGameManager.Instance.CheckForEndGame(EndGameManager.EndGameCase.TelevisionEnding);
			Vector3 to = tvOn == true ? new Vector3(0.0910290033f, 0.100000001f, 0.0460390002f) : Vector3.zero;
			float time = .2f;
			LeanTween.scale(tvPanel.gameObject, to, time);
		}

		public bool UsesIndicator() {
			return false;
		}
	}

}
