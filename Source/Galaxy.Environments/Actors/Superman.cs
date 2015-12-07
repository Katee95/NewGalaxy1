
using System.Diagnostics;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;
using Point = System.Drawing.Point;


namespace Galaxy.Environments.Actors
{
    class Superman : BaseActor
    {
        #region Constant

     private const long StartFlyMs = 2000;

        #endregion

        #region Private fields

        protected bool m_flying;
        protected Stopwatch m_flyTimer;

        #endregion

        #region Constructors

        public Superman(ILevelInfo info)
            : base(info)
        {
            Width = 30;
            Height = 30;
            ActorType = ActorType.Superman;
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
            Load(@"Assets\superm.png");
            if (m_flyTimer == null)
            {
                m_flyTimer = new Stopwatch();
                m_flyTimer.Start();
            }
        }

        #endregion

        #region Private methods
        public override bool IsAlive
        {
            get
            {
                return base.IsAlive;
            }
            set
            {
                base.IsAlive = true;
                CanDrop = false;
            }
        }

        private void h_changePosition()
        {
            int speed = 10;
            Position = new Point(Position.X + speed, Position.Y + speed );
        }

        #endregion
    }
}
