namespace com.faithstudio.ScriptedParticle
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(PSSizeOverLifeTimeModule))]
    public class PSSizeOverLifeTimeModuleEditor : Editor
    {
        private PSSizeOverLifeTimeModule m_Reference;

        private void OnEnable()
        {
            m_Reference = (PSSizeOverLifeTimeModule)target;
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

                EditorGUILayout.BeginVertical();
                {

                    EditorGUILayout.BeginHorizontal();
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

                    if (!EditorApplication.isPlaying)
                    {

                        EditorGUILayout.HelpBox("(Debug)Animate can only be work on 'PlayMode'", MessageType.Warning);
                    }
                    else {
                    }
                }
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.Space();

            m_Reference.manupulateSizeOverLifeTime = EditorGUILayout.Toggle(
                "Manupulate : Size",
                m_Reference.manupulateSizeOverLifeTime
                );



            if (m_Reference.manupulateSizeOverLifeTime)
            {

                m_Reference.maxChangeOfSizeOverLifeTime = EditorGUILayout.Slider(
                "MaxChangeOfSizeOverLifeTime",
                     m_Reference.maxChangeOfSizeOverLifeTime,
                    -1f,
                    5f
                );
            }
        }
    }
}



