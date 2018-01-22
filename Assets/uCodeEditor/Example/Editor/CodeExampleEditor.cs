
#define USE_FOLDING

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using uCodeEditor;

[CustomEditor(typeof(CodeExample))]
public class CodeExampleEditor : Editor 
{
	ShaderCodeEditor codeEditor;

	SerializedObject editorSerializedObject;

	SerializedProperty codeProp;
#if USE_FOLDING
	SerializedProperty foldedProp;
	[SerializeField] bool folded;
#endif

	void OnEnable()
	{
		editorSerializedObject = new SerializedObject(this);
		codeProp = serializedObject.FindProperty("code");
#if USE_FOLDING
		folded = true;
		foldedProp = editorSerializedObject.FindProperty("folded");
		if (folded) {} // prevent warning
#else
		SerializedProperty foldedProp = null;
#endif
		codeEditor = new ShaderCodeEditor("code", codeProp, foldedProp);
	}

    public override void OnInspectorGUI()
    {
        var myTarget = (CodeExample)target;
        serializedObject.Update();

		if (editorSerializedObject != null)
			editorSerializedObject.Update();

		if (codeEditor != null)
			codeEditor.Draw();

		if (editorSerializedObject != null)
			editorSerializedObject.ApplyModifiedProperties();

		serializedObject.ApplyModifiedProperties();
	}
}