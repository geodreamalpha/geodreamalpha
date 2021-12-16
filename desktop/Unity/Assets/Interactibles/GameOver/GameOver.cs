using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UserStats;

namespace UserGameOver
{
    public class GameOver : MonoBehaviour
    {
        Stats stats;

        private void Reset()
        {
            if (GetComponent<Stats>() == null)
                gameObject.AddComponent<Stats>();
        }

        // Start is called before the first frame update
        void Start()
        {
            stats = GetComponent<Stats>();
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (stats.health <= 0)
                Debug.Log("Gameover");
        }
    }
}