using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour,IDragHandler
{
    [SerializeField] GameData gameData;
    [SerializeField] Transform character;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    float minX2 = -1.2f;
    float maxX2= 1.0f;

    [SerializeField] GameManager gameManager;

    private void FixedUpdate()
    {
        if (gameManager.isGameStart == true && gameManager.isGameOver == false)
        {
            MoveForward();
        }
    }
    private void Update()
    {
        if (gameManager.isGameStart == false && !IsPointerOverUI())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsPointerOverUI())
                {
                    gameManager.Play();
                }
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = character.position;
        if (SaveManager.Instance.ClearLevel < 10)
        {
            pos.x = Mathf.Clamp(pos.x + (eventData.delta.x / 100) * gameData.DragSpeed, minX, maxX);
        }
        else
        {
            if (PlateLevelController.isRunnerStarted == false)
            {
                pos.x = Mathf.Clamp(pos.x + (eventData.delta.x / 100) * gameData.DragSpeed, minX2, maxX2);
            }
            else
            {
                pos.x = Mathf.Clamp(pos.x + (eventData.delta.x / 100) * gameData.DragSpeed, minX, maxX);
            }
        }
        character.position = pos;
    }

    void MoveForward()
    {
        if (PlateLevelController.isRunnerStarted == false)
        {
            character.Translate(Vector3.forward * Time.deltaTime * gameData.ForwardSpeed);
        }
        else
        {
            character.Translate(Vector3.forward * Time.deltaTime * gameData.RunnerForwardSpeed);
        }
    }
    private bool IsPointerOverUI()
    {     
        EventSystem eventSystem = EventSystem.current;
        
        if (eventSystem == null || eventSystem.currentSelectedGameObject == null)
        {
            return false;
        }

        GameObject selectedUI = eventSystem.currentSelectedGameObject;
        return selectedUI.GetComponentInParent<Canvas>() != null;
    }

}
