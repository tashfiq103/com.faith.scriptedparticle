
namespace com.faithstudio.ScriptedParticle {

    using UnityEngine;
    using System.Collections;

    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleController : MonoBehaviour
    {

        #region Public Variables

        [HideInInspector]
        public ParticleSystem particleSystemReference;

        [HideInInspector]
        public PSMainModule mainModuleReference;
        [HideInInspector]
        public PSEmissionModule emissionModuleReference;
        [HideInInspector]
        public PSVelocityOverLifeTimeModule velocityOverLifeTimeModuleReference;
        [HideInInspector]
        public PSSizeOverLifeTimeModule sizeOverLifeTimeModuleReference;
        #endregion

        #region Private Variables

        private bool m_IsStopingParticleWhenNotAlive;
        private bool m_IsForceStart;

        #endregion

        #region MonoBehaviour Callback

        private void Awake()
        {
            Initilization();
        }

        #endregion

        #region Configuretion

        private IEnumerator ControllerForStopParticle() {

            WaitForSeconds t_CycleDelay = new WaitForSeconds(0.25f);

            ParticleSystem.EmissionModule t_EmissionModule = particleSystemReference.emission;
            t_EmissionModule.enabled = false;

            //Debug.Log(gameObject.name + " (Before) : " + particleSystemReference.particleCount);
            while (particleSystemReference.particleCount > 0) {

                //Debug.Log(gameObject.name + " (Running) : " + particleSystemReference.particleCount);
                yield return t_CycleDelay;

                if (m_IsForceStart)
                    break;
            }
            //Debug.Log(gameObject.name + " (After) : " + particleSystemReference.particleCount);

            if (m_IsForceStart)
            {
                t_EmissionModule.enabled = true;
                RestoreToDefault();

                particleSystemReference.Play();
            }
            else {

                particleSystemReference.Stop();
                t_EmissionModule.enabled = true;

                RestoreToDefault();

                if (gameObject.activeInHierarchy)
                    gameObject.SetActive(false);
                
            }

            m_IsForceStart = false;
            m_IsStopingParticleWhenNotAlive = false;
            StopCoroutine(ControllerForStopParticle());
            
        }

        #endregion

        #region Public Callback

        public void PlayParticle() {

            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
            
            if (particleSystemReference.isPlaying && m_IsStopingParticleWhenNotAlive) {

                m_IsForceStart = true;
            }
            else if (!particleSystemReference.isPlaying)
            {
                particleSystemReference.Play();
            }
        }

        public void StopParticle(bool t_StopImmidieate) {
            
            if (particleSystemReference.isPlaying) {

                if (t_StopImmidieate)
                {
                    particleSystemReference.Stop();
                    RestoreToDefault();
                }
                else {
                    if (!m_IsStopingParticleWhenNotAlive)
                    {

                        m_IsForceStart = false;
                        m_IsStopingParticleWhenNotAlive = true;
                        StartCoroutine(ControllerForStopParticle());
                    }
                    else {
                        //Debug.LogWarning(gameObject.name + "DAFAQ");
                        //particleSystemReference.Stop();
                        //RestoreToDefault();
                    }
                }
                
            }
        }

        public void Initilization()
        {

            if (particleSystemReference == null)
                particleSystemReference = gameObject.GetComponent<ParticleSystem>();
            
            if (mainModuleReference != null)
                mainModuleReference.PreProcess(particleSystemReference);

            if (emissionModuleReference != null)
                emissionModuleReference.PreProcess(particleSystemReference);

            if (velocityOverLifeTimeModuleReference != null)
                velocityOverLifeTimeModuleReference.PreProcess(particleSystemReference);

            if (sizeOverLifeTimeModuleReference != null)
                sizeOverLifeTimeModuleReference.PreProcess(particleSystemReference);
        }

        public void RestoreToDefault() {

            if (mainModuleReference != null)
                mainModuleReference.RestoreToDefault();

            if (emissionModuleReference != null)
                emissionModuleReference.RestoreToDefault();

            if (velocityOverLifeTimeModuleReference != null)
                velocityOverLifeTimeModuleReference.RestoreToDefault();

            if (sizeOverLifeTimeModuleReference != null)
                sizeOverLifeTimeModuleReference.RestoreToDefault();
        }

        public void LerpOnPreset(float t_RateOfChange)
        {
            
            if (mainModuleReference != null)
                mainModuleReference.LerpOnPreset(t_RateOfChange);

            if (emissionModuleReference != null)
                emissionModuleReference.LerpOnPreset(t_RateOfChange);

            if (velocityOverLifeTimeModuleReference != null)
                velocityOverLifeTimeModuleReference.LerpOnPreset(t_RateOfChange);

            if (sizeOverLifeTimeModuleReference != null)
                sizeOverLifeTimeModuleReference.LerpOnPreset(t_RateOfChange);
        }

        public void LerpOnDefault(float t_RateOfChange) {

            if (mainModuleReference != null)
                mainModuleReference.LerpOnDefault(t_RateOfChange);

            if (emissionModuleReference != null)
                emissionModuleReference.LerpOnDefault(t_RateOfChange);

            if (velocityOverLifeTimeModuleReference != null)
                velocityOverLifeTimeModuleReference.LerpOnDefault(t_RateOfChange);

            if (sizeOverLifeTimeModuleReference != null)
                sizeOverLifeTimeModuleReference.LerpOnDefault(t_RateOfChange);
        }

        #endregion
    }
}


