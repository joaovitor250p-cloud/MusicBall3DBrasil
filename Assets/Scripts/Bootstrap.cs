using UnityEngine;
public class Bootstrap : MonoBehaviour {
 void Start(){
  if(!FindFirstObjectByType<MusicWorld>())new GameObject("Music Ball 3D Brasil").AddComponent<MusicWorld>();
  if(!FindFirstObjectByType<MusicLibrary>())new GameObject("Biblioteca").AddComponent<MusicLibrary>();
  if(!FindFirstObjectByType<MusicPlayer>())new GameObject("Reprodutor").AddComponent<MusicPlayer>();
  if(!FindFirstObjectByType<MusicMenu>())new GameObject("Interface").AddComponent<MusicMenu>();
 }
}