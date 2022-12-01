using UnityEngine;

namespace CoreGameplay
{
	public class DragNDropEvents : MonoBehaviour
	{
		public event System.Action MouseEntered;
		public event System.Action MouseExited;
		public event System.Action MousePressed;
		public event System.Action MouseReleased;
		public event System.Action MouseDragUpdated;

		private void OnMouseEnter()
		{
			MouseEntered?.Invoke();
		}

		private void OnMouseExit()
		{
			MouseExited?.Invoke();
		}

		private void OnMouseDown()
		{
			MousePressed?.Invoke();
		}

		private void OnMouseUp()
		{
			MouseReleased?.Invoke();
		}

		private void OnMouseDrag()
		{
			MouseDragUpdated?.Invoke();
		}
	}
}
