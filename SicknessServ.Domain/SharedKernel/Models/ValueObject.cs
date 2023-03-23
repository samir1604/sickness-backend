namespace SicknessServ.Domain.SharedKernel.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(ValueObject? other)
    {
        if(other is null || other.GetType() != GetType())
            return false;

        var thisValues = GetAtomicValues().GetEnumerator();
        var otherValues = other.GetAtomicValues().GetEnumerator();

        while(thisValues?.MoveNext() == true && otherValues?.MoveNext() == true)
        {
            if(thisValues.Current?.Equals(otherValues.Current) == false)
                return false;
        }

        return thisValues?.MoveNext() == false && otherValues?.MoveNext() == false;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ValueObject);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
        .Select(x => (x?.GetHashCode()) ?? 0)
        .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left?.Equals(right) ?? false;
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}