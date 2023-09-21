using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    //���·������
    public string UIResourcePath;
    //�������
    public GameObject UIPanel;
    //�㼶
    public UIPanelLayer layer;

    //��ʼ��
    public virtual void Init() { }
    //��ʼ���ǰ
    public virtual void OnShowing() { }
    //��ʾ����
    public virtual void OnShowed() { }
    //֡����
    public virtual void Update() { }
    //�ر�ǰ
    public virtual void OnClosing() { }
    //�رպ�
    public virtual void OnClosed() { }
}