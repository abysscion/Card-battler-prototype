using System;
using CoreGameplay;
using Creatures;
using UnityEngine;

namespace Cards
{
	public class CardDragNDropProvider : DragNDropProviderBase
	{
		[SerializeField] private LayerMask receiversMask;

		public event Action<Creature> DraggedOverCreature;

		private void Start()
		{
			DragSuccesseded += OnDragSuccesseded;
		}

		private void OnDragSuccesseded(Vector3 dragPos)
		{
			var ray = new Ray(Camera.main.ScreenToWorldPoint(dragPos), Vector3.down);
			if (!Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, receiversMask, QueryTriggerInteraction.Collide))
				return;
			if (!hitInfo.transform.TryGetComponent<CreatureDragNDropReceiver>(out var receiver))
				return;

			DraggedOverCreature?.Invoke(receiver.Creature);
		}
	}
}
