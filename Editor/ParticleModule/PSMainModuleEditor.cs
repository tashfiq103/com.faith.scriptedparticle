
namespace com.faithstudio.ScriptedParticle
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(PSMainModule))]
    public class PSMainModuleEditor : Editor
    {
        private PSMainModule m_Reference;

        private void OnEnable()
        {
            m_Reference = (PSMainModule)target;
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

            m_Reference.manupulateStartLifeTime = EditorGUILayout.Toggle(
                "Manupulate : StartLifeTime",
                m_Reference.manupulateStartLifeTime
                );

            

            if (m_Reference.manupulateStartLifeTime)
            {

                m_Reference.maxChangeOfStartLifeTime = EditorGUILayout.Slider(
                "MaxChangeOfStartLifeTime",
                     m_Reference.maxChangeOfStartLifeTime,
                    -1f,
                    100f
                );
                
            }
        }
    }
}


