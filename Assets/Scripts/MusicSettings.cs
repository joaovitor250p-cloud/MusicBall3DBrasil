using UnityEngine;
public enum VisualMode { Sincronizado, Infinito, Surpresa }
public class MusicSettings : MonoBehaviour {
 public VisualMode mode=VisualMode.Sincronizado;
 public float sensitivity=1f;
 public float masterVolume=.8f;
 public int seed=2026;
 public void NextMode(){mode=(VisualMode)(((int)mode+1)%3); }
 public void Apply(MusicPlayer player){if(player!=null&&player.output!=null)player.output.volume=masterVolume;}
}