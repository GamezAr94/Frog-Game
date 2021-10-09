using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField][Range(0f,3f)]
    float destroyValue = 3f; 
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyValue);
    }
}
