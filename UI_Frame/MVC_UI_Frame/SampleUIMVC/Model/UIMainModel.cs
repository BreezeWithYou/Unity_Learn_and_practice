using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIMainModel
{
    private static UIMainModel _playerData;
    public event UnityAction<UIMainModel> DataEventHandler;
    public static UIMainModel Data
    {
        get
        {
            if (_playerData == null)
            {
                _playerData = new UIMainModel();
                _playerData.Init();
            }
            return _playerData;
        }
    }

    private int _playerLev;
    public int PlayerLev => _playerLev;

    private void Init()
    {
        //举个例子
        _playerLev = PlayerPrefs.GetInt("_playerLev", 10);
    }
    private void SaveData()
    {
        //举个例子
        PlayerPrefs.SetInt("_playerLev", _playerLev);
        OnUpdateInfo(_playerData);
    }

    public void LevUp()
    {
        _playerLev += 1;
        SaveData();
    }
    public void AddListener(UnityAction<UIMainModel> func)
    {
        DataEventHandler += func;
    }

    private void OnUpdateInfo(UIMainModel playerModel)
    {
        if (DataEventHandler != null)
        {
            DataEventHandler(playerModel);
        }
    }
}
