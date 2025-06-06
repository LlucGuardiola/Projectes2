using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    private static MusicTransition instance;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
