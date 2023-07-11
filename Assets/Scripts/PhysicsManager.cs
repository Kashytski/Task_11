using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] PolygonCollider2D colliderPolygon;
    [SerializeField] Drawing drawing;

    void Awake()
    {
        drawing.changingSpriteAction += ResetColider;
    }

    void OnDestroy()
    {
        drawing.changingSpriteAction -= ResetColider;
    }

    private void ResetColider()
    {
        Destroy(colliderPolygon);
        colliderPolygon = gameObject.AddComponent<PolygonCollider2D>();
    }
}
