namespace com.faithstudio.ScriptedParticle
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(PSVelocityOverLifeTimeModule))]
    public class PSVelocityOverLifeTimeModuleEditor : Editor
    {
        private PSVelocityOverLifeTimeModule m_Reference;

        private void OnEnable()
        {
            m_Reference = (PSVelocityOverLifeTimeModule)target;
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

            if (m_Reference.debugPanel) {

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

            m_Reference.manupulateLinearVelocity = EditorGUILayout.Toggle(
                "Manupulate : LinearVelocity",
                m_Reference.manupulateLinearVelocity
                );



            if (m_Reference.manupulateLinearVelocity)
            {

                m_Reference.maxChangeOfLinearVelocity = EditorGUILayout.Slider(
                "MaxChangeOfLinearVelocity",
                     m_Reference.maxChangeOfLinearVelocity,
                    -1f,
                    100f
                );
            }
        }
    }
}



