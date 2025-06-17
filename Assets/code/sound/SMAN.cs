using UnityEngine;

namespace SoundManager{
  public class sman{
    public static void StopSound(AudioSource source){
      source.Stop();
    }

    #nullable enable
    // Returns so sound can be stopped if needed
    public static void PlaySound(AudioClip clip,  out AudioSource? sou, AudioSource? source=null, float volume=1f){
      if(source != null){
        source.PlayOneShot(clip, volume);
        sou = source;
      }
      else{
        AudioSource s = new();
        s.PlayOneShot(clip, volume);
        sou = s;
      }
    }
    #nullable disable
  }
}
