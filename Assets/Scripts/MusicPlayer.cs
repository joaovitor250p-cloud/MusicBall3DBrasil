using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class MusicPlayer : MonoBehaviour {
 public MusicLibrary library;
 public AudioSource output;
 public int index=-1;
 public bool repeatAll=true;
 public event Action OnSongChanged;
 void Awake(){output=gameObject.AddComponent<AudioSource>();output.playOnAwake=false;}
 public void PlayIndex(int i){if(library==null||i<0||i>=library.songs.Count)return;index=i;StopAllCoroutines();StartCoroutine(LoadAndPlay(library.songs[i].path));}
 IEnumerator LoadAndPlay(string path){
   output.Stop();
   string url=path.StartsWith("content:")||path.StartsWith("file:")||path.StartsWith("http")?path:"file://"+path;
   AudioType type=path.EndsWith(".wav",StringComparison.OrdinalIgnoreCase)?AudioType.WAV:path.EndsWith(".ogg",StringComparison.OrdinalIgnoreCase)?AudioType.OGGVORBIS:AudioType.MPEG;
   using(var req=UnityWebRequestMultimedia.GetAudioClip(url,type)){
     yield return req.SendWebRequest();
     if(req.result!=UnityWebRequest.Result.Success){Debug.LogError("Audio nao carregado: "+req.error+" "+url);yield break;}
     output.clip=DownloadHandlerAudioClip.GetContent(req);output.Play();OnSongChanged?.Invoke();
   }
 }
 public void Next(){if(library==null||library.songs.Count==0)return;int next=index+1;if(next>=library.songs.Count){if(!repeatAll)return;next=0;}PlayIndex(next);}
 public void Previous(){if(library==null||library.songs.Count==0)return;PlayIndex((index-1+library.songs.Count)%library.songs.Count);}
 void Update(){if(output.clip!=null&&!output.isPlaying&&output.time>=output.clip.length-.1f)Next();}
 public float Energy(){
   if(!output.isPlaying)return 0;
   float[] samples=new float[256];output.GetOutputData(samples,0);
   float sum=0;foreach(float s in samples)sum+=s*s;
   return Mathf.Clamp01(Mathf.Sqrt(sum/samples.Length)*8);
 }
}