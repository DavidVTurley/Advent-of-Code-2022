using System.Runtime.CompilerServices;

namespace Advent_of_Code.Extras;

/// <summary>
/// A way of holding two floats in an object.
/// Equality comparisons accurate up to 0.01 decimals;
/// </summary>
public struct Vec2
{
    public static Vec2 Up => new Vec2(0, 1);
    public static Vec2 Down => new Vec2(0, -1);
    public static Vec2 Left => new Vec2(-1, 0);
    public static Vec2 Right => new Vec2(1, 0);
    public static Vec2 Zero => new Vec2(0, 0);


    public Decimal X;
    public Decimal Y;

    public Vec2(Decimal x, Decimal y)
    {
        X = x;
        Y = y;
    }

    public static Vec2 GetDirectionVector(Char direction) => GetDirectionVector((Direction)direction);
    public static Vec2 GetDirectionVector(Direction direction)
    {
        return direction switch
        {
            Extras.Direction.Down => Down,
            Extras.Direction.Left => Left,
            Extras.Direction.Right => Right,
            Extras.Direction.Up => Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public Boolean TargetWithinNUnits(Vec2 target, Decimal n)
    {
        if (!(n >= 0)) throw new Exception("Number must be positive");

        Decimal xDelta = target.X - X;
        Decimal yDelta = target.Y - Y;
        
        return (xDelta > n || xDelta < -n) || (yDelta > n || yDelta < -n);
    }

    public Vec2 GetVectorToTarget(Vec2 target) => GetVectorToTarget(this, target);
    public static Vec2 GetVectorToTarget(Vec2 origin, Vec2 target)
    {
        return new Vec2(target.X - origin.X, target.Y - origin.Y);
    }

    public Vec2 Normalize() => Normalize(this);
    public static Vec2 Normalize(Vec2 vecToNormalize)
    {
        if (vecToNormalize == Zero) return vecToNormalize;

        Boolean xIsNegative = vecToNormalize.X < 0;
        Boolean yIsNegative = vecToNormalize.Y < 0;

        if (xIsNegative) vecToNormalize.X *= -1;
        if (yIsNegative) vecToNormalize.Y *= -1;

        Decimal x = vecToNormalize.X;
        Decimal y = vecToNormalize.Y;

        if (x >= y)
        {
            y /= x;
            x /= x;
        }
        else
        {
            x /= y;
            y /= y;
        }

        if (xIsNegative) x *= -1;
        if(yIsNegative) y *= -1;

        return new Vec2(x, y);
    }

    public override String ToString()
    {
        return $"{X}, {Y}";
    }



    // Operators
    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.X + right.X, left.Y + right.Y);
    }
    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.X - right.X, left.Y - right.Y);
    }
    public static Vec2 operator *(Vec2 left, Vec2 right)
    {
        return new Vec2(left.X * right.X, left.Y * right.Y);
    }
    public static Vec2 operator /(Vec2 left, Vec2 right)
    {
        return new Vec2(left.X / right.X, left.Y / right.Y);
    }
    /// <summary>
    /// accurate up to 0.01 decimal places
    /// </summary>
    public static Boolean operator ==(Vec2 left, Vec2 right)
    {
        return Math.Abs(left.X - right.X) < (Decimal)0.01 && Math.Abs(left.Y - right.Y) < (Decimal)0.01;
    }
    public static Boolean operator !=(Vec2 left, Vec2 right)
    {
        return !(left == right);
    }
    /// <summary>
    /// accurate up to 0.01 decimal places
    /// </summary>
    public Boolean Equals(Vec2 other)
    {
        return Math.Abs(X - other.X) < (Decimal)0.01 && Math.Abs(Y - other.Y) < (Decimal)0.01;
    }
    /// <summary>
    /// accurate up to 0.01 decimal places
    /// </summary>
    public override Boolean Equals(Object? obj)
    {
        return obj is Vec2 other && Equals(other);
    }
    public override Int32 GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

}