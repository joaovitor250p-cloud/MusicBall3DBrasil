using UnityEngine;
public class MusicMenu : MonoBehaviour {
 MusicLibrary library; MusicPlayer player; MusicSettings settings;
 Vector2 scroll; bool showList=true;
 void Start(){
  library=FindFirstObjectByType<MusicLibrary>();
  if(!library)library=new GameObject("Biblioteca").AddComponent<MusicLibrary>();
  player=FindFirstObjectByType<MusicPlayer>();
  if(!player)player=new GameObject("Reprodutor").AddComponent<MusicPlayer>();
  player.library=library;
  settings=FindFirstObjectByType<MusicSettings>();
 }
 void OnGUI(){
  float scale=Mathf.Max(1f,Screen.width/420f);GUI.matrix=Matrix4x4.Scale(new Vector3(scale,scale,1));
  float w=Screen.width/scale,h=Screen.height/scale;
  GUILayout.BeginArea(new Rect(12,12,w-24,h-24));
  GUILayout.BeginHorizontal();
  if(GUILayout.Button(showList?"Fechar playlist":"Abrir playlist",GUILayout.Height(42)))showList=!showList;
  if(GUILayout.Button("Buscar musicas",GUILayout.Height(42)))library.ScanPhone();
  GUILayout.EndHorizontal();
  if(showList){
   GUILayout.Label("MUSIC BALL 3D BRASIL");
   if(settings!=null&&GUILayout.Button("Modo: "+settings.mode,GUILayout.Height(42)))settings.NextMode();
   if(settings!=null){GUILayout.Label("Volume");settings.masterVolume=GUILayout.HorizontalSlider(settings.masterVolume,0,1);settings.Apply(player);}
   scroll=GUILayout.BeginScrollView(scroll,GUILayout.Height(h*.58f));
   for(int i=0;i<library.songs.Count;i++){
    int n=i;GUILayout.BeginHorizontal();
    if(GUILayout.Button(library.songs[i].title,GUILayout.Height(40)))player.PlayIndex(n);
    if(GUILayout.Button("↑",GUILayout.Width(38)))library.Move(n,-1);
    if(GUILayout.Button("↓",GUILayout.Width(38)))library.Move(n,1);
    if(GUILayout.Button("X",GUILayout.Width(38)))library.Remove(n);
    GUILayout.EndHorizontal();
   }
   GUILayout.EndScrollView();
  }
  GUILayout.FlexibleSpace();
  GUILayout.BeginHorizontal();
  if(GUILayout.Button("Anterior",GUILayout.Height(48)))player.Previous();
  if(GUILayout.Button(player.output&&player.output.isPlaying?"Pausar":"Tocar",GUILayout.Height(48))){
   if(player.output.isPlaying)player.output.Pause();
   else if(player.output.clip!=null)player.output.UnPause();
   else if(library.songs.Count>0)player.PlayIndex(0);
  }
  if(GUILayout.Button("Proxima",GUILayout.Height(48)))player.Next();
  GUILayout.EndHorizontal();
  GUILayout.EndArea();
 }
}