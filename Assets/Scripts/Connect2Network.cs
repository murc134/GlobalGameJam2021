using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NetworkManager))]
    public class Connect2Network : MonoBehaviour
    {
        public string serverAddress;

        NetworkManager manager;
        void Awake()
        {
            manager = GetComponent<NetworkManager>();
            manager.networkAddress = serverAddress;
        }
        // Start is called before the first frame update

        void Start()
        {
            if (!NetworkServer.active && !string.IsNullOrEmpty(manager.networkAddress))
                StartCoroutine(TryConnect());
        }

        IEnumerator TryConnect()
        {
            if (NetworkClient.active) yield break;

            yield return new WaitForSeconds(0.5f);

            manager.StartClient();
        }

        private void OnDisable()
        {
            manager.StopClient();
        }
    }
}
