using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;

namespace CSGO_PhoenixLoader.Common.Math
{
    public readonly struct Line3D
    {
        public readonly Vector3 StartPoint, EndPoint;

        public Line3D(Vector3 startPoint, Vector3 endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public (Vector3, Vector3) ClosestPointsBetween(Line3D other)
        {
            if (IsParallelTo(other))
            {
                return (StartPoint, other.ClosestPointTo(StartPoint, false));
            }

            var u = GetDirection();
            var v = other.GetDirection();
            var w = StartPoint - other.StartPoint;
            var uu = u.Dot(u);
            var uv = u.Dot(v);
            var vv = v.Dot(v);
            var uw = u.Dot(w);
            var vw = v.Dot(w);
            var sc = (uv * vw - vv * uw) / (uu * vv - uv * uv);
            var tc = (uu * vw - uv * uw) / (uu * vv - uv * uv);
            return (StartPoint + sc * u, other.StartPoint + tc * v);
        }

        public Vector3 ClosestPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
        {
            // Calculate the direction of the line
            Vector3 lineDirection = lineEnd - lineStart;
            lineDirection.Normalize();

            // Calculate the vector from the start of the line to the point
            Vector3 pointDirection = point - lineStart;

            // Calculate the distance along the line to the closest point
            float distanceAlongLine = Vector3.Dot(pointDirection, lineDirection);

            // Calculate the closest point on the line
            Vector3 closestPointOnLine = lineStart + lineDirection * distanceAlongLine;

            return closestPointOnLine;
        }

        public (Vector3, Vector3) ClosestPointsBetween(Line3D other, bool mustBeOnSegments)
        {
            if (!IsParallelTo(other) || !mustBeOnSegments)
            {
                var pair = ClosestPointsBetween(other);
                if (!mustBeOnSegments)
                {
                    return pair;
                }

                if ((pair.Item1 - StartPoint).Length() <= GetLength() &&
                    (pair.Item1 - EndPoint).Length() <= GetLength() &&
                    (pair.Item2 - other.StartPoint).Length() <= other.GetLength() &&
                    (pair.Item2 - other.EndPoint).Length() <= other.GetLength())
                {
                    return pair;
                }
            }

            var checkPoint = other.ClosestPointTo(StartPoint, true);
            var distance = (checkPoint - StartPoint).Length();
            var closestPair = (StartPoint, checkPoint);
            var minDistance = distance;

            checkPoint = other.ClosestPointTo(EndPoint, true);
            distance = (checkPoint - EndPoint).Length();
            if (distance < minDistance)
            {
                closestPair = (EndPoint, checkPoint);
                minDistance = distance;
            }

            checkPoint = ClosestPointTo(other.StartPoint, true);
            distance = (checkPoint - other.StartPoint).Length();
            if (distance < minDistance)
            {
                closestPair = (checkPoint, other.StartPoint);
                minDistance = distance;
            }

            checkPoint = ClosestPointTo(other.EndPoint, true);
            distance = (checkPoint - other.EndPoint).Length();
            if (distance < minDistance)
            {
                closestPair = (checkPoint, other.EndPoint);
            }

            return closestPair;
        }

        /// <summary>
        /// Get closest point to other point.
        /// </summary>
        public Vector3 ClosestPointTo(Vector3 value, bool mustBeOnSegment)
        {
            var direction = GetDirection();
            var dotProduct = (value - StartPoint).Dot(direction);

            if (mustBeOnSegment)
            {
                if (dotProduct < 0)
                {
                    dotProduct = 0;
                }

                var length = GetLength();
                if (dotProduct > length)
                {
                    dotProduct = length;
                }
            }

            return StartPoint + dotProduct * direction;
        }

        /// <summary>
        /// Get direction of the line.
        /// </summary>
        public Vector3 GetDirection()
        {
            return (EndPoint - StartPoint);
        }

        /// <summary>
        /// Get length of a line.
        /// </summary>
        public float GetLength()
        {
            return (EndPoint - StartPoint).Length();
        }

        /// <summary>
        /// Is line parallel to other line?
        /// </summary>
        public bool IsParallelTo(Line3D other)
        {
            return GetDirection().IsParallelTo(other.GetDirection());
        }
    }
}
