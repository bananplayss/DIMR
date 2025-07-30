
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
	[SerializeField] private PlayerAudio playerAudio;
	private bool played = false;

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Floor") && !played) {
			played = true;
			GetComponent<Rigidbody>().AddForce(transform.forward*5f,ForceMode.Impulse);
			playerAudio.PlayDieSFX();
		}
	}
}
