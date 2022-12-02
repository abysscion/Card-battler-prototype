using CoreGameplay;
using UnityEngine;

namespace Creatures
{
	public class CreatureController : MonoBehaviour
	{
		[SerializeField] private DragNDropEventsThrower dragNDropHelper;

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
			dragNDropHelper.MouseEntered += OnMouseEnteredAction;
			dragNDropHelper.MouseExited += OnMouseExitedAction;
			dragNDropHelper.MousePressed += OnMousePressedAction;
			dragNDropHelper.MouseDragUpdated += OnMouseDragUpdatedAction;
			dragNDropHelper.MouseReleased += OnMouseReleasedAction;
		}

		private void Unsubscribe()
		{
			dragNDropHelper.MouseEntered -= OnMouseEnteredAction;
			dragNDropHelper.MouseExited -= OnMouseExitedAction;
			dragNDropHelper.MousePressed -= OnMousePressedAction;
			dragNDropHelper.MouseDragUpdated -= OnMouseDragUpdatedAction;
			dragNDropHelper.MouseReleased += OnMouseReleasedAction;
		}

		private void OnMousePressedAction()
		{
			//Debug.Log($"Mouse pressed {gameObject.name}");
		}

		private void OnMouseExitedAction()
		{
			//Debug.Log($"Mouse exited {gameObject.name}");
		}

		private void OnMouseEnteredAction()
		{
			//Debug.Log($"Mouse entered {gameObject.name}");
		}

		private void OnMouseDragUpdatedAction(Vector3 pos)
		{
			//Debug.Log($"Mouse drag started {gameObject.name}");
		}

		private void OnMouseReleasedAction()
		{
			//Debug.Log($"Mouse drag started {gameObject.name}");
		}
	}
}
