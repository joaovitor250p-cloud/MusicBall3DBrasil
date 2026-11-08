using UnityEngine;
public class InstrumentSynth : MonoBehaviour {
 AudioSource source;
 AudioClip[] clips;
 void Awake(){
  source=gameObject.AddComponent<AudioSource>();source.playOnAwake=false;source.spatialBlend=.65f;
  clips=new AudioClip[4];
  for(int n=0;n<clips.Length;n++){
   int length=11025;float[] data=new float[length];float freq=new float[]{220,330,440,550}[n];
   for(int i=0;i<length;i++){
    float t=i/44100f;float envelope=Mathf.Exp(-t*(12+n*3));
    data[i]=Mathf.Sin(2*Mathf.PI*freq*t)*envelope*.5f+Mathf.Sin(2*Mathf.PI*freq*2.01f*t)*envelope*.12f;
   }
   var clip=AudioClip.Create("Metal "+n,length,1,44100,false);clip.SetData(data,0);clips[n]=clip;
  }
 }
 public void Strike(int note,float strength){if(source!=null)source.PlayOneShot(clips[Mathf.Abs(note)%clips.Length],Mathf.Clamp01(strength));}
}