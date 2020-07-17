namespace com.faithstudio.ScriptedParticle
{

    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class PSEmissionModule : MonoBehaviour
    {

        #region Public Variables

        [HideInInspector]
        public float maxChangeOnRateOverTime;

        [HideInInspector]
        public bool manupulateRateOverTime;
        [HideInInspector]
        public bool manupulateRateOverDistance;

#if UNITY_EDITOR
        [HideInInspector]
        public bool debugPanel;
#endif

        #endregion

        #region Private Variables

        private ParticleSystem m_ParticleSystemReference;

        private ParticleSystem.EmissionModule m_EmissionModuleReference;

        private ParticleSystem.MinMaxCurve t_DefaultRateOverTime;
        private ParticleSystem.MinMaxCurve t_DefaultMaxRateOverTime;

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
        

        public void PreProcess(ParticleSystem t_ParticleSystemReference) {

            m_ParticleSystemReference = t_ParticleSystemReference;

            Initialize();
        }

        public void Initialize()
        {
            m_EmissionModuleReference = m_ParticleSystemReference.emission;

            //EmissionModule : RateOverTime
            t_DefaultRateOverTime = m_EmissionModuleReference.rateOverTime;

            t_DefaultMaxRateOverTime.constant = t_DefaultRateOverTime.constant * maxChangeOnRateOverTime;
            t_DefaultMaxRateOverTime.constantMin = t_DefaultRateOverTime.constantMin * maxChangeOnRateOverTime;
            t_DefaultMaxRateOverTime.constantMax = t_DefaultRateOverTime.constantMax * maxChangeOnRateOverTime;
            t_DefaultMaxRateOverTime.curveMultiplier = t_DefaultRateOverTime.curveMultiplier * maxChangeOnRateOverTime;
        }

        public void RestoreToDefault() {

            m_EmissionModuleReference.rateOverTime = t_DefaultRateOverTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_RateOfChange"> the value must be betweek '0' to '1' </param>
        public void LerpOnPreset(float t_RateOfChange) {

            if (t_RateOfChange > 1)
                t_RateOfChange = 1;

            LerpOnValues(
                t_RateOfChange,
                t_DefaultRateOverTime,
                t_DefaultMaxRateOverTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_RateOfChange"> the value must be betweek '0' to '1' </param>
        public void LerpOnDefault(float t_RateOfChange) {

            if (t_RateOfChange > 1)
                t_RateOfChange = 1;

            LerpOnValues(
                t_RateOfChange,
                t_DefaultMaxRateOverTime,
                t_DefaultRateOverTime);
        }

        #endregion

        #region Configuretion

        private void LerpOnValues(
            float t_RateOfChange,
            ParticleSystem.MinMaxCurve t_InitialMinMaxCurve,
            ParticleSystem.MinMaxCurve t_TargetedMinMaxCurve) {

            if (manupulateRateOverTime)
            {

                m_ModifiedMinMaxCurve = m_EmissionModuleReference.rateOverTime;

                m_ModifiedMinMaxCurve.constant = Mathf.Lerp(t_InitialMinMaxCurve.constant, t_TargetedMinMaxCurve.constant, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMin = Mathf.Lerp(t_InitialMinMaxCurve.constantMin, t_TargetedMinMaxCurve.constantMin, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMax = Mathf.Lerp(t_InitialMinMaxCurve.constantMax, t_TargetedMinMaxCurve.constantMax, t_RateOfChange);
                m_ModifiedMinMaxCurve.curveMultiplier = Mathf.Lerp(t_InitialMinMaxCurve.curveMultiplier, t_TargetedMinMaxCurve.curveMultiplier, t_RateOfChange);

                m_EmissionModuleReference.rateOverTime = m_ModifiedMinMaxCurve;
            }
        }

        #endregion

    }
}