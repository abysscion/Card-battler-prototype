using CoreGameplay;
using UnityEditor;
using UnityEngine;

namespace Card
{
	public class Card : MonoBehaviour
	{
		[SerializeField] private DragNDropEvents dragNDropEvents;

		private void Start()
		{
			dragNDropEvents.MouseDragUpdated += DragNDropEvents_MouseDragUpdated;
			dragNDropEvents.MouseReleased += DragNDropEvents_MouseReleased;
		}

		private void DragNDropEvents_MouseReleased()
		{
			//TODO: add drag n drop observer class => craete public "mouseover object" or smth like that
		}

		private void DragNDropEvents_MouseDragUpdated()
		{
			
		}
	}
}