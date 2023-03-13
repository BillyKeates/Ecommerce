Before running the tests, enter the following variables into the command line

set USERNAME1=Billy.keates@nfocus.co.uk
set PASSWORD=billykeates
set FIRSTNAME=Billy
set LASTNAME=Keates
set STREET=20 Test Road
set CITY=Test City
set POSTCODE=YO10 4DP
set PHONENO=07718 777777


The following prompts run the tests

dotnet test

dotnet test --filter "TestCategory=TestCase1"

dotnet test --filter "TestCategory=TestCase2"

dotnet test --logger "nunit;LogFilePath=test-result.xml"