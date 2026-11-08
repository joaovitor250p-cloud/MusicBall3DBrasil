using UnityEngine;
public class Bootstrap : MonoBehaviour {
    void Start() {
        if (FindFirstObjectByType<MusicWorld>() == null) {
            var root = new GameObject("Music Ball 3D Brasil");
            root.AddComponent<MusicWorld>();
        }
    }
}