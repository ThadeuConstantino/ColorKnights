using UnityEngine;

namespace GranGames.Utils
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(this);

            if (GameObject.Find(gameObject.name)
                     && GameObject.Find(gameObject.name) != this.gameObject)
            {
                Destroy(GameObject.Find(gameObject.name));
            }

        }

    }
}