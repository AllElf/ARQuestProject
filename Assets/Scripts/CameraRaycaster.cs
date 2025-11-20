using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class CameraRaycaster : MonoBehaviour
{
    public Camera mainCamera;
    public Text hitInfoText;
    public string ignoreTag = "MainIgnore"; // тег, который нужно игнорировать

    void Update()
    {
        CastRayFromCenter();
    }

    void CastRayFromCenter()
    {
        hitInfoText.text = "";

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        // Физика
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        float physicsDist = Mathf.Infinity;
        GameObject physicsObj = null;

        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.CompareTag(ignoreTag))
            {
                physicsDist = hit.distance;
                physicsObj = hit.collider.gameObject;
            }
        }

        // UI
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenCenter
        };

        float uiDist = Mathf.Infinity;
        GameObject uiObj = null;

        GraphicRaycaster[] raycasters = FindObjectsOfType<GraphicRaycaster>();
        foreach (GraphicRaycaster gr in raycasters)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(pointerData, results);

            foreach (RaycastResult r in results)
            {
                if (!r.gameObject.CompareTag(ignoreTag))
                {
                    uiDist = r.distance;
                    uiObj = r.gameObject;
                    break;
                }
            }
        }

        // Сравнение
        if (uiObj != null && uiDist <= physicsDist)
            hitInfoText.text = $"UI объект: {uiObj.name}, Тег: {uiObj.tag}";
        else if (physicsObj != null)
            hitInfoText.text = $"Физический объект: {physicsObj.name}, Тег: {physicsObj.tag}";
        else
            hitInfoText.text = "";
    }
}