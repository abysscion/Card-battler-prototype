using System;
using UnityEngine;

namespace CoreGameplay
{
	public class DragNDropHelperBase : DragNDropEventsThrower
	{
		[SerializeField] private Renderer viewObject;
		[SerializeField] private Material ghostMaterial;

		/// <summary> T1 is a pointer on screen position. </summary>
		public event Action<Vector3> DragSuccesseded;

		private Transform _dndGhost;

		protected override void OnMouseDrag()
		{
			base.OnMouseDrag();

			if (!_dndGhost)
			{
				_dndGhost = new GameObject($"[DragNDrop] Ghost of {name}").transform;
				var ghostRenderer = Instantiate(viewObject, _dndGhost);
				ghostRenderer.material = ghostMaterial;
			}

			var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.y = viewObject.transform.position.y + 1f;
			_dndGhost.position = pos;
		}

		protected override void OnMouseUp()
		{
			base.OnMouseUp();
			if (_dndGhost)
			{
				DragSuccesseded?.Invoke(Input.mousePosition);
				Destroy(_dndGhost.gameObject);
			}
		}
	}
}
