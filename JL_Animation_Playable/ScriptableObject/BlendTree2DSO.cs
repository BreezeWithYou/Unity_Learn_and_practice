using UnityEngine;

namespace JL_Animation_Playable
{

    [CreateAssetMenu(fileName = "BlendTree2D", menuName = "JL_Animation_Playable/BlendTree2D")]
    public class BlendTree2DSO : AnimSO
    {
        private void OnEnable()
        {
            AnimType = AnimTypeEnum.BlendTree2D;
        }
        public BlendClip2D[] BlendClip2DClips;
    }
}
