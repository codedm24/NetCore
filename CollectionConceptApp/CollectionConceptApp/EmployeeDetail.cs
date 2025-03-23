using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionConceptApp
{
    public class EmployeeIdException : Exception
    { 
        public EmployeeIdException(string message) : base(message) { 
        
        }
    }

    public struct EmployeeId : IEquatable<EmployeeId>
    { 
        private readonly char _prefix;
        private readonly int _number;

        public EmployeeId(string Id)
        {
            if (string.IsNullOrEmpty(Id)) throw new ArgumentNullException(nameof(Id));

            _prefix = (Id.ToUpper())[0];
            int numLength = Id.Length - 1;
            try
            {
                _number = int.Parse(Id.Substring(1, numLength > 6 ? 6 : numLength));
            }
            catch (FormatException)
            { 
                throw new EmployeeIdException("Invalid EmployeeId format");
            }
        }

        public readonly char prefix { get; }
        public readonly int number { get; }

        public override string ToString()
        {
            return $"{_prefix}-{_number, 6:000000}";
        }

        public override int GetHashCode() => (_number ^ _number << 16) * 0x15051505;

        public bool Equals(EmployeeId? other) => (prefix == other?.prefix && number == other?.number);

        public override bool Equals([NotNullWhen(true)] object? obj) => Equals((EmployeeId)obj!);

        public static bool operator == (EmployeeId left, EmployeeId right) => left.Equals(right);

        public static bool operator !=(EmployeeId left, EmployeeId right) => !(left == right);
    }

    internal class EmployeeDetail
    {
        public EmployeeDetail()
        { 
        
        }
    }
}
