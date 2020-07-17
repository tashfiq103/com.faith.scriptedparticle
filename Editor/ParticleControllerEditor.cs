
namespace com.faithstudio.ScriptedParticle
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(ParticleController))]
    public class ParticleControllerEditor : Editor
    {
        private ParticleController m_Reference;

        private void OnEnable()
        {
            m_Reference = (ParticleController)target;

            m_Reference.Initilization();

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CustomGUI();

            DrawDefaultInspector();

            serializedObject.ApplyModifiedProperties();
        }

        private void CustomGUI() {


            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("IsAlive : " + m_Reference.particleSystemReference.IsAlive().ToString());
                EditorGUILayout.LabelField("IsEmitting : " + m_Reference.particleSystemReference.isEmitting + ", IsPaused : " + m_Reference.particleSystemReference.isPaused);
                EditorGUILayout.LabelField("IsPlaying : " + m_Reference.particleSystemReference.isPlaying + ", IsStopped : " + m_Reference.particleSystemReference.isStopped);

            } EditorGUILayout.EndVertical();


            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("After Configuring Particle, make sure to click on the 'Initialize' button",MessageType.Warning);
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Initialize"))
                {
                    m_Reference.Initilization();
                }

                if (GUILayout.Button("Restore To Default"))
                {
                    m_Reference.RestoreToDefault();
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Lerp -> Default (0.05%)"))
                {
                    m_Reference.LerpOnDefault(0.05f);
                }

                if (GUILayout.Button("Lerp -> Max (0.05%)"))
                {
                    m_Reference.LerpOnPreset(0.05f);
                }
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.Space();
            //MainModule
            EditorGUILayout.BeginHorizontal();
            {

                EditorGUILayout.LabelField("Main                :   ");
                if (m_Reference.mainModuleReference == null)
                {

                    if (GUILayout.Button("Add"))
                    {

                        m_Reference.gameObject.AddComponent<PSMainModule>();
                        m_Reference.mainModuleReference = m_Reference.gameObject.GetComponent<PSMainModule>();
                        m_Reference.mainModuleReference.PreProcess(m_Reference.particleSystemReference);
                    }
                }
                else
                {

                    if (GUILayout.Button("Remove"))
                    {
                        DestroyImmediate(m_Reference.mainModuleReference);
                    }
                }

            }
            EditorGUILayout.EndHorizontal();
            

            //EmissionModule
            EditorGUILayout.BeginHorizontal();
            {

                EditorGUILayout.LabelField("Emission            :   ");
                if (m_Reference.emissionModuleReference == null)
                {

                    if (GUILayout.Button("Add"))
                    {

                        m_Reference.gameObject.AddComponent<PSEmissionModule>();
                        m_Reference.emissionModuleReference = m_Reference.gameObject.GetComponent<PSEmissionModule>();
                        m_Reference.emissionModuleReference.PreProcess(m_Reference.particleSystemReference);
                    }
                }
                else
                {

                    if (GUILayout.Button("Remove"))
                    {
                        DestroyImmediate(m_Reference.emissionModuleReference);
                    }
                }

            }
            EditorGUILayout.EndHorizontal();


            //VelocityOverLifeTimeModule
            EditorGUILayout.BeginHorizontal();
            {

                EditorGUILayout.LabelField("VelocityOverLifeTime    :   ");
                if (m_Reference.velocityOverLifeTimeModuleReference == null)
                {

                    if (GUILayout.Button("Add"))
                    {

                        m_Reference.gameObject.AddComponent<PSVelocityOverLifeTimeModule>();
                        m_Reference.velocityOverLifeTimeModuleReference = m_Reference.gameObject.GetComponent<PSVelocityOverLifeTimeModule>();
                        m_Reference.velocityOverLifeTimeModuleReference.PreProcess(m_Reference.particleSystemReference);
                    }
                }
                else
                {

                    if (GUILayout.Button("Remove"))
                    {
                        DestroyImmediate(m_Reference.velocityOverLifeTimeModuleReference);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            //SizeOverLifeTimeModule
            EditorGUILayout.BeginHorizontal();
            {

                EditorGUILayout.LabelField("SizeOverLifeTimeModule  :   ");
                if (m_Reference.sizeOverLifeTimeModuleReference == null)
                {

                    if (GUILayout.Button("Add"))
                    {

                        m_Reference.gameObject.AddComponent<PSSizeOverLifeTimeModule>();
                        m_Reference.sizeOverLifeTimeModuleReference = m_Reference.gameObject.GetComponent<PSSizeOverLifeTimeModule>();
                        m_Reference.sizeOverLifeTimeModuleReference.PreProcess(m_Reference.particleSystemReference);
                    }
                }
                else
                {

                    if (GUILayout.Button("Remove"))
                    {
                        DestroyImmediate(m_Reference.sizeOverLifeTimeModuleReference);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

        }

    }
}


