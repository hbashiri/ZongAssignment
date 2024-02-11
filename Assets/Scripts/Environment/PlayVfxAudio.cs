using System;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Environment
{
    public class PlayVfxAudio : VFXOutputEventAbstractHandler
    {
        public override bool canExecuteInEditor { get; }
        [SerializeField] private AudioClip sfxAudioClip;
        [SerializeField] private AudioSource audioSource;
        
        
        public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(sfxAudioClip);
            }
        }

        
    }
}