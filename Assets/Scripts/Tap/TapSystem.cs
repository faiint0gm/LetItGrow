using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Tap
{
    public class TapSystem : MonoBehaviour
    {
        [SerializeField]
        private int dewsPerSecond = 0;
        [SerializeField]
        private int dewsPoolAmount = 0;
        [SerializeField]
        private Dew dewPrefab;
        [SerializeField]
        private Transform sourceTap;
        [SerializeField]
        private Transform dewsPoolObject;

        private List<Dew> dewsPool = new List<Dew>();
        private List<Dew> drippedDews = new List<Dew>();

        public void SetupTap(int dewsPerSecond, int dewsPoolAmount)
        {
            ResetDews();
            this.dewsPerSecond = dewsPerSecond;
            this.dewsPoolAmount = dewsPoolAmount;
            GenerateDews();
        }

        void ResetDews()
        {
            foreach(Dew d in drippedDews)
            {
                if (d != null)
                {
                    Destroy(d.gameObject);
                }
            }

            foreach (Dew d in dewsPool)
            {
                if (d != null)
                {
                    Destroy(d.gameObject);
                }
            }
            dewsPool.Clear();
            drippedDews.Clear();
        }

        public void GenerateDews()
        {
            for (int i = 0; i < dewsPoolAmount; i++)
            {
                GameObject go = Instantiate(dewPrefab.gameObject, dewsPoolObject);
                go.transform.position = RandomizeStartPosition();
                go.GetComponent<Dew>().Setup(GameManager.Instance.DewHPRecoveryAmount);
                dewsPool.Add(go.GetComponent<Dew>());
            }
        }

        void DripNextDew()
        {
            if (dewsPool.Count > 0)
            {
                Dew nextDew = dewsPool.LastOrDefault();
                nextDew.GetComponent<Rigidbody>().velocity = Vector3.zero;
                nextDew.transform.position = sourceTap.position;
                drippedDews.Add(nextDew);
                dewsPool.Remove(nextDew);
            }
        }

        public void ReturnDewToPool(Dew dew)
        {
            dew.gameObject.transform.position = RandomizeStartPosition();
            drippedDews.Remove(dew);
            dewsPool.Add(dew);
        }

        private IEnumerator DewsDispenser()
        {
            while (GameManager.Instance.GetCanvasSystem.GetTimer.GetTimeLeft > 0)
            {
                DripNextDew();
                yield return new WaitForSeconds(timeToDrip());
            }
        }

        public void StartDispenseDews()
        {
            StopAllCoroutines();
            StartCoroutine(DewsDispenser());
        }
        private float timeToDrip()
        {
            return 1.0f / dewsPerSecond;
        }

        public void StopDispensing()
        {
            ResetDews();
            StopCoroutine(DewsDispenser());
        }
        private Vector3 RandomizeStartPosition()
        {
            return new Vector3(dewsPoolObject.position.x + Random.Range(-1, 1),
                                dewsPoolObject.position.y + Random.Range(-1, 1),
                                dewsPoolObject.position.z + Random.Range(-1, 1));
        }
    }
}
