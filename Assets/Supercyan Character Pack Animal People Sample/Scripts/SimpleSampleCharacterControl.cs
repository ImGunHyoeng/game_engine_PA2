﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Supercyan.AnimalPeopleSample
{
    public class SimpleSampleCharacterControl : MonoBehaviour
    {
        time_ctr time_Ctr;
        Vector3 spawn;
        public AudioClip save;
        AudioSource audio;
        save_ctr_view save_ctr;
        private enum ControlMode
        {
            /// <summary>
            /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
            /// </summary>
            Tank,
            /// <summary>
            /// Character freely moves in the chosen direction from the perspective of the camera
            /// </summary>
            Direct
        }

        [SerializeField] private float m_moveSpeed = 2;
        [SerializeField] private float m_turnSpeed = 200;
        [SerializeField] private float m_jumpForce = 4;

        [SerializeField] private Animator m_animator = null;
        [SerializeField] private Rigidbody m_rigidBody = null;

        [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

        private float m_currentV = 0;
        private float m_currentH = 0;

        private readonly float m_interpolation = 10;
        private readonly float m_walkScale = 0.33f;
        private readonly float m_backwardsWalkScale = 0.16f;
        private readonly float m_backwardRunScale = 0.66f;

        private bool m_wasGrounded;
        private Vector3 m_currentDirection = Vector3.zero;

        private float m_jumpTimeStamp = 0;
        private float m_minJumpInterval = 0.25f;
        private bool m_jumpInput = false;

        private bool m_isGrounded;

        private List<Collider> m_collisions = new List<Collider>();

        private void Awake()
        {
            save_ctr = GameObject.Find("Save_ctr").GetComponent<save_ctr_view>();
            audio=this.gameObject.AddComponent<AudioSource>();
            spawn=GetComponent<Transform>().position;
            time_Ctr = GameObject.Find("Time_ctr").GetComponent<time_ctr>();
            if (!m_animator) { gameObject.GetComponent<Animator>(); }
            if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
            audio.clip = save;
        }

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!m_collisions.Contains(collision.collider))
                    {
                        m_collisions.Add(collision.collider);
                    }
                    m_isGrounded = true;
                }
            }
            if (collision.gameObject.tag == "Enemy")
            {
                time_Ctr.minus_time(10);
                spawn_point();
            }

        }
        void spawn_point()
        { transform.position = spawn; }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag =="Save")
            {
                Debug.Log(transform.position);
                Debug.Log(spawn);
                spawn = other.transform.position;
                audio.Play();
                save_ctr.save_text();
                Destroy(other.gameObject);
                
            }
            if(other.tag=="Clear")
            {
                SceneManager.LoadScene("Clear");
                set_result.st_result=time_Ctr.get_time();
            }
            if(other.tag=="Over")
            {
                SceneManager.LoadScene("Gameover");
            }
            if (other.tag == "Minus_time")
            {
                time_Ctr.minus_time(5);
            }
            if (other.tag == "Fall")
            {
                time_Ctr.minus_time(5);
                spawn_point();
            }
            if (other.tag=="Enemy")
            {
                time_Ctr.minus_time(10);
                spawn_point();
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true; break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                {
                    m_collisions.Remove(collision.collider);
                }
                if (m_collisions.Count == 0) { m_isGrounded = false; }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }

        private void Update()
        {
            /*if (!m_jumpInput && Input.GetKey(KeyCode.Space))
            {
                m_jumpInput = true;
            }*/
        }

        private void FixedUpdate()
        {
            //m_animator.SetBool("Grounded", m_isGrounded);

            switch (m_controlMode)
            {
                case ControlMode.Direct:
                    DirectUpdate();
                    break;

                case ControlMode.Tank:
                    TankUpdate();
                    break;

                default:
                    Debug.LogError("Unsupported state");
                    break;
            }

            m_wasGrounded = m_isGrounded;
            m_jumpInput = false;
        }

        private void TankUpdate()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            
            bool walk = Input.GetKey(KeyCode.LeftShift);

            if (v < 0)
            {
                
                if (walk) { v *= m_backwardsWalkScale; }
                else { v *= m_backwardRunScale; }
                
                
            }
            else if (walk)
            {
                v *= m_walkScale;
            }

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

            m_animator.SetFloat("MoveSpeed", m_currentV);

            //JumpingAndLanding();
        }

        private void DirectUpdate()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            Transform camera = Camera.main.transform;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                v *= m_walkScale;
                h *= m_walkScale;
            }

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                transform.rotation = Quaternion.LookRotation(m_currentDirection);
                transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

                m_animator.SetFloat("MoveSpeed", direction.magnitude);
            }

            //JumpingAndLanding();
        }

        private void JumpingAndLanding()
        {
            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

            if (jumpCooldownOver && m_isGrounded && m_jumpInput)
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            }

            if (!m_wasGrounded && m_isGrounded)
            {
                m_animator.SetTrigger("Land");
            }

            if (!m_isGrounded && m_wasGrounded)
            {
                m_animator.SetTrigger("Jump");
            }
        }
    }
    
}