using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Level Settings")]
    public LevelData currentLevelData;
    public Transform objectParent; // 장애물의 부모 객체
    private List<GameObject> activeObjects = new List<GameObject>();
    private bool resetting;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadLevel()
    {
        this.LoadLevel(currentLevelData);
    }
        public void LoadLevel(LevelData levelData)
    {
        ResetLevel(); // 기존 레벨 초기화
        currentLevelData = levelData;

        resetting = false;

        // 새로운 레벨 로드
        foreach (var objSettings in currentLevelData.objects)
        {
            GameObject obj = ObjectPool.Instance.GetObject(objSettings.prefab);
            Objects objScript = obj.GetComponent<Objects>();

            if (objScript != null)
            {
                objScript.Initialize(
                    objSettings.startPosition,
                    objSettings.endPosition,
                    objSettings.rotation,
                    currentLevelData.globalspeed,
                    currentLevelData.globalDelay,
                    objSettings.speedAmplifier,
                    objSettings.delay
                );
                objScript.StartMoving();
            }

            obj.transform.SetParent(objectParent, false);
            activeObjects.Add(obj);
        }
    }
    public void StopAllObjects()
    {
        foreach (GameObject obj in activeObjects)
        {
            Objects objScript = obj.GetComponent<Objects>();
            if (objScript != null)
            {
                objScript.Stop();
            }
        }
    }

    public void ResetLevel()
    {
        foreach (GameObject obj in activeObjects)
        {
            if (obj == null) continue;

            Objects objScript = obj.GetComponent<Objects>();
            if (objScript != null)
            {
                objScript.ResetObject();
            }

            ObjectPool.Instance.ReturnObject(obj);
        }

        activeObjects.Clear();
    }

    public void CompleteLevel()
    {
        Debug.Log("Level Completed!");

        if (currentLevelData != null && currentLevelData.nextLevel != null)
        {
            LoadLevel(currentLevelData.nextLevel); // 다음 레벨 로드
        }
        else
        {
            Debug.Log("No more levels! Game complete.");
            // 게임 종료 또는 첫 레벨로 돌아가기
            // LoadLevel(firstLevelData);
        }
    }
}
