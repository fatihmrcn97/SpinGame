using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent), editorForChildClasses: true)]
public abstract class GameEventEditor : GameAssetEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUI.enabled = Application.isPlaying;
        if (GUILayout.Button("Raise")) ButtonClicked();
    }

    public abstract void ButtonClicked();
}
