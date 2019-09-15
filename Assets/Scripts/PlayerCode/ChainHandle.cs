using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerCode
{
    public class ChainHandle : MonoBehaviour
    {
        [SerializeField]
        VerletChain chainSystem;

        string handleTag;
        string elementTag;
        string enemyTag;

        private void Start()
        {
            handleTag = gameObject.tag;
            elementTag = chainSystem.chainPrefab.tag;
            enemyTag = handleTag == "HandleOne" ? "TentacleTwo" : "TentacleOne";
        }
    }
}
