using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> _waves;
    [SerializeField] int _waveNumber = 0;
    [SerializeField] EntityManager _entitiesManager;

    private IEnumerator<Wave> _waveIterator;
    private IEnumerator<Entity> _entitiesIterator;

    private Wave _currentWave;

    bool _isWaveDurationCompleted = false;
    bool _areEntitiesDead = false;

    private const float MIN_TIME_BTW_ENTITIES = 1f;

    private IEnumerator Start() 
    {
        yield return new WaitForSecondsRealtime(2f);
        StartFirstWave();
    }   

    //public Wave CurrentWave { get => _currentWave; }

    [ContextMenu("Start First Wave")]
    public void StartFirstWave()
    {
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
            //GameManager.Instance.Win();
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

    private IEnumerator COR_Wave(Wave waveData)
    {
        Debug.Log("Wave Started!");
        _entitiesIterator = waveData.Entities.GetEnumerator();

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

    private IEnumerator COR_WaitForWaveDuration(float duration)
    {
        _isWaveDurationCompleted = false;
        yield return new WaitForSeconds(duration);
        _isWaveDurationCompleted = true;

    }

    private IEnumerator COR_WaitForWaveCleanUp()
    {
        _areEntitiesDead = false;
        while (_entitiesManager.noEntities == false)
        {
            Debug.Log("Check!");
            yield return new WaitForSeconds(1f);
        }
        _areEntitiesDead = true;

    }
}
