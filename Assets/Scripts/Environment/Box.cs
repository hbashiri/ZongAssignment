using System;
using Player;
using TMPro;
using UnityEngine;

namespace Environment
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private Transform message;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.tag.CompareTo("Instrument") == Decimal.Zero)
            {
                OnInstrumentEnter(other.GetComponent<InstrumentBase>());
                
            }
        }

        protected virtual void OnInstrumentEnter(InstrumentBase instrument)
        {
            var playerPosition = MainPlayer.Instance.transform.position;
            message.rotation = Quaternion.LookRotation(new Vector3(playerPosition.x,
                transform.position.y, playerPosition.z) - transform.position, Vector3.up);
            _animator.SetTrigger("Blast");
            instrument.EnterTheBox();
        }
        
        protected void OnTriggerExit(Collider other)
        {
            if (other.tag.CompareTo("Instrument") == Decimal.Zero)
            {
                OnInstrumentExit(other.GetComponent<InstrumentBase>());
            }
        }

        protected virtual void OnInstrumentExit(InstrumentBase instrument)
        {
            instrument.ExitTheBox();
        }
    }
}