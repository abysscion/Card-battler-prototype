using CoreGameplay;
using UnityEngine;

namespace Creature
{
	public class CreatureController : MonoBehaviour
	{
		[SerializeField] private DragNDropEvents dragNDropEvents;

		private void Start()
		{
			Subscribe();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe()
		{
			dragNDropEvents.MouseEntered += DragNDrop_MouseEntered;
			dragNDropEvents.MouseExited += DragNDrop_MouseExited;
			dragNDropEvents.MousePressed += DragNDrop_MousePressed;
			dragNDropEvents.MouseDragUpdated += DragNDrop_MouseDragUpdated;
			dragNDropEvents.MouseReleased += DragNDrop_MouseReleased;
		}

		private void Unsubscribe()
		{
			dragNDropEvents.MouseEntered -= DragNDrop_MouseEntered;
			dragNDropEvents.MouseExited -= DragNDrop_MouseExited;
			dragNDropEvents.MousePressed -= DragNDrop_MousePressed;
			dragNDropEvents.MouseDragUpdated -= DragNDrop_MouseDragUpdated;
			dragNDropEvents.MouseReleased += DragNDrop_MouseReleased;
		}

		private void DragNDrop_MousePressed()
		{
			Debug.Log($"Mouse pressed {gameObject.name}");
		}

		private void DragNDrop_MouseExited()
		{
			Debug.Log($"Mouse exited {gameObject.name}");
		}

		private void DragNDrop_MouseEntered()
		{
			Debug.Log($"Mouse entered {gameObject.name}");
		}

		private void DragNDrop_MouseDragUpdated()
		{
			Debug.Log($"Mouse drag started {gameObject.name}");
		}

		private void DragNDrop_MouseReleased()
		{
			Debug.Log($"Mouse drag started {gameObject.name}");
		}
	}
}
