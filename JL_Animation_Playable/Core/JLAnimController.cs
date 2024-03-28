using JL_Animation_Playable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class JLAnimController
{
    private PlayableGraph m_graph;
    public PlayableGraph Graph => m_graph;
    private Mixer m_mixer;
    public Animator m_animator;

    private Dictionary<string, int> m_animMap2Int;
    private Dictionary<string, AnimBehaviour> m_animMap;
    private Dictionary<string, BlendTree1D> m_blendTree1DMap;
    private Dictionary<string, BlendTree2D> m_blendTree2DMap;

    private AnimMixAnimator m_animMixAnimator;

    private Coroutine m_animationCroosFadeCallback;
    private Coroutine m_TransitionToCallback;


    // [Obsolete]private Rigidbody m_body;
    // private CharacterController m_characterController;

    private Transform m_modle;

    private MonoBehaviour m_owner;

    public JLAnimController(MonoBehaviour owner, Animator animator, Transform modle)
    {
        m_owner = owner;

        m_graph = PlayableGraph.Create();
        m_mixer = new Mixer(m_graph);
        m_animMap2Int = new Dictionary<string, int>();

        m_animMap = new Dictionary<string, AnimBehaviour>();
        m_blendTree1DMap = new Dictionary<string, BlendTree1D>();
        m_blendTree2DMap = new Dictionary<string, BlendTree2D>();

        m_animator = animator;
        m_modle = modle;
    }

    public void Start()
    {
        AnimHelper.SetOutput(m_graph, m_animator, m_mixer);
        AnimHelper.Start(m_graph);
    }

    public void Stop()
    {
        m_graph.Destroy();
    }

    #region 过渡

    public void TransitionTo(string name, Action callback = null, float enterTime = -1f, float animationClipLength = -1f)
    {
        if (enterTime != -1f)
            SetEnterTime(name, enterTime);

        TransitionTo(name);
        if (m_TransitionToCallback != null)
            m_owner.StopCoroutine(m_TransitionToCallback);
        if (m_animationCroosFadeCallback != null)
            m_owner.StopCoroutine(m_animationCroosFadeCallback);
        if (callback != null)
        {
            if (animationClipLength != -1f)
                m_TransitionToCallback = m_owner.StartCoroutine(TransitionToEnumerator(name, animationClipLength, callback));
            else
                m_TransitionToCallback = m_owner.StartCoroutine(TransitionToEnumerator(name, GetAnimLength(name), callback));
        }
    }



    private void SetEnterTime(string name, float enterTIme)
    {
        m_animMap[name].SetEnterTime(enterTIme);
    }

    private void TransitionTo(string name)
    {
        // if (m_animMap2Int.ContainsKey(name))
            m_mixer.TransitionTo(m_animMap2Int[name]);
        // else
        //  Debug.LogWarning("不存在该动画片段"+name);
    }
    public void TransitionTo(int name, Action callback = null, float enterTIme = -1f, float animationClipLength = -1f)
    {
        m_mixer.TransitionTo(name);
    }


    private void TransitionTo(int name)
    {
        m_mixer.TransitionTo(name);
    }

    public void CheakDir(string name)
    {
        if (m_animMap2Int.ContainsKey(name))
        {
            Debug.Log("存在");
        }
        else
        {
            Debug.Log("不存在");
        }
    }
    private IEnumerator TransitionToEnumerator(string name, float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
        yield break;
    }

    #endregion 过渡

    #region 添加删除动画节点

    //添加Animator
    public void AddAnimator(string name, float enterTIme)
    {
        m_animMixAnimator = new AnimMixAnimator(m_graph, m_animator, 0.2f);
        AddState(name, m_animMixAnimator);
    }

    //添加BlendTree1D
    public void AddBlendTree1D(string name, BlendClip1D[] blendTree1DClips, float enterTime)
    {
        BlendTree1D blendTree1D = new BlendTree1D(m_graph, blendTree1DClips, 0.2f);
        m_blendTree1DMap.Add(name, blendTree1D);
        AddState(name, blendTree1D);
    }
    // 移除BlendTree1D
    public void RemoveBlendTree1D(string name)
    {
        m_blendTree2DMap.Remove(name);
        RemoveState(name);
    }

    //添加BlendTree2D
    public void AddBlendTree2D(string name, BlendClip2D[] blendTree2DClips, float enterTime)
    {
        BlendTree2D blendTree2D = new BlendTree2D(m_graph, blendTree2DClips, 0.2f);
        m_blendTree2DMap.Add(name, blendTree2D);
        AddState(name, blendTree2D);
    }

    // 移除BlendTree2D
    public void RemoveBlendTree2D(string name)
    {
        m_blendTree2DMap.Remove(name);
        RemoveState(name);
    }

    //添加单个动画
    public void AddAnimUnit(string name, AnimationClip animationClip, float enterTime)
    {
        if (!m_animMap2Int.TryGetValue(name, out int index))
        {
            var anim = new AnimUnit(m_graph, animationClip, enterTime);
            AddState(name, anim);
        }
    }
    // 删除单个动画
    public void RemoveAnimUnit(string name)
    {
        if (m_animMap2Int.TryGetValue(name, out int index))
        {
            RemoveState(name);
        }
    }

    // 向根混合器添加状态
    public void AddState(string name, AnimBehaviour behaviour)
    {
        m_mixer.AddInput(behaviour);
        m_animMap2Int.Add(name, m_mixer.inputCount - 1);
        m_animMap.Add(name, behaviour);
    }

    // 向根混合器/删除状态
    public void RemoveState(string name)
    {
        m_mixer.RemoveInput(m_animMap2Int[name]);
        m_animMap2Int.Remove(name);
        m_animMap.Remove(name);
    }

    #endregion 添加动画节点

    #region BlendTree

    public void BlendTree1DSetValue(string name, float value)
    {
        if (m_blendTree1DMap.TryGetValue(name, out BlendTree1D blendTree1D))
        {
            blendTree1D.SetValue(value);
        }
    }

    public float BlendTree1DGetValue(string name)
    {
        if (m_blendTree1DMap.TryGetValue(name, out BlendTree1D blendTree1D))
        {
            return blendTree1D.CurrentValue;
        }
        return 0f;
    }

    public void BlendClipTree2DSetPointer(string name, Vector2 value)
    {
        if (m_blendTree2DMap.TryGetValue(name, out BlendTree2D blendTree2D))
        {
            blendTree2D.SetPointer(value);
        }
    }

    #endregion BlendTree


    #region AnimatonMixer

    public void AnimatorCroosFade(string stateName, float transitionDuration, Action callback = null)
    {
        TransitionTo("Animator");
        m_animMixAnimator.CrossFade(stateName, transitionDuration);
        if (m_TransitionToCallback != null)
            m_owner.StopCoroutine(m_TransitionToCallback);
        if (m_animationCroosFadeCallback != null)
            m_owner.StopCoroutine(m_animationCroosFadeCallback);
        if (callback != null)
        {
            m_animationCroosFadeCallback = m_owner.StartCoroutine(AnimationCroosFadeEnumerator(m_animMixAnimator.GetAnimLength(0), callback));
        }
    }

    private IEnumerator AnimationCroosFadeEnumerator(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    public void AnimatorSetFloat(string name, float value)
    { m_animMixAnimator.SetFloat(name, value); }

    public void AnimatorSetBool(string name, bool value)
    { m_animMixAnimator.SetBool(name, value); }

    public void AnimatorSetTrigger(string name)
    { m_animMixAnimator.SetTrigger(name); }

    public float AnimatorGetAnimLength(int layer)
    { return m_animMixAnimator.GetAnimLength(layer); }

    #endregion AnimatonMixer

    //获取当前动画播放长度
    public float GetAnimLength(string name)
    {
        if (m_animMap.TryGetValue(name, out AnimBehaviour anim))
        {
            return anim.GetAnimLength();
        }
        return 0;
    }

    //----------------------功能 Test 脚本---------------------------------
    public void DebugAnimation()
    {
        foreach(var dir in m_animMap2Int)
        {
            Debug.Log(dir.Key+" "+dir.Value);
        }
        foreach (var dir in m_animMap)
        {
            Debug.Log(dir.Key + " " + dir.Value);
        }
    }


    public void RemoveAnimation(string name)
    {
        RemoveAnimUnit(name);
        Debug.Log("已经成功移除对应动画");
    }
    //---------------------------------------------------------------------
}
