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
    // A class that represents a clinical record element
    public class ClinicalRecord
    {
        // The type of the clinical record
        public string Type { get; set; }

        // The source of the clinical record
        public string Source { get; set; }

        // The identifier of the clinical record
        public string Identifier { get; set; }

        // The date of the clinical record
        public DateTime Date { get; set; }

        // The FHIR resource of the clinical record
        public XElement FhirResource { get; set; }

        // A constructor that takes a clinical record element as a parameter
        public ClinicalRecord(XElement clinicalRecordElement)
        {
            Type = clinicalRecordElement.Attribute("type")?.Value ?? string.Empty;
            Source = clinicalRecordElement.Attribute("sourceName")?.Value ?? string.Empty;
            Identifier = clinicalRecordElement.Attribute("identifier")?.Value ?? string.Empty;
            Date = DateTime.TryParse(clinicalRecordElement.Attribute("startDate")?.Value, out DateTime parsedDate) ? parsedDate : default;
            FhirResource = clinicalRecordElement.Element("FHIRResource") ?? new XElement("FHIRResource");
        }
    }
}
