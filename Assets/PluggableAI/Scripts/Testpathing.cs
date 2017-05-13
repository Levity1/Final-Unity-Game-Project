using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Complete
{
    public class Testpathing : MonoBehaviour
    {

        public List<Transform> wayPointsForAI;
        private StateController m_StateController;				// Reference to the StateController for AI tanks
        public Transform enemy;
        // Use this for initialization
        private void Start()
        {
            SetupAI(wayPointsForAI);
        }

        public void SetupAI(List<Transform> wayPointList)
        {
            m_StateController = enemy.GetComponent<StateController>();
            m_StateController.SetupAI(true, wayPointList);

            //m_Shooting = m_Instance.GetComponent<TankShooting> ();
            //m_Shooting.m_PlayerNumber = m_PlayerNumber;

            //	m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas> ().gameObject;

            // Get all of the renderers of the tank.

            // Go through all the renderers...
         
        }

        // Update is called once per frame

    }
}
