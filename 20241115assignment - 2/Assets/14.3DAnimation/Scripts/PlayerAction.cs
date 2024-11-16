using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;
using Unity.VisualScripting;
namespace MyProject
{

    [RequireComponent(typeof(Animator))]
    public class PlayerAction : MonoBehaviour
    {
        Animator animator;
        Rig rig;
        private YieldInstruction untilclip;
        public AnimationClip reloadClip;
        private WaitUntil untilReload;

        public AnimationClip FireClip;
        private WaitUntil untilFire;

        public AnimationClip GrenadeClip;
        private WaitUntil untilGrande;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rig = GetComponent<RigBuilder>().layers[0].rig;

        }

        private void Start()
        {
            untilReload = new WaitUntil(() => isReloading);
            untilFire = new WaitUntil(() => isFireing);
            untilGrande = new WaitUntil(() => isGrenading);

            StartCoroutine(ReloadCorutine());
            StartCoroutine(FireCorutine());
            StartCoroutine(GrenadeCorutine());
        }

        private bool isReloading = false;   
        private bool isFireing = false;
        private bool isGrenading = false;
        private void Update()
        {
            if (false == isReloading && Input.GetKeyDown(KeyCode.R))
            {
                //재장전
                rig.weight = 0f;
                isReloading = true;
                animator.SetTrigger("Reload");
            }
            if(false == isFireing && Input.GetKeyDown(KeyCode.T))
            {
                rig.weight = 0f;
                isFireing = true;
                animator.SetTrigger("Hit");
            }
            if(false == isGrenading && Input.GetKeyDown(KeyCode.H))
            {
                rig.weight = 0f;
                isGrenading = true;
                animator.SetTrigger("Grenade");
            }
        }

        /*public void OnReloadEnd() //외부에서 제작된 FBx 내자 애니메이션 이벤트도 추가할 수 있는데
        {
            rig.weight = 1f; //문제는 Animation Rig는 에니메에션 이벤트로 weight를 조정 불가능
            isReloading = false;
        }*/


        IEnumerator ReloadCorutine()
        {
            while (true)
            {
                yield return untilReload;
                yield return new WaitForSeconds(reloadClip.length);
                isReloading = false;
                rig.weight = 1f;
            }
        }
        IEnumerator FireCorutine()
        {
            while(true)
            {
                yield return untilFire;
                yield return new WaitForSeconds(FireClip.length);
                isFireing = false;
                rig.weight = 1f;
            }
        }

        IEnumerator GrenadeCorutine()
        {
            while(true)
            {
                yield return untilGrande;
                yield return new WaitForSeconds(GrenadeClip.length);
                isGrenading = false;
                rig.weight = 1f;
            }
        }
    }

}
