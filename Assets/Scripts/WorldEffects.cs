using UnityEngine;
public class WorldEffects : MonoBehaviour {
 public MusicWorld world;
 public BeatTracker tracker;
 public Light pulseLight;
 void Start(){
  var lightObject=new GameObject("Luz de pulso");
  pulseLight=lightObject.AddComponent<Light>();
  pulseLight.type=LightType.Point;pulseLight.range=14;pulseLight.intensity=1;
  pulseLight.color=new Color(.12f,.65f,1f);
 }
 void Update(){
  if(world==null)return;
  pulseLight.transform.position=world.BallPosition+Vector3.up*2;
  float energy=tracker!=null?tracker.energy:0;
  pulseLight.intensity=Mathf.Lerp(pulseLight.intensity,1+energy*5,Time.deltaTime*8);
  pulseLight.color=Color.HSVToRGB(Mathf.Repeat(Time.time*.04f+energy*.2f,1),.75f,1);
 }
}