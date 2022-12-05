using System;
using Cards;
using Creatures;
using UnityEditor;
using UnityEngine;

public partial class CardConfigEditor : Editor
{
	private void DrawEffectSettings(SerializedProperty effectConfigProperty)
	{
		EditorGUILayout.BeginVertical();
		var cardEffectType = DrawCardEffectTypeEnumPopup(effectConfigProperty.FindPropertyRelative("effectType"));
		DrawTurnsDurationProperty(effectConfigProperty);
		DrawEffectSettingsDependingOnEffectType(cardEffectType, effectConfigProperty);
		EditorGUILayout.EndVertical();
	}

	private void DrawEffectSettingsDependingOnEffectType(CardEffectType cardEffectType, SerializedProperty effectConfigProperty)
	{
		switch (cardEffectType)
		{
			case CardEffectType.StatChange:
				DrawStatChangeEffectSettings(effectConfigProperty);
				break;
			case CardEffectType.HealthDamage:
				DrawHealthDamageEffectSettings(effectConfigProperty);
				break;
			case CardEffectType.Heal:
				DrawHealEffectSettings(effectConfigProperty);
				break;
			case CardEffectType.Shield:
				DrawShieldEffectSettings(effectConfigProperty);
				break;
			case CardEffectType.Poison:
				DrawPoisonEffectSettings(effectConfigProperty);
				break;
			default:
				Debug.LogError($"[{nameof(CardConfigEditor)}] There is no drawer for effect type {cardEffectType}!");
				break;
		}
	}

	private void DrawPoisonEffectSettings(SerializedProperty effectConfigProperty)
	{
		DrawStatChangeEffectSettings(effectConfigProperty);
	}

	private void DrawShieldEffectSettings(SerializedProperty effectConfigProperty)
	{
		var valueProperty = effectConfigProperty.FindPropertyRelative("value");

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Capacity: ", _labelsWidth);
		valueProperty.floatValue = EditorGUILayout.FloatField(valueProperty.floatValue, _valuesWidth);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawHealEffectSettings(SerializedProperty effectConfigProperty)
	{
		var valueProperty = effectConfigProperty.FindPropertyRelative("value");

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Heal amount: ", _labelsWidth);
		valueProperty.floatValue = EditorGUILayout.FloatField(valueProperty.floatValue, _valuesWidth);
		EditorGUILayout.EndHorizontal();
		DrawDispellableEffectTypesProperty(effectConfigProperty);
	}

	private void DrawHealthDamageEffectSettings(SerializedProperty effectConfigProperty)
	{
		var valueProperty = effectConfigProperty.FindPropertyRelative("value");

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Damage amount: ", _labelsWidth);
		valueProperty.floatValue = EditorGUILayout.FloatField(valueProperty.floatValue, _valuesWidth);
		EditorGUILayout.EndHorizontal();
	}

	private void DrawStatChangeEffectSettings(SerializedProperty effectConfigProperty)
	{
		var statModifiersListProperty = effectConfigProperty.FindPropertyRelative("statModifiers");

		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		GUILayout.Label("Affectable stats:", _labelsWidth);
		for (var i = 0; i < statModifiersListProperty.arraySize; i++)
		{
			var statModifierProperty = statModifiersListProperty.GetArrayElementAtIndex(i);
			EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
			DrawCreatureStatModifierProperty(statModifierProperty);
			if (GUILayout.Button("X", _xButtonWidth, _xButtonHeight))
				statModifiersListProperty.DeleteArrayElementAtIndex(i);
			EditorGUILayout.EndHorizontal();
			GUILayout.Space(5);
		}
		DrawNavigationButtonsForListProperty(statModifiersListProperty);
		EditorGUILayout.EndVertical();
	}

	private void DrawDispellableEffectTypesProperty(SerializedProperty effectConfigProperty)
	{
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		GUILayout.Label("Will dispell:", _labelsWidth);
		var dispellableEffectTypesListProperty = effectConfigProperty.FindPropertyRelative("dispellableEffectTypes");
		for (var i = 0; i < dispellableEffectTypesListProperty.arraySize; i++)
		{
			EditorGUILayout.BeginHorizontal();
			var dispellableTypeProperty = dispellableEffectTypesListProperty.GetArrayElementAtIndex(i);
			var selected = (CardEffectType)dispellableTypeProperty.enumValueIndex;
			var selectedTypeName = EditorGUILayout.EnumPopup(selected, _valuesWidth).ToString();
			var selectedTypeIndex = (int)Enum.Parse(selected.GetType(), selectedTypeName);
			dispellableTypeProperty.enumValueIndex = selectedTypeIndex;
			if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
				dispellableEffectTypesListProperty.DeleteArrayElementAtIndex(i);
			EditorGUILayout.EndHorizontal();
		}
		DrawNavigationButtonsForListProperty(dispellableEffectTypesListProperty);
		EditorGUILayout.EndVertical();
	}

	private void DrawCreatureStatModifierProperty(SerializedProperty creatureStatModifierProperty)
	{
		EditorGUILayout.BeginVertical();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Stat:", _labelsWidth);
		var typeProperty = creatureStatModifierProperty.FindPropertyRelative("type");
		var selected = (CreatureStatType)typeProperty.enumValueIndex;
		var selectedTypeName = EditorGUILayout.EnumPopup(selected, _valuesWidth).ToString();
		var selectedTypeIndex = (int)Enum.Parse(selected.GetType(), selectedTypeName);
		typeProperty.enumValueIndex = selectedTypeIndex;
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Modifier:", _labelsWidth);
		var valueProperty = creatureStatModifierProperty.FindPropertyRelative("value");
		valueProperty.floatValue = EditorGUILayout.FloatField(valueProperty.floatValue, _valuesWidth);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndVertical();
	}

	private void DrawTurnsDurationProperty(SerializedProperty effectConfigProperty)
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Turns duration:", _labelsWidth);
		var duration = effectConfigProperty.FindPropertyRelative("turnsDuration");
		duration.intValue = EditorGUILayout.IntField(duration.intValue, _valuesWidth);
		EditorGUILayout.EndHorizontal();
	}
}
