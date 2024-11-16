using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

namespace MyProject
{
    [RequireComponent(typeof(Animator), typeof(RigBuilder))]
    public class InputSystemAction : MonoBehaviour
    {
        Animator animator;
        Rig rig;
        WaitUntil untilReload;
        bool isReloading;
        public AnimationClip ReloadClip;

        WaitUntil untilFire;
        bool isFireing;
        public AnimationClip FireingClip;

        WaitUntil untilGrenade;
        bool isGrenading;
        public AnimationClip GrenadeClip; 

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rig = GetComponent<RigBuilder>().layers[0].rig;
        }

        private void Start()
        {
            StartCoroutine(ReloadCorutine());
            StartCoroutine(FireCorutine());
            StartCoroutine(GrenadeCorutine());
        }

        private IEnumerator ReloadCorutine()
        {
            untilReload = new WaitUntil(() => isReloading);
            while (true)
            {
                yield return untilReload;
                yield return new WaitForSeconds(ReloadClip.length);
                isReloading = false;
                rig.weight = 1f;
            }
        }
        private IEnumerator FireCorutine()
        {
            untilFire = new WaitUntil(() => isFireing);
            while (true)
            {
                yield return untilFire;
                yield return new WaitForSeconds(FireingClip.length);
                isFireing = false;
                rig.weight = 1f;
            }
        }

        private IEnumerator GrenadeCorutine()
        {
            untilGrenade = new WaitUntil(() => isGrenading);
            while(true)
            {
                yield return untilGrenade;
                yield return new WaitForSeconds(GrenadeClip.length);
                isGrenading = false;
                rig.weight = 1f;
            }
        }

        public void OnReloadEvent(InputAction.CallbackContext context)
        {
            if (isReloading) return;
            if (context.performed)
            {
                rig.weight = 0f;
                isReloading = true;
                animator.SetTrigger("Reload");
            }
        }

        public void OnFireEvent(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (isFireing) return;
                rig.weight = 0f;
                isFireing = true;
                animator.SetTrigger("Hit");
            }
        }

        public void OnGrenadeEvent(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                if(!isGrenading) return;    
                rig.weight = 0f;
                isGrenading = true;
                animator.SetTrigger("Grenade");
            }
        }

        public void onReloadEnd()
        {
            print("onReloadEnd called by Animation Event");
        }

        private void OnReload(InputValue Value)
        {
            print($"OnReload »£√‚µ . isPressed : {Value.isPressed}, {Value.Get<Single>()}");
            if (isReloading) return;

            rig.weight = 0f;
            isReloading = true;
            animator.SetTrigger("Reload");
        }
        private void OnFire(InputValue Value)
        {
            if(isFireing) return;
            rig.weight = 0f;
            isFireing = true;
            animator.SetTrigger("Hit");
        }

        private void OnGrenade(InputValue Value)
        {
            if(isGrenading)return;  
            rig.weight = 0f;
            isGrenading = true;
            animator.SetTrigger("Grenade");
        }    
    }
}
