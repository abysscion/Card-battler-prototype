using System;
using UnityEngine;

namespace CoreGameplay
{
	public class DragNDropProviderBase : DragNDropEventsThrower
	{
		[SerializeField] private Renderer viewObject;
		[SerializeField] private Material ghostMaterial;

		/// <summary> T0 is a pointer on screen position. </summary>
		public event Action<Vector3> DragSuccesseded;

		private Transform _dndGhost;

		protected virtual void Start()
		{
			GameController.TurnEnded += OnTurnEnded;
		}

		protected virtual void OnDestroy()
		{
			GameController.TurnEnded -= OnTurnEnded;
		}

		protected override void OnMouseDrag()
		{
			base.OnMouseDrag();

			if (!_dndGhost)
			{
				_dndGhost = new GameObject($"[DragNDrop] Ghost of {name}").transform;
				var ghostRenderer = Instantiate(viewObject, _dndGhost);
				var originalMaterialColor = viewObject.material.color;
				originalMaterialColor.a = ghostMaterial.color.a;
				ghostRenderer.material = ghostMaterial;
				ghostRenderer.material.color = originalMaterialColor;
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

		private void OnTurnEnded(GameTeamType _)
		{
			if (_dndGhost)
				Destroy(_dndGhost.gameObject);
		}
	}
}
