using UnityEngine;

public class AutoScale : MonoBehaviour
{
    [SerializeField]
    private float defaultHeight = 1.8f;

    private void Resize()
    {
        float headHeight = transform.localPosition.y;
        float scale = defaultHeight / headHeight;
        transform.localScale = Vector3.one * scale;
    }
    
    void OnEnable()
    {
        Resize();
    }
}
