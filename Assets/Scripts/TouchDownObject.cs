using UnityEngine;

public class TouchDownObject : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    private void OnMouseDown()
    {
        if (gameObjects != null)
        {
            gameObjects[0].SetActive(false);
            gameObjects[1].SetActive(true);
        }
    }
}
