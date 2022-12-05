using System.Collections.Generic;
using Cards;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardConfigsContainer))]
public class CardConfigsContainerEditor : Editor
{
	private CardConfigsContainer _script;

	private void OnEnable()
	{
		UpdateCardConfigsContainer();
	}

	public override void OnInspectorGUI()
	{
		_script = target as CardConfigsContainer;

		//GUI.enabled = false;
		DrawDefaultInspector();
		//GUI.enabled = true;
		//DrawSpecificScriptLayout();

		serializedObject.ApplyModifiedProperties();
		if (GUI.changed)
			EditorUtility.SetDirty(_script);
	}

	private void DrawSpecificScriptLayout()
	{
		if (GUILayout.Button("Update card configs container"))
			UpdateCardConfigsContainer();
	}

	private void UpdateCardConfigsContainer()
	{
		//var assetsGUIDs = AssetDatabase.FindAssets($"t:{typeof(CardConfig).FullName}", new[] { "Assets" });
		//var bindingFlags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
		//var cardsList = _script.GetType().GetField("cards", bindingFlags).GetValue(_script) as ICollection<CardConfig>;

		//cardsList.Clear();
		//foreach (var assetGUID in assetsGUIDs)
		//{
		//	var assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
		//	var asset = AssetDatabase.LoadAssetAtPath<CardConfig>(assetPath);

		//	cardsList.Add(asset);
		//}
	}
}
