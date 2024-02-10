using System;
using Player;
using TMPro;
using UnityEngine;

namespace Environment
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private ParticleSystem boxParticle;
        [SerializeField] private Transform message;
        [SerializeField] private Transform textObject;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.CompareTo("Instrument") == Decimal.Zero)
            {
                print($"Ball Enter box name: {gameObject.name}");
                var playerPosition = MainPlayer.Instance.transform.position;
                message.rotation = Quaternion.LookRotation(new Vector3(playerPosition.x,
                    transform.position.y, playerPosition.z) - transform.position, Vector3.up);
                _animator.SetTrigger("Blast");
                other.GetComponent<InstrumentBase>().EnterTheBox();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.CompareTo("Instrument") == Decimal.Zero)
            {
                other.GetComponent<InstrumentBase>().ExitTheBox();
            }
        }
    }
}