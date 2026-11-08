using UnityEngine;
public class BeatTracker : MonoBehaviour {
 public MusicPlayer player;
 public float energy;
 public bool beat;
 public float bpmEstimate=120f;
 float average=.08f,lastBeat=-1f;
 void Update(){
  energy=player!=null?player.Energy():0f;
  average=Mathf.Lerp(average,energy,Time.deltaTime*2);
  beat=false;
  if(player!=null&&player.output!=null&&player.output.isPlaying&&energy>Mathf.Max(.08f,average*1.35f)&&Time.time-lastBeat>.18f){
   if(lastBeat>0){float interval=Time.time-lastBeat;bpmEstimate=Mathf.Lerp(bpmEstimate,Mathf.Clamp(60f/interval,60f,220f),.15f);}
   lastBeat=Time.time;beat=true;
  }
 }
}