using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        private EntityConrtoller _controller;
        public EntityConrtoller Controller
        {
            get => _controller;
            set
            {
                if (_controller != null)
                    _controller.EntityParent = null; // ������� �������� � ����������� �����������
                _controller = value; 
                _controller.EntityParent = this; // ����� �������� � �������� �����������
            }
        }

        protected virtual void Start()
        {

        }
        
        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        protected float deltaTime => Time.deltaTime;
        protected float fixedDeltaTime => Time.fixedDeltaTime;
    }
}
