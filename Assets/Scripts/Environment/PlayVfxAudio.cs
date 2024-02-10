using System;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Environment
{
    public class PlayVfxAudio : VFXOutputEventAbstractHandler
    {
        public override bool canExecuteInEditor { get; }
        [SerializeField] private AudioSource _audioSource;
        
        
        public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
        {
            if (_audioSource != null)
            {
                _audioSource.Play();
            }
        }

        
    }
}