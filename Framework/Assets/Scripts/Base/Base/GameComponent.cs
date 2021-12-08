using System;
using UnityEngine;

namespace Base.Base
{
    public abstract class GameComponent : MonoBehaviour
    {
        /// <summary>
        /// 防止进行其他操作，不能重写。
        /// </summary>
        private void Awake()
        {
            GameEntry.RegisterComponent(this);
        }

        public abstract void Init();
        public virtual int Priority => 0;
        public abstract void Update(float elapseSeconds, float realElapseSeconds);
        public abstract void Shutdown();
    }
}