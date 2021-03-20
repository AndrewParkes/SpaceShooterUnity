using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class DebugTool : EditorWindow {

    private List<DebugObject> debuggableObjects  = new List<DebugObject>();
    public GameObject source;

 public Transform root;

    [MenuItem("Tools/Debugger Tool")]
    public static void DebuggerTool() {
        GetWindow<DebugTool>("Debug Tool");
    }

    void OnGUI() {

root = EditorGUILayout.ObjectField("root", root, typeof(Transform), true) as Transform;

        GUILayout.Button("Debug");

        source = EditorGUILayout.ObjectField(source, typeof(GameObject), true) as GameObject;
        if(source != null) {
            DebugObject debugObject = new DebugObject();
            debugObject.debuggableObject = source;
            debuggableObjects.Add(debugObject);
            source = null;
        }
        
        for (int i = 0; i < debuggableObjects.Count; i++) {
            GameObject debuggableObject=debuggableObjects[i].debuggableObject;
            GUILayout.BeginVertical();
            debuggableObjects[i].collapse = EditorGUILayout.Foldout(debuggableObjects[i].collapse, ""+debuggableObject.name);

            if(GUILayout.Button("Remove")) {
                debuggableObjects.Remove(debuggableObjects[i]);
                continue;
            }

            GUILayout.EndVertical();
            if (debuggableObjects[i].collapse)
            {
                AddVariables(debuggableObject);

            }
        }
        GUILayout.Space(20);
        GUILayout.Button("Debug size " + debuggableObjects.Count);
    }

    private void AddVariables(GameObject debuggableObject) {
        foreach (var thisVar in debuggableObject.GetType().GetProperties())
            {
                GUILayout.BeginHorizontal();
                
                
                try {
                if( thisVar.GetValue(debuggableObject,null) is Component) {
                    GUILayout.Label("Comp");
                    //AddVariables((GameObject)thisVar.GetValue(debuggableObject,null));
                }
                
                
                    
                    GUILayout.Label(""+thisVar.Name);
                    GUILayout.Label(""+thisVar.PropertyType);
                    GUILayout.Label(""+thisVar.GetValue(debuggableObject,null));
                    
                }
                catch {
                } 
                GUILayout.EndHorizontal();
            }
    }

    private class DebugObject {
        public bool collapse = true;
        public GameObject debuggableObject;
    }
}
