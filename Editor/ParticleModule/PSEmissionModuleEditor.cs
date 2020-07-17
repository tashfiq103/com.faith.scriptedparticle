
namespace com.faithstudio.ScriptedParticle
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(PSEmissionModule))]
    public class PSEmissionModuleEditor : Editor
    {
        private PSEmissionModule m_Reference;

        private void OnEnable()
        {
            m_Reference = (PSEmissionModule)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CustomGUI();

            DrawDefaultInspector();

            serializedObject.ApplyModifiedProperties();
        }

        private void CustomGUI()
        {

            EditorGUILayout.Space();

            m_Reference.debugPanel = EditorGUILayout.Foldout(
                     m_Reference.debugPanel,
                     "Debug Option"
                 );

            if (m_Reference.debugPanel)
            {

                EditorGUILayout.BeginHorizontal();
                {

                    EditorGUILayout.BeginVertical();
                    {
                        if (GUILayout.Button("Initialize"))
                        {

                            m_Reference.Initialize();
                        }

                        if (GUILayout.Button("Restore Default"))
                        {

                            m_Reference.RestoreToDefault();
                        }
                    }
                    EditorGUILayout.EndVertical();



                    EditorGUILayout.BeginVertical();
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
                    EditorGUILayout.EndVertical();

                }
                EditorGUILayout.EndHorizontal();
            }



            EditorGUILayout.Space();
            
            m_Reference.manupulateRateOverTime = EditorGUILayout.Toggle(
                "Manupulate : RateOverTime",
                m_Reference.manupulateRateOverTime
                );

            if (m_Reference.manupulateRateOverTime) {

                m_Reference.maxChangeOnRateOverTime = EditorGUILayout.Slider(
                "MaxChangeOfEmissionRate",
                     m_Reference.maxChangeOnRateOverTime,
                    -1f,
                    25f
                );
            }
        }
    }
}


