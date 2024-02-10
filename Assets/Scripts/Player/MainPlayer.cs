using System;
using UnityEngine;

namespace Player
{
    public class MainPlayer : MonoBehaviour
    {
        public static MainPlayer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}