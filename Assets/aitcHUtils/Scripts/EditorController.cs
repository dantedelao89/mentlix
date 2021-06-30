using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EditorController : Editor
{
    [MenuItem("GameObject/UI/Option",false)]
    static void InstantiatePrefab()
    {
        GameObject obj = PrefabUtility.InstantiatePrefab(Resources.Load("Option") as GameObject) as GameObject;
        Undo.RegisterCreatedObjectUndo(obj, "Option");
        PrefabUtility.UnpackPrefabInstance(obj, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        obj.transform.SetParent(Selection.gameObjects[0].transform, false);
        Selection.activeObject = obj;
        //EditorWindow.focusedWindow.SendEvent(new Event
        //{
        //    keyCode = KeyCode.F2, type = EventType.KeyDown
        //});
    }
}
#endif
