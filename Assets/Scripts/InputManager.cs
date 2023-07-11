using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Camera cameraObj;
    [SerializeField] Drawing drawing;

    private bool hasСhanges = false;


    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            InputCalculations();

            if (Input.GetTouch(0).phase == TouchPhase.Ended && hasСhanges)
                drawing.SpriteChangeEnd();
        }
    }

    private void InputCalculations()
    {
        Ray ray = cameraObj.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);

        if (hit.collider)
            if (hit.collider.tag == "panel")
            {
                drawing.Draw(hit.point);
                hasСhanges = true;
            }
    }
}
