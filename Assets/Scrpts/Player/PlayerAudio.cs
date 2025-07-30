using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public void PlayWakeUpSFX() {
        AudioManager.Instance.PlayClip(Sound.WakeUp);
    }

    public void PlayJumpLandSFX() {
        AudioManager.Instance.PlayClip(Sound.JumpLand);
    }

    public void PlayDieSFX() {
        AudioManager.Instance.PlayClip(Sound.Die);
    }

    public void PLayBreathingSFX() {
		AudioManager.Instance.PlayClip(Sound.Breathing,.7f);
	}

    public void PlayScreamSFX() {
		AudioManager.Instance.PlayClip(Sound.Scream,1,1.3f);
	}

    public void PlaySlamSFX() {
		AudioManager.Instance.PlayClip(Sound.DoorSlam,.7f,1.1f);
	}

    public void PlayHitPillowSFX() {
		AudioManager.Instance.PlayClip(Sound.HitPillow);
	}

    public void PlayDemonScreamSFX() {
		AudioManager.Instance.PlayClip(Sound.DemonScream);
	}
}
