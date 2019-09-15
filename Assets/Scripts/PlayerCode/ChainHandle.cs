using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.PlayerCode
{
    public class ChainHandle : MonoBehaviour
    {
        [SerializeField]
        Player player;
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

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.CompareTag(enemyTag))
            {
                GameManager.Instance.GetSoundManager.PlayOneShot(0, player.beatClip);
                GetEnemy().SetInterruptInput(true);
                GetEnemy().GetHit();
                GetEnemy().SetInterruptInput(false);
            }
        }

        Player GetEnemy()
        {
            if(enemyTag == "TentacleOne")
            {
                return GameManager.Instance.GetPlayer(Enums.PlayerType.PlayerOne);
            }
            else if (enemyTag == "TentacleTwo")
            {
                return GameManager.Instance.GetPlayer(Enums.PlayerType.PlayerTwo);
            }
            else
            {
                return null;
            }
        }
    }
}
