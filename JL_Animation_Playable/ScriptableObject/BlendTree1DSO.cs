using UnityEngine;

namespace JL_Animation_Playable
{
    [CreateAssetMenu(fileName = "BlendTree1D",menuName = "JL_Animation_Playable/BlendTree1D")]
    public class BlendTree1DSO : AnimSO
    {
        private void OnEnable()
        {
            AnimType = AnimTypeEnum.BlendTree1D;
        }
        public BlendClip1D[] BlendClip1DClips;
    }
}
