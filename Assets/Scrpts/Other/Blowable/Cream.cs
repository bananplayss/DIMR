using UnityEngine;

public class Cream : MonoBehaviour, IBlowable {
	private bool isBlowing = false;
	private Animator animator;
	private const string BLOW_CREAM = "BlowCream";

	private void Start() {
		animator = GetComponent<Animator>();
	}

	public void Blow() {
		if (isBlowing) return;
		animator.Play(BLOW_CREAM);
	}

	public void StopBlow() {
	}
}
