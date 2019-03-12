using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using ATM.Interfaces;

namespace ATM.Classes
{
    public class SeparationChecker : ISeparationChecker
    {
        private enum Direction {north,south,east,west};
        private IAirSpace _airSpace;
        private ICondition _separationCondition;

        public SeparationChecker(IAirSpace airSpace, ICondition separationCondition)
        {
            _airSpace = airSpace;
            _separationCondition = separationCondition;
        }

        private int AltitudeSeparation(int t1, int t2) 
        {
            int sep = Math.Abs(t1 - t2);
            return sep;
        }

        private double distanceBetween(int x1, int y1, int x2, int y2)
        {
            int dY = Math.Abs(y1 - y2);
            int dX = Math.Abs(x1 - x2);
            return Math.Sqrt((dY * dY) + (dX * dX));
        }

        private Direction calculateDirection(Track track1)
        {
            if (track1.CurrentCompCourse >= 45 && track1.CurrentCompCourse < 135) //Eastbound course
            {
                return Direction.east;
            }
            else if (track1.CurrentCompCourse >= 135 && track1.CurrentCompCourse < 225)
            {
                return Direction.south;
            }
            else if (track1.CurrentCompCourse >= 225 && track1.CurrentCompCourse < 315)
            {
                return Direction.west;
            }
            else
            {
                return Direction.north;
            }
        }

        private Point calculateEndPoint(Track track, Direction d)
        {
            switch (d)
            {
                case Direction.east:
                    int toEast = _airSpace.getEastBoundary() - track.XCoordinate;
                    int e = Convert.ToInt32(Math.Tan(track.CurrentCompCourse) * toEast); //SKAL UNDERSØGES OM DEN VIRKER!!!!!
                    if (track.CurrentCompCourse < 90)
                    {
                        return new Point(80000, track.YCoordinate + e);
                    }
                    else if (track.CurrentCompCourse > 90)
                    {
                        return new Point(80000, track.YCoordinate - e);
                    }
                    else
                    {
                        return new Point(80000,track.YCoordinate);
                    }
                    break;
                case Direction.west:
                    int toWest = track.XCoordinate;
                    int w = Convert.ToInt32(Math.Tan(track.CurrentCompCourse) * toWest); //SKAL UNDERSØGES OM DEN VIRKER!!!!!
                    if (track.CurrentCompCourse < 270)
                    {
                        return new Point(80000, track.YCoordinate + w);
                    }
                    else if (track.CurrentCompCourse > 270)
                    {
                        return new Point(80000, track.YCoordinate - w);
                    }
                    else
                    {
                        return new Point(0, track.YCoordinate);
                    }
                    break;
                case Direction.north:
                    int toNorth = _airSpace.getNorthBoundary() - track.YCoordinate;
                    int n = Convert.ToInt32(Math.Tan(track.CurrentCompCourse) * toNorth); //SKAL UNDERSØGES OM DEN VIRKER!!!!!
                    if (track.CurrentCompCourse < 360)
                    {
                        return new Point(track.XCoordinate - n, 80000);
                    }
                    else if (track.CurrentCompCourse > 0)
                    {
                        return new Point(track.XCoordinate + n, 80000);
                    }
                    else
                    {
                        return new Point(track.XCoordinate, 80000);
                    }
                    break;
                case Direction.south:
                    int toSouth = track.YCoordinate;
                    int s = Convert.ToInt32(Math.Tan(track.CurrentCompCourse) * toSouth); //SKAL UNDERSØGES OM DEN VIRKER!!!!!
                    if (track.CurrentCompCourse < 180)
                    {
                        return new Point(track.XCoordinate + s, 80000);
                    }
                    else if (track.CurrentCompCourse > 180)
                    {
                        return new Point(track.XCoordinate - s, 80000);
                    }
                    else
                    {
                        return new Point(track.XCoordinate, 80000);
                    }
                    break;
                default:
                    return null;
            }
        }

