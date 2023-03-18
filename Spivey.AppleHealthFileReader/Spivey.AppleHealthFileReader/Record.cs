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
    // A class that represents a record element
    public class Record
    {
        // The type of the record
        public string Type { get; set; }

        // The source of the record
        public string Source { get; set; }

        // The unit of the record
        public string Unit { get; set; }

        // The value of the record
        public string Value { get; set; }

        // The date of the record
        public DateTime Date { get; set; }

        // A constructor that takes a record element as a parameter
        public Record(XElement recordElement)
        {
            Type = recordElement.Attribute("type")?.Value ?? string.Empty;
            Source = recordElement.Attribute("sourceName")?.Value ?? string.Empty;
            Unit = recordElement.Attribute("unit")?.Value ?? string.Empty;
            Value = recordElement.Attribute("value")?.Value ?? string.Empty;
            DateTime.TryParse(recordElement.Attribute("creationDate")?.Value, out DateTime parsedDate);
            Date = parsedDate;
        }
    }
}
