
using System;
using UnityEngine;

namespace bananplayss {
	public class PlayerMovement : MonoBehaviour {
		public event Action OnDemonJumpscare;

		private CharacterController characterController;
		public float speed = 5f;

		public float mouseSensitivity = 2f;
		private float verticalRotation = 0f;
		private Transform cameraTransform;
		private float moveHorizontal;
		private float moveVertical;
		private Vector3 movementVector;
		private float timer;
		private float defaultYPos = 0;
		private float headBobSpeed = 9.4f;
		private float headBobAmount = 0.08f;

		private PlayerSleep playerSleep;
		private bool isBusy = false;

		private float minimumWalkTime = .4f;
		private float walkTimer;
		public AudioSource footsteps;

		[SerializeField] private Transform feet;
		[SerializeField] private GameObject walkParticles;
		[SerializeField] private Transform cameraSway;
		[SerializeField] private Transform television;
		[SerializeField] private MeshRenderer playerModelRenderer;
		[SerializeField] private GameObject notebook;
		[SerializeField] private WaterBlock waterBlock;
		[SerializeField] private SleepUI sleepUI;
		[SerializeField] private Transform demonRoomPosition;
		[SerializeField] private AudioSource underwaterSFX;
		[SerializeField] private AudioSource brownNoise;
		[SerializeField] private AudioSource creepyAmbient;

		private bool inDemonRoom = false;

		private float particleCooldown = .5f;
		private float particleTimer;
		private bool canInstantiateParticles = true;

		[Header("Sway Settings")]
		private float swayAmount = 3f;
		private float smoothAmount = 6f;
		private float maxRotation = 7f;
		Quaternion initialRotation;

		private Animator anim;

		private bool isDead = false;
		private bool pausedGame = false;

		public void Die() {
			GetComponent<Animator>().enabled = false;
			isDead = true;
			playerModelRenderer.enabled = false;
			notebook.SetActive(false);
			playerSleep.Vignette();
			cameraTransform.GetComponent<Collider>().enabled = true;
			cameraTransform.GetComponent<Rigidbody>().isKinematic = false;
			GetComponent<Collider>().enabled = false;
			Invoke(nameof(DelayEndGame), 5f);
		}

		private void DelayEndGame() {
			EndGameScreenUI.Instance.ShowEndGameScreen(EndGameManager.EndGameCase.NotebookEnding);
		}

		private void Start() {
			anim = GetComponent<Animator>();
			initialRotation = cameraSway.localRotation;
			characterController = GetComponent<CharacterController>();
			cameraTransform = Camera.main.transform;
			playerSleep = GetComponent<PlayerSleep>();
			sleepUI.OnFadeOut += SleepUI_OnFadeOut;
			CinematicManager.Instance.OnStartSleepCutscene += CinematicManager_OnStartSleepCutscene;
			CinematicManager.Instance.OnStartSitCutscene += CinematicManager_OnStartSitCutscene;
			CinematicManager.Instance.OnStartStandupCutscene += CinematicManager_OnStartStandupCutscene;
			EscapeMenuUI.Instance.OnEnableEscapeMenu += Instance_OnEnableEscapeMenu1;
			CinematicManager.Instance.OnStartGameCutscene += CinematicManager_OnStartGameCutscene;
			CinematicManager.Instance.OnStartGame += Instance_OnStartGame;
			playerSleep.OnWakeup += PlayerSleep_OnWakeup;
			waterBlock.OnTriggerHead += WaterBlock_OnTriggerHead;
		}

		private void Instance_OnStartGame() {
			verticalRotation = 0f;
			isBusy = false;
		}

		private void CinematicManager_OnStartGameCutscene() {
			verticalRotation = 0f;
			isBusy = true;
			footsteps.enabled = false;
		}

		private void Instance_OnEnableEscapeMenu1(bool obj) {
			pausedGame = obj;
		}

		private void SleepUI_OnFadeOut() {
			creepyAmbient.Play();
			anim.enabled = false;
			transform.position = demonRoomPosition.position;
			inDemonRoom = true;
			Invoke(nameof(DelayDemonJumpscare), 7f);
		}

