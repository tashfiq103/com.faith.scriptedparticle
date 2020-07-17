
namespace com.faithstudio.ScriptedParticle
{

    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class PSVelocityOverLifeTimeModule : MonoBehaviour
    {

        #region Public Variables

#if UNITY_EDITOR
        [HideInInspector]
        public bool debugPanel;
#endif

        [HideInInspector]
        public bool manupulateLinearVelocity;
        [HideInInspector]
        public float maxChangeOfLinearVelocity;
        
        #endregion

        #region Private Variables

        private ParticleSystem m_ParticleSystemReference;

        private ParticleSystem.VelocityOverLifetimeModule m_VelocityOverLifetimeModuleReference;

        private ParticleSystem.MinMaxCurve t_DefaultLinearVelocityOnX;
        private ParticleSystem.MinMaxCurve t_DefaultLinearVelocityOnY;
        private ParticleSystem.MinMaxCurve t_DefaultLinearVelocityOnZ;

        private ParticleSystem.MinMaxCurve t_DefaultMaxLinearVelocityOnX;
        private ParticleSystem.MinMaxCurve t_DefaultMaxLinearVelocityOnY;
        private ParticleSystem.MinMaxCurve t_DefaultMaxLinearVelocityOnZ;

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
            m_VelocityOverLifetimeModuleReference = m_ParticleSystemReference.velocityOverLifetime;

            //VelocityOverLifeTime : LinearX
            t_DefaultLinearVelocityOnX = m_VelocityOverLifetimeModuleReference.x;

            t_DefaultMaxLinearVelocityOnX.constant = t_DefaultLinearVelocityOnX.constant * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnX.constantMin = t_DefaultLinearVelocityOnX.constantMin * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnX.constantMax = t_DefaultLinearVelocityOnX.constantMax * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnX.curveMultiplier = t_DefaultLinearVelocityOnX.curveMultiplier * maxChangeOfLinearVelocity;

            //VelocityOverLifeTime : LinearY
            t_DefaultLinearVelocityOnY = m_VelocityOverLifetimeModuleReference.y;

            t_DefaultMaxLinearVelocityOnY.constant = t_DefaultLinearVelocityOnY.constant * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnY.constantMin = t_DefaultLinearVelocityOnY.constantMin * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnY.constantMax = t_DefaultLinearVelocityOnY.constantMax * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnY.curveMultiplier = t_DefaultLinearVelocityOnY.curveMultiplier * maxChangeOfLinearVelocity;

            //VelocityOverLifeTime : LinearZ
            t_DefaultLinearVelocityOnZ = m_VelocityOverLifetimeModuleReference.z;

            t_DefaultMaxLinearVelocityOnZ.constant = t_DefaultLinearVelocityOnZ.constant * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnZ.constantMin = t_DefaultLinearVelocityOnZ.constantMin * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnZ.constantMax = t_DefaultLinearVelocityOnZ.constantMax * maxChangeOfLinearVelocity;
            t_DefaultMaxLinearVelocityOnZ.curveMultiplier = t_DefaultLinearVelocityOnZ.curveMultiplier * maxChangeOfLinearVelocity;

        }

