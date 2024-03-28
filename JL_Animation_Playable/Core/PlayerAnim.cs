using JL_Animation_Playable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAnim : MonoBehaviour
{

    private JLAnimController m_animController;
    public PlayerAnimSO InitFile;
    public PlayerAnimDatas playerAnimDatas;

    [Header("Test字段")]
    public string Tname;
    public SingleAnimClipSO Clips;
    public ComboAssetsSO Combo;
    public void Init(FSMController fsmContorller)
    {
        m_animController = new JLAnimController(this, fsmContorller.animator, fsmContorller.Modle);
        ResourceLoadInit();
        m_animController.AddAnimator("Animator", 0.2f);
        m_animController.Start();
    }

    private void OnDestroy()
    {
        m_animController.Stop();
    }

    public void TransitionTo(string name, Action callback = null, float enterTIme = -1f, float animationClipLength = -1f)
    {
        m_animController.TransitionTo(name, callback, enterTIme, animationClipLength);
    }
    public void TransitionTo(int name, Action callback = null, float enterTIme = -1f, float animationClipLength = -1f)
    {
        m_animController.TransitionTo(name);
    }
    public void AnimatorCroosFade(string stateName, float transitionDuration, Action callback = null)
    {
        m_animController.AnimatorCroosFade(stateName, transitionDuration, callback);
    }
    public void BlendTree1DSetValue(string name, float value)
    {
        m_animController.BlendTree1DSetValue(name, value);
    }
    public float BlendTree1DGetValue(string name)
    {
        return m_animController.BlendTree1DGetValue(name);
    }
    public void BlendTree2DSetValue(string name, Vector2 value)
    {
        m_animController.BlendClipTree2DSetPointer(name, value);
    }

    

    #region Resource 资源加载模块
    // -------------------------------------------------

    private void ResourceLoadInit()
    {
        for (int i = 0; i < InitFile.AnimDatas.singleAnimClips?.Count; i++)
        {
            ResourceLoadAnimUnit(InitFile.AnimDatas.singleAnimClips[i]);
        }
        for (int i = 0; i < InitFile.AnimDatas.blendTree1Ds?.Count; i++)
        {
            ResourceLoadBlendTree1D(InitFile.AnimDatas.blendTree1Ds[i]);
        }
        for (int i = 0; i < InitFile.AnimDatas.blendTree2Ds?.Count; i++)
        {
            ResourceLoadBlendTree2D(InitFile.AnimDatas.blendTree2Ds[i]);
        }
        ResourceLoadComboAssets(InitFile.AnimDatas.WeaponDatas);
    }

    // --------------------------------------------------
    public void ResourceLoadComboAssets(ComboAssetsSO comba, float time = 0.1f)
    {
        // 资源检查---------------------
        if (playerAnimDatas.WeaponDatas != null)
        {
            for (int i = 0; i < playerAnimDatas.WeaponDatas.comboAssets?.Count; i++)
            {
                m_animController.RemoveAnimUnit(playerAnimDatas.WeaponDatas.comboAssets[i].comboName);
            }
        }
        playerAnimDatas.WeaponDatas = comba;
        for (int i = 0; i < playerAnimDatas.WeaponDatas.comboAssets?.Count; i++)
        {
            m_animController.AddAnimUnit(playerAnimDatas.WeaponDatas.comboAssets[i].comboName, playerAnimDatas.WeaponDatas.comboAssets[i].comboAnim.AnimationClip, 0.1f);
        }
    }

    public void ResourceLoadBlendTree2D(BlendTree2DSO blendTree, float time = 0.1f)
    {
        // 资源检查---------------------
        if (blendTree == null)
            return;
        foreach (var so in playerAnimDatas.blendTree2Ds)
        {
            if (so == blendTree)
                return;
        }
        playerAnimDatas.blendTree2Ds.Add(blendTree);
        m_animController.AddBlendTree2D(blendTree.name, blendTree.BlendClip2DClips, 0.1f);
    }

    public void ResourceUninstallBlendTree2D(BlendTree2DSO blendTree)
    {
        // 资源检查---------------------
        if (m_animController == null)
            return;
        for (int i = 0; i < playerAnimDatas.blendTree2Ds?.Count; i++)
        {
            if (playerAnimDatas.blendTree2Ds[i].name == blendTree.name)
                m_animController.RemoveBlendTree2D(blendTree.name);
        }
        playerAnimDatas.blendTree2Ds.Remove(blendTree);
    }

    public void ResourceLoadBlendTree1D(BlendTree1DSO blendTree, float time = 0.1f)
    {
        // 资源检查---------------------
        if (blendTree == null)
            return;
        foreach (var so in playerAnimDatas.blendTree1Ds)
        {
            if (so == blendTree)
                return;
        }
        playerAnimDatas.blendTree1Ds.Add(blendTree);
        m_animController.AddBlendTree1D(blendTree.name, blendTree.BlendClip1DClips, 0.1f);
    }

    public void ResourceUninstallBlendTree1D(BlendTree1DSO blendTree)
    {
        // 资源检查---------------------
        if ( m_animController == null)
            return;
        for (int i = 0; i < playerAnimDatas.blendTree1Ds?.Count; i++)
        {
            if (playerAnimDatas.blendTree1Ds[i].name == blendTree.name)
                m_animController.RemoveBlendTree1D(blendTree.name);
        }
        playerAnimDatas.blendTree1Ds.Remove(blendTree);
    }

    public void ResourceLoadAnimUnit(SingleAnimClipSO singleAnimClipSO, float time = 0.1f)
    {
        // 资源检查---------------------
        if (singleAnimClipSO == null)
            return;
        foreach (var so in playerAnimDatas.singleAnimClips)
        {
            if (so == singleAnimClipSO)
                return;
        }
        playerAnimDatas.singleAnimClips.Add(singleAnimClipSO);
        m_animController.AddAnimUnit(singleAnimClipSO.name, singleAnimClipSO.AnimationClip, time);
    }

    public void ResourceUninstallAnimUnit(SingleAnimClipSO singleAnimClipSO)
    {
        // 资源检查---------------------
        if (m_animController == null)
            return;
        for (int i = 0; i < playerAnimDatas.singleAnimClips?.Count; i++)
        {
            if (playerAnimDatas.singleAnimClips[i].name == singleAnimClipSO.name)
                m_animController.RemoveAnimUnit(singleAnimClipSO.name);
        }
        playerAnimDatas.singleAnimClips.Remove(singleAnimClipSO);
    }

    #endregion

    #region 脚本测试模块
    // -----------------脚本 Test------------------------
    [ContextMenu("DebugAniamtion")]
    public void DebugAniamtion()
    {
        m_animController.DebugAnimation();
    }
    [ContextMenu("RemoveAniamtion")]
    public void RemoveAniamtion()
    {
        ResourceUninstallAnimUnit(Clips);
    }
    [ContextMenu("TestAniamtion")]
    public void TestAniamtion()
    {
        TransitionTo("Attack_01");
    }
    [ContextMenu("ChangeAniamtion")]
    public void ChangeAniamtion()
    {
        ResourceLoadComboAssets(Combo);
    }
    // --------------------------------------------------
    #endregion
}