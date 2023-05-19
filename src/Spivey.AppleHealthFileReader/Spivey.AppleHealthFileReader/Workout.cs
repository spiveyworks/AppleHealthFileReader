#region License
// Copyright (c) 2023 Michael Spivey
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System.Xml.Linq;

namespace Spivey.AppleHealthFileReader
{
    // A class that represents a workout element
    public class Workout
    {
        // The type of the workout
        public string Type { get; set; }

        // The source of the workout
        public string Source { get; set; }

        // The duration of the workout
        public double Duration { get; set; }

        // The duration unit of the workout
        public string DurationUnit { get; set; }

        // The total distance of the workout
        public double TotalDistance { get; set; }

        // The total distance unit of the workout
        public string TotalDistanceUnit { get; set; }

        // The total energy burned of the workout
        public double TotalEnergyBurned { get; set; }

        // The total energy burned unit of the workout
        public string TotalEnergyBurnedUnit { get; set; }

        // The date of the workout
        public DateTime Date { get; set; }

        // A constructor that takes a workout element as a parameter
        public Workout(XElement workoutElement)
        {
            Type = workoutElement.Attribute("workoutActivityType")?.Value ?? string.Empty;
            Source = workoutElement.Attribute("sourceName")?.Value ?? string.Empty;
            Duration = double.TryParse(workoutElement.Attribute("duration")?.Value, out double parsedDuration) ? parsedDuration : default;
            DurationUnit = workoutElement.Attribute("durationUnit")?.Value ?? string.Empty;
            TotalDistance = double.TryParse(workoutElement.Attribute("totalDistance")?.Value, out double parsedTotalDistance) ? parsedTotalDistance : default;
            TotalDistanceUnit = workoutElement.Attribute("totalDistanceUnit")?.Value ?? string.Empty;
            TotalEnergyBurned = double.TryParse(workoutElement.Attribute("totalEnergyBurned")?.Value, out double parsedTotalEnergyBurned) ? parsedTotalEnergyBurned : default;
            TotalEnergyBurnedUnit = workoutElement.Attribute("totalEnergyBurnedUnit")?.Value ?? string.Empty;
            DateTime.TryParse(workoutElement.Attribute("startDate")?.Value, out DateTime parsedDate);
            Date = parsedDate;
        }
    }
}