        private bool willIntersect(Point p1Start, Point p1End, Point p2Start, Point p2End)
        {
            float dx12 = Math.Abs(p1End.X - p1Start.X);
            float dy12 = Math.Abs(p1End.Y - p1Start.Y);
            float dx34 = Math.Abs(p2End.X - p2Start.X);
            float dy34 = Math.Abs(p2End.Y - p2Start.Y);

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1Start.X - p2Start.X) * dy34 + (p2Start.Y - p1Start.Y) * dx34)
                / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                return false;
            }

            return true;
        }

        private double FindDistanceToSegment(Point point, Point lineStart, Point lineEnd)
        {
            float dx = lineEnd.X - lineStart.X;
            float dy = lineEnd.Y - lineStart.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                dx = point.X - lineStart.X;
                dy = point.Y - lineStart.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            float t = ((point.X - lineStart.X) * dx + (point.Y - lineStart.Y) * dy) /
                      (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                dx = point.X - lineStart.X;
                dy = point.Y - lineStart.Y;
            }
            else if (t > 1)
            {
                dx = point.X - lineEnd.X;
                dy = point.Y - lineEnd.Y;
            }
            else
            {
                Point closest = new Point(Convert.ToInt32(lineStart.X + t * dx), Convert.ToInt32(lineStart.Y + t * dy));
                dx = point.X - closest.X;
                dy = point.Y - closest.Y;
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }


        private bool tracksWillConflict(Point p1Start, Point p1End, Point p2Start, Point p2End)
        {
            Point closest;
            double best_dist = double.MaxValue, test_dist;

            // Try p1.
            test_dist = FindDistanceToSegment(p1Start, p2Start, p2End);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
            }

            // Try p2.
            test_dist = FindDistanceToSegment(p1End, p2Start, p2End);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
            }

            // Try p3.
            test_dist = FindDistanceToSegment(p2Start, p1Start, p1End);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
            }

            // Try p4.
            test_dist = FindDistanceToSegment(p2End, p1Start, p1End);
            if (test_dist < best_dist)
            {
                best_dist = test_dist;
            }

            return (best_dist < _separationCondition.getHorizontalSeparationCondition());
        }
        private bool willHaveConflict(Track track1, Track track2)
        {
            Direction t1D = calculateDirection(track1);
            Direction t2D = calculateDirection(track2);
            Point t1EndPoint = calculateEndPoint(track1, t1D);
            Point t2EndPoint = calculateEndPoint(track2, t2D);
            Point t1StartPoint = new Point(track1.XCoordinate,track1.YCoordinate);
            Point t2StartPoint = new Point(track2.XCoordinate,track2.YCoordinate);
            if (willIntersect(t1StartPoint, t1EndPoint, t2StartPoint, t2EndPoint))
            {
                return true;
            }
            else if (tracksWillConflict(t1StartPoint, t1EndPoint, t2StartPoint, t2EndPoint))
            {
                return true;
            }

            return false;

        }

        private bool horizontalSeparationConflict(Track track1, Track track2)
        {
            if (distanceBetween(track1.XCoordinate, track1.YCoordinate, track2.XCoordinate, track2.YCoordinate) < //Checks for current distance between tracks
            _separationCondition.getHorizontalSeparationCondition())
            {
                return true;
            }
            else if (willHaveConflict(track1, track2))
            {
                return true;
            }

            return false;
        }


        private bool hasConflict(Track track1, Track track2)
        {
            if (track1.CurrentCompCourse == 0.0 | track1.Velocity == 0.0 | track2.Velocity == 0.0 |
                track2.CurrentCompCourse == 0.0)                                                       //No information on current course or velocity
            {
                return false;
            }
            else
            {
                if ((AltitudeSeparation(track1.Altitude, track2.Altitude) > _separationCondition.getVerticalSeparationCondition()) && horizontalSeparationConflict(track1, track2))
                {
                    return true;
                }

                return false;
            }
        }
        public List<int> CheckForSeparation(List<Track> tracks, Track track)
        {
            int index = 0;
            List<int> indexes = new List<int>();
            foreach (var t in tracks)
            {
                if (t.Tag != track.Tag)
                {
                    if (hasConflict(t, track))
                    {
                        int indexToAdd = index;
                        indexes.Add(indexToAdd);
                    }
                }

                ++index;
            }

            return indexes;
        }


    }
}
