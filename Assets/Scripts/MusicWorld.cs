using UnityEngine;
using System.Collections.Generic;
public class MusicWorld : MonoBehaviour {
    public AudioSource music;
    Transform ball;
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
        music=gameObject.AddComponent<AudioSource>();
        music.playOnAwake=false;
    }
    void Update() {
        float speed=music && music.isPlaying ? 5f + Mathf.Clamp(music.volume*2,0,2) : 3f;
        travel=Mathf.Repeat(travel+Time.deltaTime*speed,245f);
        float x=Mathf.Sin(travel*.09f)*2;
        ball.position=new Vector3(x,.2f+Mathf.Abs(Mathf.Sin(travel*1.8f))*.25f,travel);
        ball.Rotate(Vector3.right,Time.deltaTime*420);
        if(Camera.main) {
            Vector3 target=ball.position+new Vector3(0,4,-9);
            Camera.main.transform.position=Vector3.Lerp(Camera.main.transform.position,target,Time.deltaTime*4);
            Camera.main.transform.LookAt(ball.position+Vector3.forward*5);
        }
    }
}