#region using

using System.Diagnostics;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;
using Point = System.Drawing.Point;


#endregion

namespace Galaxy.Environments.Actors
{
  public class Ship2 : Ship
  {
      public Ship2(ILevelInfo info):base(info)
    {
        Width = 30;
        Height = 30;
        ActorType = ActorType.Enemy;
    }
     
    
      #region Overrides  

    public override void Load()
      {
        base.Load();
        Load(@"Assets\ship2.png");
        if (m_flyTimer == null)
        {
            m_flyTimer = new Stopwatch();
            m_flyTimer.Start();
        }
      }
   
   #endregion

    private void h_changePosition()
    {
        Position = new Point((int)(Position.X), (int)(Position.Y));
    }
  }
}
