using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    //面板路径加载
    public string UIResourcePath;
    //物体对象
    public GameObject UIPanel;
    //层级
    public UIPanelLayer layer;

    //初始化
    public virtual void Init() { }
    //开始面板前
    public virtual void OnShowing() { }
    //显示面板后
    public virtual void OnShowed() { }
    //帧更新
    public virtual void Update() { }
    //关闭前
    public virtual void OnClosing() { }
    //关闭后
    public virtual void OnClosed() { }
}