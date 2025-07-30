using System;
using UnityEngine;
using UnityEngine.AI;

namespace bananplayss {
	public class PlayerNavigation : MonoBehaviour {
		public event Action OnStartSleepCutscene;

		private Animator anim;
		private NavMeshAgent agent;
		[SerializeField] private Vector3 animStartPosVec;
		[SerializeField] private Vector3 animStartPlayerRot;
		[SerializeField] private Vector3 animStartCamRot;

		private bool navigating = false;
		private bool rotating = false;

		private float distanceNeeded = 1f;
		private float rotateTime = .5f;
		private float delayTime = 3.5f;
		private float defaultYpos = 1.08f;

		private PlayerSleep playerSleep;

		private void Start() {
			//Disabled
			this.enabled = false;

			anim = GetComponent<Animator>();
			agent = GetComponent<NavMeshAgent>();
			agent.enabled = false;
			playerSleep = GetComponent<PlayerSleep>();
			CinematicManager.Instance.OnStartSleepCutscene += CinematicManager_OnStartSleepCutsceneNavigation;
			playerSleep.OnSleep += PlayerSleep_OnSleep;
		}

		private void PlayerSleep_OnSleep() {
			rotating = false;
		}

		private void CinematicManager_OnStartSleepCutsceneNavigation() {
			//navigating = true;
			//agent.enabled = true;
			//agent.SetDestination(animStartPosVec);

		}

		public bool IsNavigating() {
			return navigating;
		}

		private void Update() {
			if (agent.destination != Vector3.zero) {
				if (Vector3.Distance(transform.position, agent.destination) < distanceNeeded) {
					rotating = true;

					Invoke(nameof(DelayEventTrigger), delayTime);
					agent.enabled = false;
					transform.position = new Vector3(transform.position.x, defaultYpos, transform.position.z);
					navigating = false;
					anim.enabled = false;
				}
			}

			if (rotating) {
				MoveTowardsRot(Camera.main.transform, animStartCamRot, rotateTime);
				MoveTowardsRot(transform, animStartPlayerRot, rotateTime * 1.5f);
			}
		}

		private void MoveTowardsRot(Transform transform, Vector3 rot, float rotateTime) {
			transform.localRotation = Quaternion.Euler(Vector3.MoveTowards(transform.localRotation.eulerAngles, rot, rotateTime));
		}

		private void DelayEventTrigger() {
			OnStartSleepCutscene?.Invoke();
		}
	}

}