        public void RestoreToDefault()
        {

            m_VelocityOverLifetimeModuleReference.x = t_DefaultLinearVelocityOnX;
            m_VelocityOverLifetimeModuleReference.y = t_DefaultLinearVelocityOnY;
            m_VelocityOverLifetimeModuleReference.z = t_DefaultLinearVelocityOnZ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_RateOfChange"> the value must be betweek '0' to '1' </param>
        public void LerpOnPreset(float t_RateOfChange)
        {

            if (t_RateOfChange > 1)
                t_RateOfChange = 1;

            LerpOnValues(
                t_RateOfChange,
                t_DefaultLinearVelocityOnX,
                t_DefaultLinearVelocityOnY,
                t_DefaultLinearVelocityOnZ,
                t_DefaultMaxLinearVelocityOnX, 
                t_DefaultMaxLinearVelocityOnY, 
                t_DefaultMaxLinearVelocityOnZ);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_RateOfChange"> the value must be betweek '0' to '1' </param>
        public void LerpOnDefault(float t_RateOfChange)
        {

            if (t_RateOfChange > 1)
                t_RateOfChange = 1;

            LerpOnValues(t_RateOfChange,
                t_DefaultMaxLinearVelocityOnX,
                t_DefaultMaxLinearVelocityOnY,
                t_DefaultMaxLinearVelocityOnZ,
                t_DefaultLinearVelocityOnX, 
                t_DefaultLinearVelocityOnY, 
                t_DefaultLinearVelocityOnZ);
        }

        #endregion

        #region Configuretion

        private void LerpOnValues(
            float t_RateOfChange,
            ParticleSystem.MinMaxCurve t_InitialMinMaxCurveOnX,
            ParticleSystem.MinMaxCurve t_InitialMinMaxCurveOnY,
            ParticleSystem.MinMaxCurve t_InitialMinMaxCurveOnZ,
            ParticleSystem.MinMaxCurve t_TargetedMinMaxCurveOnX, 
            ParticleSystem.MinMaxCurve t_TargetedMinMaxCurveOnY, 
            ParticleSystem.MinMaxCurve t_TargetedMinMaxCurveOnZ)
        {

            if (manupulateLinearVelocity)
            {
                //VelocityOverLifeTime : LinearX
                m_ModifiedMinMaxCurve = m_VelocityOverLifetimeModuleReference.x;

                m_ModifiedMinMaxCurve.constant = Mathf.Lerp(t_InitialMinMaxCurveOnX.constant, t_TargetedMinMaxCurveOnX.constant, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMin = Mathf.Lerp(t_InitialMinMaxCurveOnX.constantMin, t_TargetedMinMaxCurveOnX.constantMin, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMax = Mathf.Lerp(t_InitialMinMaxCurveOnX.constantMax, t_TargetedMinMaxCurveOnX.constantMax, t_RateOfChange);
                m_ModifiedMinMaxCurve.curveMultiplier = Mathf.Lerp(t_InitialMinMaxCurveOnX.curveMultiplier, t_TargetedMinMaxCurveOnX.curveMultiplier, t_RateOfChange);

                m_VelocityOverLifetimeModuleReference.x = m_ModifiedMinMaxCurve;

                //VelocityOverLifeTime : LinearY
                m_ModifiedMinMaxCurve = m_VelocityOverLifetimeModuleReference.y;

                m_ModifiedMinMaxCurve.constant = Mathf.Lerp(t_InitialMinMaxCurveOnY.constant, t_TargetedMinMaxCurveOnY.constant, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMin = Mathf.Lerp(t_InitialMinMaxCurveOnY.constantMin, t_TargetedMinMaxCurveOnY.constantMin, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMax = Mathf.Lerp(t_InitialMinMaxCurveOnY.constantMax, t_TargetedMinMaxCurveOnY.constantMax, t_RateOfChange);
                m_ModifiedMinMaxCurve.curveMultiplier = Mathf.Lerp(t_InitialMinMaxCurveOnY.curveMultiplier, t_TargetedMinMaxCurveOnY.curveMultiplier, t_RateOfChange);

                m_VelocityOverLifetimeModuleReference.y = m_ModifiedMinMaxCurve;

                //VelocityOverLifeTime : LinearZ
                m_ModifiedMinMaxCurve = m_VelocityOverLifetimeModuleReference.z;

                m_ModifiedMinMaxCurve.constant = Mathf.Lerp(t_InitialMinMaxCurveOnZ.constant, t_TargetedMinMaxCurveOnZ.constant, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMin = Mathf.Lerp(t_InitialMinMaxCurveOnZ.constantMin, t_TargetedMinMaxCurveOnZ.constantMin, t_RateOfChange);
                m_ModifiedMinMaxCurve.constantMax = Mathf.Lerp(t_InitialMinMaxCurveOnZ.constantMax, t_TargetedMinMaxCurveOnZ.constantMax, t_RateOfChange);
                m_ModifiedMinMaxCurve.curveMultiplier = Mathf.Lerp(t_InitialMinMaxCurveOnZ.curveMultiplier, t_TargetedMinMaxCurveOnZ.curveMultiplier, t_RateOfChange);

                m_VelocityOverLifetimeModuleReference.z = m_ModifiedMinMaxCurve;
            }
        }

        #endregion

    }
}
