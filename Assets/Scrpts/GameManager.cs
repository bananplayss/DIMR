
using UnityEngine;

namespace bananplayss {
	public class GameManager : MonoBehaviour {
		public static GameManager Instance { get; private set; }

		public static int allEndings = 7;

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			PlayerStats.LoadAllStats();
		}

		private void OnDisable() {
			PlayerStats.SaveAllStats();
		}
	}

}
