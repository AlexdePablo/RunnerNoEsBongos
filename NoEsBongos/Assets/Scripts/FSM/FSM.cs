using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiniteStateMachine
{
    public class State
    {
        protected FSM m_FSM;
        public State(FSM fSM)
        {
            m_FSM = fSM;
        }

        public virtual void Init() { Debug.Log("Entering state: " + GetType()); }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { Debug.Log("Exiting state: " + GetType()); }
        public virtual void OnCollisionEnter2D(Collision2D collision) { }
        public virtual void OnCollisionStay2D(Collision2D collision) { }
        public virtual void OnCollisionExit2D(Collision2D collision) { }
        public virtual void OnTriggerEnter2D(Collider2D collider) { }
        public virtual void OnTriggerStay2D(Collider2D collider) { }
        public virtual void OnTriggerExit2D(Collider2D collider) { }

    }

    public class FSM
    {
   


        private GameObject m_Owner;
        public GameObject Owner
        {
            get { return m_Owner; }
        }
        private State m_CurrentState = null;
        private List<State> m_States = new List<State>();

        public FSM(GameObject owner)
        {
            m_Owner = owner;
        }

        public void AddState(State state)
        {
            if (!m_States.Contains(state))
                m_States.Add(state);
        }

        public T GetState<T>() where T : State
        {
            return m_States.First(state => state.GetType() == typeof(T)) as T;
        }

        public bool ChangeState<T>() where T : State
        {
            T state = GetState<T>();
            if (state != null)
            {
                m_CurrentState?.Exit();

                m_CurrentState = state;
                m_CurrentState.Init();

                return true;
            }
            return false;
        }

        public void Update()
        {
            m_CurrentState?.Update();
        }
        public void FixedUpdate()
        {
            m_CurrentState?.FixedUpdate();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            m_CurrentState?.OnCollisionEnter2D(collision);
        }
        public void OnCollisionStay2D(Collision2D collision)
        {
            m_CurrentState?.OnCollisionStay2D(collision);
        }
        public void OnCollisionExit2D(Collision2D collision)
        {
            m_CurrentState?.OnCollisionExit2D(collision);
        }
        public void OnTriggerEnter2D(Collider2D collider)
        {
            m_CurrentState?.OnTriggerEnter2D(collider);
        }
        public void OnTriggerStay2D(Collider2D collider)
        {
            m_CurrentState?.OnTriggerStay2D(collider);
        }
        public void OnTriggerExit2D(Collider2D collider)
        {
            m_CurrentState?.OnTriggerExit2D(collider);
        }
    }
}

