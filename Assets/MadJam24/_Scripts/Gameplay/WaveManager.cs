using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] PopupText[] _preWaveNarration;
    [SerializeField] List<Wave> _waves;
    [SerializeField] int _waveNumber = 0;
    [SerializeField] EntityManager _entitiesManager;
    [SerializeField] Wall _leftWall, _rightWall, _topWall, _bottomWall;


    private IEnumerator<Wave> _waveIterator;
    private IEnumerator<Entity> _entitiesIterator;

    private Wave _currentWave;

    bool _isWaveDurationCompleted = false;
    bool _areEntitiesDead = false;

    private const float MIN_TIME_BTW_ENTITIES = .2f;



    private IEnumerator Start() 
    {
        if(SessionVariablesTracker.Instance.HasPlayed == false)
            foreach (var popUpData in _preWaveNarration)
            {
                UIManager.Instance.DisplayPopUpText(popUpData);
                yield return new WaitForSecondsRealtime(popUpData.popupDuration +.1f);
            }

        StartFirstWave();
    }   


    [ContextMenu("Start First Wave")]
    public void StartFirstWave()
    {
        SessionVariablesTracker.Instance.HasPlayed = true;

        _waveNumber = 0;
        _waveIterator = _waves.GetEnumerator();
        StartNextWave();
    }

    public void ResetWaves()
    {
        StopAllCoroutines();
        _waveNumber = 0;
        _waveIterator = _waves.GetEnumerator();
    }

    private void StartNextWave()
    {
        _waveNumber++;
        ProcWaveStartActions(_waveNumber);
        StopAllCoroutines();

        if (_waveIterator.MoveNext())
        {
            _currentWave = _waveIterator.Current;
        }
        else
        {
            StartCoroutine(COR_WinGameOnCleanUp());
            return;
        }

        if (_currentWave)
            StartCoroutine(COR_Wave(_currentWave));
    }

    private void ProcWaveStartActions(int waveNumber)
    {

        Debug.Log("Wave: " + waveNumber + "/" + _waves.Count);
        // UIManager.instance.DisplayAlertMsg("Wave: " + waveNumber + "/" + _waves.Count, 2.5f);
    }
    private IEnumerator COR_DisplayAllWaveNarration(List<PopupText> popups)
    {
        foreach (var item in popups)
        {
            UIManager.Instance.DisplayPopUpText(item);
            yield return new WaitForSeconds(item.popupDuration);
        }
       
    }
    private IEnumerator COR_Wave(Wave waveData)
    {
        Debug.Log("Wave Started!");
        StartCoroutine(COR_DisplayAllWaveNarration(waveData.narrationStrings));
        UIManager.Instance.StartWaveDurationDisplay(waveData.durationInSeconds);
        _entitiesIterator = waveData.Entities.GetEnumerator();

        SetupWalls(waveData);

        while (_entitiesIterator.MoveNext())
        {
            var entity = _entitiesIterator.Current;
            _entitiesManager.InstantiateEntity(entity);
            yield return new WaitForSeconds(MIN_TIME_BTW_ENTITIES);
        }

        
         StartCoroutine(COR_WaitForWaveCleanUp());
         StartCoroutine(COR_WaitForWaveDuration(waveData.durationInSeconds));

        yield return new WaitUntil(()=> _areEntitiesDead || _isWaveDurationCompleted);
        yield return new WaitForSeconds(1f);

        Debug.Log("Wave Completed!");
        StartNextWave();
    }

    private void SetupWalls(Wave waveData)
    {
        _bottomWall.Setup(waveData.bottomConvexWall);
        _topWall.Setup(waveData.topConvexWall);
        _rightWall.Setup(waveData.rightConvexWall);
        _leftWall.Setup(waveData.leftConvexWall);
    }

    private IEnumerator COR_WaitForWaveDuration(float duration)
    {
        _isWaveDurationCompleted = false;
        yield return new WaitForSeconds(duration);
        _isWaveDurationCompleted = true;

    }

    private IEnumerator COR_WaitForWaveCleanUp()
    {
        _areEntitiesDead = false;
        while (_entitiesManager.AreAllEntitiesDead() == false)
        {
            Debug.Log("Check!");
            yield return new WaitForSeconds(1.5f);
        }
        _areEntitiesDead = true;
    }

    private IEnumerator COR_WinGameOnCleanUp()
    {
        yield return StartCoroutine(COR_WaitForWaveCleanUp());
        GameManager.Instance.WinGame();
    }
}
