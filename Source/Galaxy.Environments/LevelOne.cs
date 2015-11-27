#region using

using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using Galaxy.Core.Actors;
using Galaxy.Core.Collision;
using Galaxy.Core.Environment;
using Galaxy.Environments.Actors;
using System;

#endregion

namespace Galaxy.Environments
{
    /// <summary>
    ///   The level class for Open Mario.  This will be the first level that the player interacts with.
    /// </summary>
    public class LevelOne : BaseLevel
    {
        private int m_frameCount;


        #region Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="LevelOne" /> class.
        /// </summary>
        public LevelOne()
        {
            // Backgrounds
            FileName = @"Assets\LevelOne.png";

            // Enemies
            for (int i = 0; i < 7; i++)
            {
                var ship = new Ship(this);
                int positionY = ship.Height + 10;
                int positionX = 150 + i*(ship.Width + 50);
                ship.Position = new Point(positionX, positionY);
                

                Actors.Add(ship);
            }
            // Enemies2
            for (int i = 0; i < 5; i++)
            {
                var ship2 = new Ship2(this);
                int positionY1 = ship2.Height + 30;
                int positionX1 = 180 + i * (ship2.Width + 50);

                ship2.Position = new Point(positionX1, positionY1);

                Actors.Add(ship2);
            }
            for (int i = 0; i < 6; i++)
            {
                var ship3 = new Ship3(this);
                int positionY = ship3.Height + 70;
                int positionX = 180 + i*(ship3.Width + 50);
                ship3.Position = new Point(positionX, positionY);

                //присваиваем значение Ship куда он должен лететь true false
                if (i == 0 || i == 1 || i == 2)
                {
                    ship3.Movement = true;
                }
                else
                {
                    ship3.Movement = false;
                }

                Actors.Add(ship3);
            }

            for (int i = 0; i < 5; i++)
            {
                var ship4 = new Ship(this);
                int positionY3 = ship4.Height + 70;
                int positionX3 = 180 + i * (ship4.Width + 50);
                ship4.Position = new Point(positionX3, positionY3);

                Actors.Add(ship4);
            }

            // Player
            Player = new PlayerShip(this);
            int playerPositionX = Size.Width/2 - Player.Width/2;
            int playerPositionY = Size.Height - Player.Height - 50;
            Player.Position = new Point(playerPositionX, playerPositionY);
            Actors.Add(Player);

          
        }

        #endregion

        #region Overrides

        public void BShots()
        {
            if (Shot.ElapsedMilliseconds < 100)
                return;

            var bR = new BulletER(this);

            var lis = Actors.Where((actor) => actor is Ship || actor is Ship2).ToList();

            if (lis.Count > 0)
            {
                Random rd = new Random();
                int a = rd.Next(lis.Count);

                var mission = lis[a].Position;
                bR.Position = new Point(mission.X, mission.Y + 10);

                bR.Load();

                Actors.Add(bR);

                Shot.Restart();
            }
        }

       private void h_dispatchKey()
        {
            if (!IsPressed(VirtualKeyStates.Space)) return;

            if (m_frameCount % 10 != 0) return;


            Bullet bullet = new Bullet(this)
            {
                Position = Player.Position
            };

            bullet.Load();
            Actors.Add(bullet);
        }

        public override BaseLevel NextLevel()
        {
            return new StartScreen();
        }

        private Stopwatch Shot = new Stopwatch();
        

        public override void Update()
        {
            m_frameCount++;
            h_dispatchKey();
            BShots();
            base.Update();

            IEnumerable<BaseActor> killedActors = CollisionChecher.GetAllCollisions(Actors);

            foreach (BaseActor killedActor in killedActors)
            {
                if (killedActor.IsAlive)
                    killedActor.IsAlive = false;
            }

            List<BaseActor> toRemove = Actors.Where(actor => actor.CanDrop).ToList();
            BaseActor[] actors = new BaseActor[toRemove.Count()];
            toRemove.CopyTo(actors);

            foreach (BaseActor actor in actors.Where(actor => actor.CanDrop))
            {
                Actors.Remove(actor);
            }

            if (Player.CanDrop)
                Failed = true;

            if (Actors.All(actor => actor.ActorType != ActorType.Enemy))
                
                Success = true;
        }

        public override void Load()
        {
            base.Load();
            Shot.Start();
        }

        #endregion
        
    }
}
