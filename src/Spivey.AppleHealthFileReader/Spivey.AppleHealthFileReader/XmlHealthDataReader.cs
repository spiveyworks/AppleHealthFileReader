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
using System.Xml;

namespace Spivey.AppleHealthFileReader
{
    // A class that represents the root element of the Apple Health export file
    public class XmlHealthDataReader
    {
        // A constructor that takes the path of the Apple Health export ZIP file path as a parameter
        public AppleHealthData Load(string zipPath, string exportFileName = "export.xml")
        {
            using ZipArchive archive = ZipFile.OpenRead(zipPath);
            ZipArchiveEntry? entry = archive.Entries
                .FirstOrDefault(e => e.Name.Equals(exportFileName, StringComparison.OrdinalIgnoreCase));

            if (entry == null)
                throw new FileNotFoundException($"{exportFileName} is missing from the .zip file");

            using Stream stream = entry.Open();
            XDocument doc = XDocument.Load(stream, LoadOptions.None);
            return Load(doc);
        }

        // Load an Apple Health export from an XML stream
        public AppleHealthData Load(Stream xmlStream)
        {
            XDocument doc = XDocument.Load(xmlStream, LoadOptions.None);
            return Load(doc);
        }

        // A constructor that takes an Apple Health export file XML document as a parameter
        public AppleHealthData Load(XDocument document)
        {
            if (document.Root == null)
                throw new KeyNotFoundException("Root element is missing from the XML file");

            // Get the root element
            XElement root = document.Root;
            AppleHealthData data = new AppleHealthData();

            // Parse the record elements
            data.Records = ParseRecords(root);

            // Parse the workout elements
            data.Workouts = ParseWorkouts(root);

            // Parse the clinical record elements
            data.ClinicalRecords = ParseClinicalRecords(root);
            return data;
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
            return root
                .Elements("Record")
                .AsParallel()
                .Select(e => new Record(e))
                .ToList();
        }

        // A method that parses the workout elements and returns a list of workout objects
        private List<Workout> ParseWorkouts(XElement root)
        {
            return root
                .Elements("Workout")
                .AsParallel()
                .Select(e => new Workout(e))
                .ToList();
        }

        // A method that parses the clinical record elements and returns a list of clinical record objects
        private List<ClinicalRecord> ParseClinicalRecords(XElement root)
        {
            return root
                .Elements("ClinicalRecord")
                .AsParallel()
                .Select(e => new ClinicalRecord(e))
                .ToList();
        }
    }
}