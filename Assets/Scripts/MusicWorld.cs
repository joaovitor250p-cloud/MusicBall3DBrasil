using UnityEngine;
using System.Collections.Generic;
public class MusicWorld : MonoBehaviour {
    public AudioSource music;
    Transform ball;
    public Vector3 BallPosition => ball != null ? ball.position : Vector3.zero;
    MusicPlayer player;
    BeatTracker tracker;
    MusicSettings settings;
    InstrumentSynth synth;
    int lastPad=-1;
    readonly List<Transform> pads = new List<Transform>();
    float travel;
    void Start() {
        Camera cam = Camera.main;
        if (!cam) { var c = new GameObject("Camera"); cam = c.AddComponent<Camera>(); c.tag = "MainCamera"; }
        cam.backgroundColor = new Color(.025f,.025f,.07f);
        cam.clearFlags = CameraClearFlags.SolidColor;
        var light = new GameObject("Key Light").AddComponent<Light>();
        light.type = LightType.Directional; light.intensity = 1.5f;
        light.transform.rotation = Quaternion.Euler(35,-25,0);
        var orb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        orb.name = "Bolinha 3D"; orb.transform.localScale = Vector3.one*.75f; ball = orb.transform;
        orb.GetComponent<Renderer>().material.color = new Color(.1f,.8f,1f);
        for(int i=0;i<100;i++) {
            float z=i*2.5f;
            var rail=GameObject.CreatePrimitive(PrimitiveType.Cube);
            rail.name="Trilho"; rail.transform.position=new Vector3(Mathf.Sin(z*.09f)*2,-.65f,z);
            rail.transform.localScale=new Vector3(2.3f,.25f,2.55f);
            rail.GetComponent<Renderer>().material.color=new Color(.12f,.18f,.28f);
            if(i%4==0) {
                var pad=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                pad.name="Instrumento "+i; pad.transform.position=new Vector3(Mathf.Sin(z*.09f)*2,-.35f,z);
                pad.transform.localScale=new Vector3(.85f,.12f,.85f);
                pad.GetComponent<Renderer>().material.color=new Color(.85f,.55f,.12f);
                pads.Add(pad.transform);
            }
        }
        settings=FindFirstObjectByType<MusicSettings>();
        if(!settings)settings=new GameObject("Configuracoes").AddComponent<MusicSettings>();
        player=FindFirstObjectByType<MusicPlayer>();
        tracker=gameObject.AddComponent<BeatTracker>();tracker.player=player;
        synth=gameObject.AddComponent<InstrumentSynth>();
        var effects=gameObject.AddComponent<WorldEffects>();effects.world=this;effects.tracker=tracker;
    }
    void Update() {
        if(player==null)player=FindFirstObjectByType<MusicPlayer>();
        if(tracker!=null&&tracker.player==null)tracker.player=player;
        float energy=tracker!=null?tracker.energy:0;
        float speed=player!=null&&player.output!=null&&player.output.isPlaying?5f+energy*3f:3f;
        if(settings!=null&&settings.mode==VisualMode.Infinito)speed+=Mathf.Sin(Time.time*.7f)*.6f;
        if(settings!=null&&settings.mode==VisualMode.Surpresa)speed+=Mathf.Sin(Time.time*.3f)*1.2f;
        travel=Mathf.Repeat(travel+Time.deltaTime*speed,245f);
        float x=Mathf.Sin(travel*.09f)*2;
        ball.position=new Vector3(x,.2f+Mathf.Abs(Mathf.Sin(travel*1.8f))*.25f,travel);
        ball.Rotate(Vector3.right,Time.deltaTime*(420+energy*200));
        int padIndex=Mathf.RoundToInt(travel/10f);
        if(padIndex!=lastPad&&padIndex>=0&&padIndex<pads.Count&&Mathf.Abs(travel-padIndex*10f)<.7f){
            lastPad=padIndex;
            if(synth!=null)synth.Strike(padIndex,.4f+energy*.6f);
            pads[padIndex].localScale=new Vector3(1.1f,.18f,1.1f);
        }
        foreach(var pad in pads)if(pad!=null)pad.localScale=Vector3.Lerp(pad.localScale,new Vector3(.85f,.12f,.85f),Time.deltaTime*5);
        if(Camera.main) {
            Vector3 target=ball.position+new Vector3(0,4,-9);
            Camera.main.transform.position=Vector3.Lerp(Camera.main.transform.position,target,Time.deltaTime*4);
            Camera.main.transform.LookAt(ball.position+Vector3.forward*5);
        }
    }
}