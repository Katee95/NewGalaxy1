using System;
using System.Collections.Generic;
using System.Linq;
using Galaxy.Core.Actors;
using Galaxy.Core.Environment;
using System.Drawing;

namespace Galaxy.Environments.Actors
{
  
    public class BulletER : BaseActor
     {
            #region Constant
            private static int Speed = 10;
            #endregion
  
 
            #region Constructors

            public BulletER(ILevelInfo info) : base(info)
             {
                 Width = 6;
                 Height = 12;
                ActorType = ActorType.Enemy;
             }
 
             #endregion
  
 
  #region Overrides

 
  public override void Load()
   {
      Load(@"Assets\bullet.png");
   }


  public override void Update()
      {
          Position = new Point(Position.X, Position.Y + Speed);
         
      }

    #endregion
  
  }
} 


