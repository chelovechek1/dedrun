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
                    _controller.EntityParent = null; // Удаляем родителя у предыдущего контроллера
                _controller = value; 
                _controller.EntityParent = this; // Задаём родителя у текущего контроллера
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
