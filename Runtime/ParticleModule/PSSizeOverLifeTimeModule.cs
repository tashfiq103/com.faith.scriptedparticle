namespace com.faithstudio.ScriptedParticle
{

    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class PSSizeOverLifeTimeModule : MonoBehaviour
    {

        #region Public Variables

        [HideInInspector]
        public bool manupulateSizeOverLifeTime;
        [HideInInspector]
        public float maxChangeOfSizeOverLifeTime;

#if UNITY_EDITOR
        [HideInInspector]
        public bool debugPanel;
#endif

        #endregion

        #region Private Variables

        private ParticleSystem m_ParticleSystemReference;

        private ParticleSystem.SizeOverLifetimeModule m_SizeOverLifeTimeModuleReference;

        private float t_DefaultSizeOverLifeTime;
        private float t_DefaultSizeOverLifeTimeOnX;
        private float t_DefaultSizeOverLifeTimeOnY;
        private float t_DefaultSizeOverLifeTimeOnZ;

        private float t_DefaultMaxSizeOverLifeTime;
        private float t_DefaultMaxSizeOverLifeTimeOnX;
        private float t_DefaultMaxSizeOverLifeTimeOnY;
        private float t_DefaultMaxSizeOverLifeTimeOnZ;

        private bool m_IsAnimationRunning;
        private float m_AnimationTime;
        #endregion

        #region MonoBehaviour Callback

        private void Awake()
        {
            
        }

        #endregion

        #region Public Callback


        public void PreProcess(ParticleSystem t_ParticleSystemReference)
        {

            m_ParticleSystemReference = t_ParticleSystemReference;

            Initialize();
        }

        public void Initialize()
        {
            m_SizeOverLifeTimeModuleReference = m_ParticleSystemReference.sizeOverLifetime;

            //MainModule : StartLifeTime
            t_DefaultSizeOverLifeTime       = m_SizeOverLifeTimeModuleReference.sizeMultiplier;
            t_DefaultMaxSizeOverLifeTime    = t_DefaultSizeOverLifeTime * maxChangeOfSizeOverLifeTime;

            t_DefaultSizeOverLifeTimeOnX    = m_SizeOverLifeTimeModuleReference.xMultiplier;
            t_DefaultMaxSizeOverLifeTimeOnX = t_DefaultSizeOverLifeTimeOnX * maxChangeOfSizeOverLifeTime;

            t_DefaultSizeOverLifeTimeOnY = m_SizeOverLifeTimeModuleReference.xMultiplier;
            t_DefaultMaxSizeOverLifeTimeOnY = t_DefaultSizeOverLifeTimeOnY * maxChangeOfSizeOverLifeTime;

            t_DefaultSizeOverLifeTimeOnZ = m_SizeOverLifeTimeModuleReference.xMultiplier;
            t_DefaultMaxSizeOverLifeTimeOnZ = t_DefaultSizeOverLifeTimeOnZ * maxChangeOfSizeOverLifeTime;

        }

        public void RestoreToDefault()
        {

            m_SizeOverLifeTimeModuleReference.sizeMultiplier = t_DefaultSizeOverLifeTime;

            m_SizeOverLifeTimeModuleReference.xMultiplier = t_DefaultSizeOverLifeTimeOnX;
            m_SizeOverLifeTimeModuleReference.yMultiplier = t_DefaultSizeOverLifeTimeOnY;
            m_SizeOverLifeTimeModuleReference.zMultiplier = t_DefaultSizeOverLifeTimeOnZ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_RateOfChange"> the value must be betweek '0' to '1' </param>
        public void LerpOnPreset(float t_RateOfChange)
        {

            if (t_RateOfChange > 1)
                t_RateOfChange = 1;

            if(!m_IsAnimationRunning)
                LerpOnValues(
                    t_RateOfChange,
                    t_DefaultSizeOverLifeTime, 
                    t_DefaultSizeOverLifeTimeOnX, 
                    t_DefaultSizeOverLifeTimeOnY, 
                    t_DefaultSizeOverLifeTimeOnZ,
                    t_DefaultMaxSizeOverLifeTime, 
                    t_DefaultMaxSizeOverLifeTimeOnX, 
                    t_DefaultMaxSizeOverLifeTimeOnY, 
                    t_DefaultMaxSizeOverLifeTimeOnZ);
            else
                Debug.LogWarning("Cannot lerp to default : Animation already running");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_RateOfChange"> the value must be betweek '0' to '1' </param>
        public void LerpOnDefault(float t_RateOfChange)
        {

            if (t_RateOfChange > 1)
                t_RateOfChange = 1;

            if (!m_IsAnimationRunning)
                LerpOnValues(
                    t_RateOfChange,
                    t_DefaultMaxSizeOverLifeTime,
                    t_DefaultMaxSizeOverLifeTimeOnX,
                    t_DefaultMaxSizeOverLifeTimeOnY,
                    t_DefaultMaxSizeOverLifeTimeOnZ,
                    t_DefaultSizeOverLifeTime, 
                    t_DefaultSizeOverLifeTimeOnX, 
                    t_DefaultSizeOverLifeTimeOnY, 
                    t_DefaultSizeOverLifeTimeOnZ);
            else
                Debug.LogWarning("Cannot lerp to default : Animation already running");
        }
        

        public void StopAnimation() {

            m_IsAnimationRunning = false;
        }

        #endregion

        #region Configuretion

        private void LerpOnValues(
            float t_RateOfChange,
            float InitialSizeMultiplier,
            float InitialSizeMultiplierOnX,
            float InitialSizeMultiplierOnY,
            float InitialSizeMultiplierOnZ,
            float t_TargetedSizeMultiplier, 
            float t_TargetedSizeMultiplierOnX, 
            float t_TargetedSizeMultiplierOnY, 
            float t_TargetedSizeMultiplierOnZ)
        {

            if (manupulateSizeOverLifeTime)
            {

                m_SizeOverLifeTimeModuleReference.sizeMultiplier= Mathf.Lerp(InitialSizeMultiplier, t_TargetedSizeMultiplier, t_RateOfChange);
                m_SizeOverLifeTimeModuleReference.xMultiplier   = Mathf.Lerp(InitialSizeMultiplierOnX, t_TargetedSizeMultiplierOnX, t_RateOfChange);
                m_SizeOverLifeTimeModuleReference.yMultiplier   = Mathf.Lerp(InitialSizeMultiplierOnY, t_TargetedSizeMultiplierOnY, t_RateOfChange);
                m_SizeOverLifeTimeModuleReference.zMultiplier   = Mathf.Lerp(InitialSizeMultiplierOnZ, t_TargetedSizeMultiplierOnZ, t_RateOfChange);
                
            }
        }

        private void LerpOnValuesForAnimation() {


        }
        

        #endregion

    }
}