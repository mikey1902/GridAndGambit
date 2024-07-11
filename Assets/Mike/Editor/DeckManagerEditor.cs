using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DrawManager))]
public class DeckManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		DrawManager drawManager = (DrawManager)target;
		if (GUILayout.Button("Draw Next Card"))
		{
			HandManager handManager = FindObjectOfType<HandManager>();
			if (handManager != null)
			{
				drawManager.DrawCard(handManager);
			}
		}
	}
}
#endif