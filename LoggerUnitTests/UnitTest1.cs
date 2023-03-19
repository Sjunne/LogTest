using LogUsers.Builder;


namespace LoggerUnitTests
{
    public class UnitTest1
    {
        private string _fileDestination;

        //**Warning, unit test directory will bea cleaned every run - DO NOT ADD SENSITIVE DOCUMENTS TO FILEDESTINATION**//
        [Fact]
        public void TestCallToAsyncLog()
        {
            //Arrange
            _fileDestination = @"C:\LogTestUnitTest";
            //Directory.Delete(_fileDestination, true);
            var logger = new LoggerBuilder()
               .LogToFile(_fileDestination)
               .Build();

            //Act
            logger.WriteSingleItem("UnitTestString");
            Thread.Sleep(100);
            logger.StopWithFlush();
            //Assert

            //**Checking if File exists
            var directory = Directory.GetFiles(_fileDestination);
            var file = directory.First();
            var fileExists = File.Exists(file);
            Assert.True(fileExists, $"The file {file} exists");

            //**Checking if file content are satifisfying
            using (StreamReader sr = new StreamReader(file))
            {
                string contents = sr.ReadToEnd();
                var fileContents = contents.Contains("UnitTestString");
                Assert.True(fileContents, $"The file {file} contains 'UnitTestString'");
            }
        }

        [Fact]
        public void TestNewFilesCreatedIfNewDay()
        {

        }

        [Fact]
        public void TestStopBehaviourWithoutFlush()
        {

        }

        [Fact]
        public void TestStopBehaviourWithFlush()
        {

        }
    }
}