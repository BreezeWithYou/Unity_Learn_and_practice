using JL_Animation_Playable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WeaponAnim : AnimBehaviour
{
    private float timer;
    public float TimeToPlay;

    private Mixer m_mixer;
    private PlayableGraph m_grapha;
    private AnimSelector m_Selector;
    private AnimUnit m_Default;


    public WeaponAnim(PlayableGraph graph, AnimationClip[] animationClips, float timeToPlay = 10f, float enterTime = 0f) : base(graph, enterTime)
    {
        m_grapha = graph;
        TimeToPlay = timeToPlay;
        timer = timeToPlay;

        m_Selector = new AnimSelector(m_grapha, enterTime);

        m_Default = new AnimUnit(m_grapha, animationClips[0], enterTime);

        for (int i = 1; i < animationClips.Length; i++)
        {
            m_Selector.AddInput(animationClips[i], enterTime);
        }
        m_mixer = new Mixer(m_grapha);

        Playable playable = m_mixer.GetAdaptrtPlayable();
        m_adapterPlayable.AddInput(playable, 0, 1f);

        m_mixer.AddInput(m_Default);
        m_mixer.AddInput(m_Selector);
    }

    public override void Enable()
    {
        base.Enable();
        m_adapterPlayable.SetTime(0f);
        m_adapterPlayable.Play();
        m_mixer.Enable();
    }

    public override void Excute(Playable playable, FrameData info)
    {
        base.Excute(playable, info);
        timer -= info.deltaTime;
        if (timer <= 0)
        {
            timer = TimeToPlay;
            m_Selector.Select();
            m_mixer.TransitionTo(1);
        }

        if (m_Selector.remainTime == 0f && !m_mixer.isTransition && m_mixer.currentIndex != 0)
        {
            m_mixer.TransitionTo(0);
        }
    }

    public override void Disable()
    {
        base.Disable();
        m_mixer.Disable();
        m_adapterPlayable.Pause();
        m_Selector.Disable();
        m_Default.Disable();
    }
}
