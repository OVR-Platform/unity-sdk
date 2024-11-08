using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OverSDK
{
    public class OvrControllableObject : MonoBehaviour
    {
        public bool IsControllable { get => isControllable; set => isControllable = value; }
        [Space][SerializeField] private bool isControllable = true;

    }
}
