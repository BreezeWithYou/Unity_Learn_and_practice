using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMainView : MonoBehaviour
{
    /// 主面板View，View类主要负责处理显示内容，保证显示的实时更新。
    /// 所以View会有一个实际响应函数UpdateData。
    /// 为什么叫实际响应函数？
    /// 因为实际上Controller层的响应函数只是一个中间商，Controller响应函数会进一步分发事件。
    /// 

    //玩家等级
    public Text playerLev;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        // 这里举个例子
        playerLev.text = UIMainModel.Data.PlayerLev.ToString();
    }
    public void UpdateData(UIMainModel UIMainModel)
    {
        playerLev.text = UIMainModel.Data.PlayerLev.ToString();
    }


}
