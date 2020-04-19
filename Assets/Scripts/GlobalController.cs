using UnityEngine;

namespace Assets.Scripts
{
    public class GlobalController : MonoBehaviour
    {
        public static GlobalController Instance;

        public int EnemiesKilled;
        public int ScrapCollected;

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
