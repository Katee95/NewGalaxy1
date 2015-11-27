using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Galaxy.Environments.Actors
{
    internal class Ship3 : DethAnimationActor
    {
        #region Constant

        private const int MaxSpeed = 3;
        private const long StartFlyMs = 2000;
        private bool sign;

        #endregion

        #region Private fields

        protected bool m_flying;
        protected Stopwatch m_flyTimer;

        #endregion

        #region Constructors

        public Ship3(ILevelInfo info) : base(info)
        {
            Width = 30;
            Height = 30;
            ActorType = ActorType.Enemy;
        }

        public bool Movement
        {
            get { return sign; }
            set { sign = value; }
        }

        #endregion

        #region Overrides

        public override void Update()
        {
            base.Update();

            if (!IsAlive)
                return;

            if (!m_flying)
            {
                if (m_flyTimer.ElapsedMilliseconds <= StartFlyMs) return;

                m_flyTimer.Stop();
                m_flyTimer = null;
                h_changePosition();
                m_flying = true;
            }
            else
            {
                h_changePosition();
            }
        }

        #endregion

        #region Overrides

        public override void Load()
        {
            Load(@"Assets\ship3.png");
            //InitTimer();
            if (m_flyTimer == null)
            {
                m_flyTimer = new Stopwatch();
                m_flyTimer.Start();
            }
        }

        #endregion

        #region Private methods

        private void h_changePosition()
        {
            if (Movement == true)
            {

                Position = new Point(Position.X + 3, Position.Y + 3);
            }
            else
            {
                Position = new Point(Position.X - 3, Position.Y + 3);
            }

            #endregion
        }
    }
}

