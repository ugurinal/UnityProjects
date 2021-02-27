using UnityEngine;

namespace SpaceTraveler.Animations
{
    public class AnimationDestroyer : StateMachineBehaviour
    {
        /// <summary>
        /// this script is attached to animation states which sould be destroyed when the animation ends
        /// </summary>
        /// <param name="animator">Animator component</param>
        /// <param name="stateInfo">Animator State</param>
        /// <param name="layerIndex">Layer Index</param>
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Destroy(animator.gameObject, stateInfo.length);
        }
    }
}