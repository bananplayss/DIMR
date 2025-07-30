
using UnityEngine;
namespace bananplayss{
	public class DoorTrigger : MonoBehaviour {
		[SerializeField] private Door door;

		private void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Player")) {
				ActivityIndicatorUI.Instance.HideIndicators();
				door.Close();
				gameObject.SetActive(false);
			}
		}
	}

}
