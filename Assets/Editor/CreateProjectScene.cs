#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public static class CreateProjectScene {
    [MenuItem("Music Ball Brasil/Criar cena inicial")]
    public static void Create() {
        Directory.CreateDirectory("Assets/Scenes");
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        new GameObject("Bootstrap").AddComponent<Bootstrap>();
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Main.unity");
        EditorBuildSettings.scenes = new [] { new EditorBuildSettingsScene("Assets/Scenes/Main.unity", true) };
    }
}
#endif