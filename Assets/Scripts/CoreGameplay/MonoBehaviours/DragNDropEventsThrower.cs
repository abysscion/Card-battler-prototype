using System;
using UnityEngine;

namespace CoreGameplay
{
	public class DragNDropEventsThrower : MonoBehaviour
	{
		public event Action<Vector3> MouseDragUpdated;
		public event Action MouseReleased;
		public event Action MousePressed;
		public event Action MouseEntered;
		public event Action MouseExited;

		protected virtual void OnMouseEnter()
		{
			MouseEntered?.Invoke();
		}

		protected virtual void OnMouseExit()
		{
			MouseExited?.Invoke();
		}

		protected virtual void OnMouseDown()
		{
			MousePressed?.Invoke();
		}

		protected virtual void OnMouseUp()
		{
			MouseReleased?.Invoke();
		}

		protected virtual void OnMouseDrag()
		{
			MouseDragUpdated?.Invoke(Input.mousePosition);
		}
	}
}
