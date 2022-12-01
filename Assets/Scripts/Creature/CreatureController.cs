using CoreGameplay;
using UnityEngine;

namespace Creature
{
	public class CreatureController : MonoBehaviour
	{
		[SerializeField] private DragNDropEvents dragNDrop;

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
			dragNDrop.MouseEntered += DragNDrop_MouseEntered;
			dragNDrop.MouseExited += DragNDrop_MouseExited;
			dragNDrop.MousePressed += DragNDrop_MousePressed;
			dragNDrop.MouseDragUpdated += DragNDrop_MouseDragUpdated;
			dragNDrop.MouseReleased += DragNDrop_MouseReleased;
		}

		private void Unsubscribe()
		{
			dragNDrop.MouseEntered -= DragNDrop_MouseEntered;
			dragNDrop.MouseExited -= DragNDrop_MouseExited;
			dragNDrop.MousePressed -= DragNDrop_MousePressed;
			dragNDrop.MouseDragUpdated -= DragNDrop_MouseDragUpdated;
			dragNDrop.MouseReleased += DragNDrop_MouseReleased;
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
