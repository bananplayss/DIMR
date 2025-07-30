using bananplayss;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomAudioManager : MonoBehaviour
{
   public static RoomAudioManager Instance { get; private set; }

	[SerializeField] private AudioSource[] audioSources;
	[SerializeField] private float[] volumes;
	private bool isBait = false;

	private void Awake() {
		Instance = this;
	}

	private void Start() {
		CinematicManager.Instance.OnStartSleepEndingCutscene += Instance_OnStartSleepEndingCutscene;

		volumes = new float[audioSources.Length];
		for (int i = 0; i < volumes.Length; i++) {
			volumes[i] = audioSources[i].volume;
		}
	}

	private void Instance_OnStartSleepEndingCutscene() {
		isBait = true;
	}

	public void StartFadingSources(int delay) {
		for (int i = 0; i < audioSources.Length; i++) {
			audioSources[i].volume = 0;
		}

		Invoke(nameof(FadeInSources), delay);

	}
	public void FadeInSources() {
		if (isBait) return;
		for (int i = 0; i < audioSources.Length; i++) {
			audioSources[i].volume = volumes[i];
		}
	}
}