		private void DelayDemonJumpscare() {
			OnDemonJumpscare?.Invoke();
			AudioManager.Instance.PlayClip(Sound.DemonJumpscare,.8f);
		}

		private void WaterBlock_OnTriggerHead() {
			characterController.enabled = false;
			footsteps.enabled = false;
			underwaterSFX.Play();
			brownNoise.Stop();
			Invoke(nameof(DelaySuffocateCinematic), 10f);
		}

		private void DelaySuffocateCinematic() {
			CinematicManager.Instance.StartSuffocateCutscene();
			isDead = true;
		}

		private void CinematicManager_OnStartStandupCutscene() {
			verticalRotation = 0f;
			isBusy = false;
			footsteps.enabled = false;
		}

		private void CinematicManager_OnStartSitCutscene() {
			isBusy = true;
			footsteps.enabled = false;
		}

		private void PlayerSleep_OnWakeup() {
			verticalRotation = 0f;
			isBusy = false;
		}

		private void CinematicManager_OnStartSleepCutscene() {
			isBusy = true;
			footsteps.enabled = false;
		}

		private void MovePlayer() {
			Vector3 move;
			if (characterController.enabled == false) {
				move = Vector3.zero;
				movementVector = Vector3.zero;
				return;
			}
			move = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
			movementVector = move * speed;

			if (movementVector != Vector3.zero && characterController.velocity != Vector3.zero) {
				walkTimer += Time.deltaTime;
				if (walkTimer > minimumWalkTime && !isBusy) {
					footsteps.enabled = true;
				}
			} else {
				walkTimer = 0f;
				footsteps.enabled = false;
			}

			characterController.Move(movementVector);
		}

		private void HandleHeadbob() {
			if (movementVector != Vector3.zero && characterController.velocity != Vector3.zero) {
				timer += Time.deltaTime * headBobSpeed;
				cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, defaultYPos + Mathf.Sin(timer) * headBobAmount, cameraTransform.localPosition.z);

				Vector3 velocity = characterController.velocity.normalized;

				Vector3 localVelocity = cameraTransform.InverseTransformDirection(velocity.normalized);

				float swayZ = Mathf.Clamp(-localVelocity.x * swayAmount, -maxRotation, maxRotation);
				float swayX = Mathf.Clamp(localVelocity.z * swayAmount, -maxRotation, maxRotation);

				Quaternion targetRotation = Quaternion.Euler(swayX, 0f, swayZ);

				cameraSway.localRotation = Quaternion.Slerp(cameraSway.localRotation, initialRotation * targetRotation, Time.deltaTime * smoothAmount);


				float requiredY = .42f;
				if (cameraTransform.localPosition.y < requiredY && canInstantiateParticles) {
					//DisablePArticles
					return;
					canInstantiateParticles = false;

					GameObject particles = Instantiate(walkParticles, feet.position, Quaternion.identity);
					particles.transform.SetParent(transform, true);
					particles.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
					particles.transform.forward = cameraTransform.forward;
				}
				if (!canInstantiateParticles) {
					particleTimer += Time.deltaTime;
					if (particleTimer > particleCooldown) {
						canInstantiateParticles = true;
						particleTimer = 0;
					}
				}
			}
		}

		private void RotateCamera() {
			if (inDemonRoom) {
				cameraTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
				cameraTransform.localPosition = Vector3.zero;
				return;
			}
			float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
			transform.Rotate(0, horizontalRotation, 0);

			float maxAngle = 90f;
			verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
			verticalRotation = Mathf.Clamp(verticalRotation, -maxAngle, maxAngle);

			cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		}

		private void Update() {
			if (isBusy || isDead || pausedGame) return;
			moveHorizontal = Input.GetAxisRaw("Horizontal");
			moveVertical = Input.GetAxisRaw("Vertical");

			RotateCamera();
		}
		private void FixedUpdate() {
			if (isBusy || isDead || inDemonRoom || pausedGame) return;
			MovePlayer();
			HandleHeadbob();
		}
	}

}
