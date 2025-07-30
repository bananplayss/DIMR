using UnityEngine;

public class Towel : MonoBehaviour, IBlowable {
	private Animator animator;
	private const string BLOW = "Blow";
	private bool isBlowing = false;

	private void Start() {
		animator = GetComponent<Animator>();
	}

	public void Blow() {
		if (isBlowing) return;
		isBlowing = true;
		animator.SetBool(BLOW, true);
	}

	public void StopBlow() {
		isBlowing = false;
		animator.SetBool(BLOW, false);
	}
}
