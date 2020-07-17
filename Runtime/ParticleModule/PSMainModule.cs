namespace com.faithstudio.ScriptedParticle
{

    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class PSMainModule : MonoBehaviour
    {

        #region Public Variables

        [HideInInspector]
        public bool manupulateStartLifeTime;
        [HideInInspector]
        public float maxChangeOfStartLifeTime;

#if UNITY_EDITOR
        [HideInInspector]
        public bool debugPanel;
#endif

        #endregion

        #region Private Variables

        private ParticleSystem m_ParticleSystemReference;

        private ParticleSystem.MainModule m_MainModuleReference;

        private ParticleSystem.MinMaxCurve t_DefaultStartLifeTime;
        private ParticleSystem.MinMaxCurve t_DefaultMaxStartLifeTime;

        private ParticleSystem.MinMaxCurve m_ModifiedMinMaxCurve;

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
            m_MainModuleReference = m_ParticleSystemReference.main;

            //MainModule : StartLifeTime
            t_DefaultStartLifeTime = m_MainModuleReference.startLifetime;

            t_DefaultMaxStartLifeTime.constant = t_DefaultStartLifeTime.constant * maxChangeOfStartLifeTime;
            t_DefaultMaxStartLifeTime.constantMin = t_DefaultStartLifeTime.constantMin * maxChangeOfStartLifeTime;
            t_DefaultMaxStartLifeTime.constantMax = t_DefaultStartLifeTime.constantMax * maxChangeOfStartLifeTime;
            t_DefaultMaxStartLifeTime.curveMultiplier = t_DefaultStartLifeTime.curveMultiplier * maxChangeOfStartLifeTime;
        }

        public void RestoreToDefault()
        {

            m_MainModuleReference.startLifetime = t_DefaultStartLifeTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_RateOfChange"> the value must be betweek '0' to '1' </param>
        public void LerpOnPreset(float t_RateOfChange)
        {

            if (t_RateOfChange > 1)
                t_RateOfChange = 1;

            LerpOnValues(t_RateOfChange, t_DefaultStartLifeTime, t_DefaultMaxStartLifeTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_RateOfChange"> the value must be betweek '0' to '1' </param>
        public void LerpOnDefault(float t_RateOfChange)
        {
            if (t_RateOfChange > 1)
                t_RateOfChange = 1;

            LerpOnValues(t_RateOfChange, t_DefaultMaxStartLifeTime, t_DefaultStartLifeTime);
        }

        #endregion

        #region Configuretion

        private void LerpOnValues(float t_RateOfChange, ParticleSystem.MinMaxCurve t_DefaultMinMaxCurve, ParticleSystem.MinMaxCurve t_TargetedMinMaxCurve)
        {
            
            if (manupulateStartLifeTime)
            {
                m_ModifiedMinMaxCurve = m_MainModuleReference.startLifetime;

                m_ModifiedMinMaxCurve.constant = Mathf.Lerp(t_DefaultMinMaxCurve.constant, t_TargetedMinMaxCurve.constant, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMin = Mathf.Lerp(t_DefaultMinMaxCurve.constantMin, t_TargetedMinMaxCurve.constantMin, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMax = Mathf.Lerp(t_DefaultMinMaxCurve.constantMax, t_TargetedMinMaxCurve.constantMax, t_RateOfChange);
                m_ModifiedMinMaxCurve.curveMultiplier = Mathf.Lerp(t_DefaultMinMaxCurve.curveMultiplier, t_TargetedMinMaxCurve.curveMultiplier, t_RateOfChange);

                m_MainModuleReference.startLifetime = m_ModifiedMinMaxCurve;
            }
        }

        #endregion

    }
}