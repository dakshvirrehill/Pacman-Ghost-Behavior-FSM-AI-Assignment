using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class SBCreator : Editor
{

    [MenuItem("Assets/Create/Finite State Machine Behaviour")]
    public static void CreateStateBehaviour()
    {
        if (Selection.activeObject == null)
        {
            Debug.Log("No Object Selected");
            return;
        }
        if (Selection.activeObject.GetType() != typeof(AnimatorState))
        {
            Debug.LogFormat("Selected Object Not Animation State Type = {0}", Selection.activeObject.GetType().Name);
            return;
        }
        if (!AssetDatabase.IsValidFolder("Assets/Game"))
        {
            AssetDatabase.CreateFolder("Assets", "Game");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Game/Scripts"))
        {
            AssetDatabase.CreateFolder("Assets/Game", "Scripts");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Game/Scripts/Ghosts"))
        {
            AssetDatabase.CreateFolder("Assets/Game/Scripts", "Ghosts");
        }
        if (!AssetDatabase.IsValidFolder("Assets/Game/Scripts/Ghosts/FSM"))
        {
            AssetDatabase.CreateFolder("Assets/Game/Scripts/Ghosts", "FSM");
        }
        string aPath = GetStateBehaviourPath();
        if (File.Exists(aPath))
        {
            if (EditorUtility.DisplayDialog("File Already Exists", "Do you want to overwrite it?", "Yes", "No"))
            {
                CreateNewStateBehaviour(aPath);
            }
            return;
        }
        CreateNewStateBehaviour(aPath);
    }

    [UnityEditor.Callbacks.DidReloadScripts]
    static void AddToState()
    {
        if(Selection.activeObject == null)
        {
            return;
        }
        if (Selection.activeObject.GetType() != typeof(AnimatorState))
        {
            return;
        }

        AnimatorState  aCurState = (AnimatorState)Selection.activeObject;
        if(aCurState.behaviours.Length > 0)
        {
            return;
        }
        string aPath = GetStateBehaviourPath();

        if(!File.Exists(aPath))
        {
            return;
        }

        string aAssetPath = aPath.Substring(aPath.IndexOf("Assets"));
        aCurState.AddStateMachineBehaviour(((MonoScript)AssetDatabase.LoadAssetAtPath(aAssetPath, typeof(MonoScript))).GetClass());
    }

    static void CreateNewStateBehaviour(string pPath)
    {
        try
        {
            using (StreamWriter aWriter = new StreamWriter(pPath, false))
            {
                aWriter.WriteLine(GetDefaultStateBehaviour());
            }
        }
        catch (System.Exception aE)
        {
            Debug.LogErrorFormat("Error Occured : {0}\n{1}", aE.Message, aE.StackTrace);
        }
        AssetDatabase.Refresh();
    }

    static string GetStateBehaviourPath()
    {
       return Application.dataPath + "/Game/Scripts/Ghosts/FSM/" + GetStateBehaviourName() +".cs";
    }

    static string GetStateBehaviourName()
    {
        return Selection.activeObject.name.Replace(" ", "") + "StateBehaviour";
    }

    static string GetDefaultStateBehaviour()
    {
        System.Text.StringBuilder aStateBehaviour = new System.Text.StringBuilder();
        aStateBehaviour.AppendLine("using System.Collections;");
        aStateBehaviour.AppendLine("using System.Collections.Generic;");
        aStateBehaviour.AppendLine("using UnityEngine;");
        aStateBehaviour.AppendLine();
        aStateBehaviour.AppendLine("public class " + GetStateBehaviourName() + " : GhostBehaviour");
        aStateBehaviour.AppendLine("{");
        aStateBehaviour.AppendLine();
        aStateBehaviour.AppendLine("    //override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)");
        aStateBehaviour.AppendLine("    //{");
        aStateBehaviour.AppendLine("    //    ");
        aStateBehaviour.AppendLine("    //}");
        aStateBehaviour.AppendLine();
        aStateBehaviour.AppendLine("    //override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)");
        aStateBehaviour.AppendLine("    //{");
        aStateBehaviour.AppendLine("    //    ");
        aStateBehaviour.AppendLine("    //}");
        aStateBehaviour.AppendLine();
        aStateBehaviour.AppendLine("    //override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)");
        aStateBehaviour.AppendLine("    //{");
        aStateBehaviour.AppendLine("    //    ");
        aStateBehaviour.AppendLine("    //}");
        aStateBehaviour.AppendLine();
        aStateBehaviour.AppendLine("}");
        return aStateBehaviour.ToString();
    }
}
