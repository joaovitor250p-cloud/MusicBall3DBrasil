using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Android;
#endif

[Serializable] public class Song { public string title; public string path; public Song(string t,string p){title=t;path=p;} }
public class MusicLibrary : MonoBehaviour {
    public readonly List<Song> songs=new List<Song>();
    public event Action Changed;
    string SavePath => Path.Combine(Application.persistentDataPath,"playlist.json");
    [Serializable] class SaveData { public List<Song> items=new List<Song>(); }
    void Awake(){ Load(); }
    public void Load(){
        songs.Clear();
        if(File.Exists(SavePath)){
            try { var data=JsonUtility.FromJson<SaveData>(File.ReadAllText(SavePath)); if(data!=null && data.items!=null) songs.AddRange(data.items); }catch(Exception e){Debug.LogWarning(e);}
        }
        Changed?.Invoke();
    }
    public void Save(){File.WriteAllText(SavePath,JsonUtility.ToJson(new SaveData{items=new List<Song>(songs)},true));Changed?.Invoke();}
    public void Add(string title,string path){if(songs.Exists(s=>s.path==path))return;songs.Add(new Song(title,path));Save();}
    public void Remove(int index){if(index<0||index>=songs.Count)return;songs.RemoveAt(index);Save();}
    public void Move(int index,int direction){int target=index+direction;if(index<0||target<0||target>=songs.Count)return;var s=songs[index];songs[index]=songs[target];songs[target]=s;Save();}
    public void ScanPhone(){
#if UNITY_ANDROID && !UNITY_EDITOR
        if(!Permission.HasUserAuthorizedPermission("android.permission.READ_MEDIA_AUDIO")){
            Permission.RequestUserPermission("android.permission.READ_MEDIA_AUDIO");return;
        }
        try {
            using(var player=new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using(var activity=player.GetStatic<AndroidJavaObject>("currentActivity"))
            using(var resolver=activity.Call<AndroidJavaObject>("getContentResolver"))
            using(var media=new AndroidJavaClass("android.provider.MediaStore$Audio$Media")){
                var uri=media.GetStatic<AndroidJavaObject>("EXTERNAL_CONTENT_URI");
                string[] projection={"_id","title"};
                using(var cursor=resolver.Call<AndroidJavaObject>("query",uri,projection,null,null,"title ASC")){
                    if(cursor!=null) while(cursor.Call<bool>("moveToNext")){
                        long id=cursor.Call<long>("getLong",0);
                        string title=cursor.Call<string>("getString",1);
                        Add(title,"content://media/external/audio/media/"+id);
                    }
                }
            }
        }catch(Exception e){Debug.LogError("Falha ao ler musicas: "+e);}
#else
        Debug.Log("No Editor: importe arquivos .mp3/.wav/.ogg em StreamingAssets/Music.");
        string dir=Path.Combine(Application.streamingAssetsPath,"Music");
        if(Directory.Exists(dir))foreach(string p in Directory.GetFiles(dir))if(p.EndsWith(".mp3")||p.EndsWith(".wav")||p.EndsWith(".ogg"))Add(Path.GetFileNameWithoutExtension(p),p);
#endif
    }
}