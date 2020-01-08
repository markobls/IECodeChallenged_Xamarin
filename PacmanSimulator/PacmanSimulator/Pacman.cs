using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PacmanSimulator
{
    public class Pacman
    {
        public enum Direction
        {
            EAST = 0,
            SOUTH = 1,
            WEST = 2,
            NORTH = 3,
        }

        double _x;
        public double PositionX
        {
            get
            {
                return _x;
            }
            set
            {
                // maintain inside grid
                if (value < 0 || value > App.GridSize)
                    return;
                else
                {
                    _x = value;
                }
            }
        }

        double _y;
        public double PositionY
        {
            get
            {
                return _y;
            }
            set
            {
                // maintain inside grid
                if (value < 0 || value > App.GridSize)
                    return;
                else
                {
                    _y = value;
                }
            }
        }

        double _angle = 0;
        public double Facing
        {
            get
            {
                return _angle;
            }
            set
            {
                if (value > 270)
                    _angle = 0;
                else if (value < 0)
                    _angle = 270;
                else
                    _angle = value;
            }
        }

        public string FacingString => Enum.GetName(typeof(Direction), (int) _angle / 90);

        public void Place(int x, int y, Direction dir)
        {
            PositionX = x;
            PositionY = y;
            Facing = (int) dir * 90;
        }

        public void Move()
        {
            // 0.0174533 converts to radians
            PositionX += Math.Round(Math.Cos(_angle * 0.0174533));
            PositionY -= Math.Round(Math.Sin(_angle * 0.0174533));
        }

        public void Turn(bool left = false)
        {
            Facing += left ? -90 : 90;
        }

    }
}
