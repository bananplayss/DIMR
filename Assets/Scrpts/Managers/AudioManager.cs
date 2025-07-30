using UnityEngine;

public enum Sound {
	WakeUp,
	JumpLand,
	Die,
	Jumpscare,
	Breathing,
	Scream,
	DoorSlam,
	HitPillow,
	Gasping,
	Void,
	CreepyAmbient,
	DemonJumpscare,
	Laughter,
	DemonScream,
}

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance {  get; private set; }

    private AudioSource m_AudioSource;

	private void Awake() {
		Instance = this;
	}

	[SerializeField] private AudioClip[] audioClipArray;

	private void Start() {
		m_AudioSource = GetComponent<AudioSource>();
	}
	public void PlayClip(Sound clip,float volume = 1f, float pitch = 1,float spatialBlend = 0) {
		m_AudioSource.clip = audioClipArray[(int)clip];
		m_AudioSource.volume = volume;
		m_AudioSource.pitch = pitch;
		m_AudioSource.spatialBlend = spatialBlend;

		m_AudioSource.Play();
    }
}
