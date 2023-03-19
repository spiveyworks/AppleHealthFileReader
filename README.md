# AppleHealthFileReader
A c# package for reading exported Apple Health Zip files that contain health records, workouts and clinical records into memory. Use LINQ to query the data to generate your own health analytics of your data. This project is not affiliated with Apple in any way.

## Download Your Apple Health Data
To download your Apple Health data, go to your Apple Health app on your iPhone and click on the profile icon in the top right corner. Then click on "Export Health Data". This will generate a zip file that you can download to your computer. This zip file contains all of your health data in XML format. [See Apple's documentation for more information](https://support.apple.com/guide/iphone/share-your-health-data-iph5ede58c3d/16.0/ios/16.0).

## Usage
To use this package, you will need to download your Apple Health data zip file. Then you can use the following code to read the data into memory.

	var reader = new AppleHealthFileReader(@"C:\Users\me\Downloads\export.zip");
	var myHealthRecords = reader.Records;
	var myClinicalRecords = reader.ClinicalRecords;
	var myWorkouts = reader.Workouts;

Or if you want to read the data from an XDocument:

	XDocument xdoc = XDocument.Load(@"C:\Users\me\Downloads\export.xml");
	var reader = new AppleHealthFileReader(xdoc);
	var myHealthRecords = reader.Records;
	var myClinicalRecords = reader.ClinicalRecords;
	var myWorkouts = reader.Workouts;

## Related Packages
* [Spivey.Health](https://github.com/spiveyworks/Spivey.Health) can be used to convert Apple Health data into a normalized and more efficient data structure.