using System;
using Cards;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardConfig))]
public partial class CardConfigEditor : Editor
{
	private SerializedProperty _effectConfigsListProperty;
	private SerializedProperty _cardTargetTypeProperty;
	private SerializedProperty _prefabProperty;
	private CardConfig _script;

	private readonly GUILayoutOption _valuesWidth = GUILayout.Width(150);
	private readonly GUILayoutOption _labelsWidth = GUILayout.Width(100);
	private readonly GUILayoutOption _xButtonWidth = GUILayout.Width(30), _xButtonHeight = GUILayout.Height(30);

	public override void OnInspectorGUI()
	{
		_script = target as CardConfig;
		_effectConfigsListProperty = serializedObject.FindProperty("effectConfigs");
		_cardTargetTypeProperty = serializedObject.FindProperty("targetType");
		_prefabProperty = serializedObject.FindProperty("prefab");

		DrawSpecificScriptLayout();

		serializedObject.ApplyModifiedProperties();
		if (GUI.changed)
			EditorUtility.SetDirty(_script);
	}

	private void DrawSpecificScriptLayout()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Card prefab:", _labelsWidth);
		_prefabProperty.objectReferenceValue = EditorGUILayout.ObjectField("", _prefabProperty.objectReferenceValue, typeof(Card),
																			false, GUILayout.ExpandWidth(false));
		EditorGUILayout.EndHorizontal();

		DrawCardTargetTypeEnumPopup();
		GUILayout.Space(10);
		DrawEffectsList();
	}

	private void DrawEffectsList()
	{
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		GUILayout.Label("Effects:");
		for (var i = 0; i < _effectConfigsListProperty.arraySize; i++)
		{
			var effectConfigProperty = _effectConfigsListProperty.GetArrayElementAtIndex(i);
			EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
			DrawEffectSettings(effectConfigProperty);
			if (GUILayout.Button("X", _xButtonWidth, _xButtonHeight))
				_effectConfigsListProperty.DeleteArrayElementAtIndex(i);
			EditorGUILayout.EndHorizontal();
			GUILayout.Space(7);
		}
		DrawNavigationButtonsForListProperty(_effectConfigsListProperty);
		EditorGUILayout.EndVertical();
	}

	private void DrawNavigationButtonsForListProperty(SerializedProperty listProperty)
	{
		if (GUILayout.Button("Add new"))
			listProperty.InsertArrayElementAtIndex(0);
		if (GUILayout.Button("Remove all"))
		{
			if (EditorUtility.DisplayDialog("All elements removing", "Remove all elements?", "Sure!", "Nope."))
				listProperty.ClearArray();
		}
	}

	private CardEffectType DrawCardEffectTypeEnumPopup(SerializedProperty cardEffectTypeProperty)
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Effect type:", _labelsWidth);
		var selected = (CardEffectType)cardEffectTypeProperty.enumValueIndex;
		var selectedTypeName = EditorGUILayout.EnumPopup(selected, _valuesWidth, GUILayout.ExpandWidth(false)).ToString();
		var selectedTypeIndex = (int)Enum.Parse(selected.GetType(), selectedTypeName);
		cardEffectTypeProperty.enumValueIndex = selectedTypeIndex;
		EditorGUILayout.EndHorizontal();
		return (CardEffectType)selectedTypeIndex;
	}

	private void DrawCardTargetTypeEnumPopup()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Target type:", GUILayout.ExpandWidth(false), _labelsWidth);
		var selected = (CardTargetType)_cardTargetTypeProperty.enumValueIndex;
		var selectedTypeName = EditorGUILayout.EnumPopup(selected, _valuesWidth, GUILayout.ExpandWidth(false)).ToString();
		var selectedTypeIndex = (int)Enum.Parse(selected.GetType(), selectedTypeName);
		_cardTargetTypeProperty.enumValueIndex = selectedTypeIndex;
		EditorGUILayout.EndHorizontal();
	}
}
