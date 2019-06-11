// This class provides a list of measurements to be selected from

using System.Collections.Generic;

namespace Pandalicious.Services
{
    public static class UnitsOfMeasurement
    {
        public static List<string> UnitsVolume { get; } = new List<string>()
        {
            "cup",
            "oz",
            "tsp",
            "Tbs",
            "doz",
            "gal",
            "qt"
        };

        public static List<string> UnitsWeight { get; } = new List<string>()
        {
            "lb",
            "kg",
            "g"
        };
    }
}