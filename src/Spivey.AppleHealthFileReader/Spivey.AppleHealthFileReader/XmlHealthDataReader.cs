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

using System.IO.Compression;
using System.Xml.Linq;

namespace Spivey.AppleHealthFileReader
{
    // A class that represents the root element of the Apple Health export file
    public class XmlHealthDataReader
    {
        // A list of record objects
        public List<Record> Records { get; set; }

        // A list of workout objects
        public List<Workout> Workouts { get; set; }

        // A list of clinical record objects
        public List<ClinicalRecord> ClinicalRecords { get; set; }

        // A constructor that takes the path of the Apple Health export ZIP file path as a parameter
        public XmlHealthDataReader(string zipPath)
        {
            // Extract the XML file from the ZIP archive
            string xmlPath = ExtractXmlFile(zipPath);

            // Load the XML document
            XDocument doc = XDocument.Load(xmlPath);

            if (doc.Root== null )
                throw new KeyNotFoundException("Root element is missing from the XML file");

            // Get the root element
            XElement root = doc.Root;

            // Parse the record elements
            Records = ParseRecords(root);

            // Parse the workout elements
            Workouts = ParseWorkouts(root);

            // Parse the clinical record elements
            ClinicalRecords = ParseClinicalRecords(root);
        }

        // A constructor that takes an Apple Health export file XML document as a parameter
        public XmlHealthDataReader(XDocument document)
        {
            if (document.Root == null)
                throw new KeyNotFoundException("Root element is missing from the XML file");

            // Get the root element
            XElement root = document.Root;

            // Parse the record elements
            Records = ParseRecords(root);

            // Parse the workout elements
            Workouts = ParseWorkouts(root);

            // Parse the clinical record elements
            ClinicalRecords = ParseClinicalRecords(root);
        }

        // A method that extracts the XML file from the ZIP archive and returns its path
        private string ExtractXmlFile(string zipPath, string exportFileName = "export.xml")
        {
            // Create a temporary folder
            string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            // Extract the ZIP archive to the temporary folder
            ZipFile.ExtractToDirectory(zipPath, tempFolder);

            // Get the path of the XML file
            string xmlPath = Directory.GetFiles(tempFolder, exportFileName, SearchOption.AllDirectories).FirstOrDefault();

            if (xmlPath == null)
                throw new FileNotFoundException($"{exportFileName} is missing from the .zip file");

            // Return the path of the XML file
            return xmlPath;
        }

        // A method that parses the record elements and returns a list of record objects
        private List<Record> ParseRecords(XElement root)
        {
            // Create an empty list of record objects
            List<Record> records = new List<Record>();

            // Get all the record elements
            IEnumerable<XElement> recordElements = root.Elements("Record");

            // Loop through each record element
            foreach (XElement recordElement in recordElements)
            {
                // Create a record object from the record element
                Record record = new Record(recordElement);

                // Add the record object to the list
                records.Add(record);
            }

            // Return the list of record objects
            return records;
        }

        // A method that parses the workout elements and returns a list of workout objects
        private List<Workout> ParseWorkouts(XElement root)
        {
            // Create an empty list of workout objects
            List<Workout> workouts = new List<Workout>();

            // Get all the workout elements
            IEnumerable<XElement> workoutElements = root.Elements("Workout");

            // Loop through each workout element
            foreach (XElement workoutElement in workoutElements)
            {
                // Create a workout object from the workout element
                Workout workout = new Workout(workoutElement);

                // Add the workout object to the list
                workouts.Add(workout);
            }

            // Return the list of workout objects
            return workouts;
        }

        // A method that parses the clinical record elements and returns a list of clinical record objects
        private List<ClinicalRecord> ParseClinicalRecords(XElement root)
        {
            // Create an empty list of clinical record objects
            List<ClinicalRecord> clinicalRecords = new List<ClinicalRecord>();

            // Get all the clinical record elements
            IEnumerable<XElement> clinicalRecordElements = root.Elements("ClinicalRecord");

            // Loop through each clinical record element
            foreach (XElement clinicalRecordElement in clinicalRecordElements)
            {
                // Create a clinical record object from the clinical record element
                ClinicalRecord clinicalRecord = new ClinicalRecord(clinicalRecordElement);

                // Add the clinical record object to the list
                clinicalRecords.Add(clinicalRecord);
            }

            // Return the list of clinical record objects
            return clinicalRecords;
        }
    }
}