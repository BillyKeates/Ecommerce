Before running the tests, enter the following variables into the command line

set SECRET_USERNAME=Billy.keates@nfocus.co.uk
set SECRET_PASSWORD=billykeates
set BASEURL = https://www.edgewordstraining.co.uk/demo-site/my-account/


The following prompts run the tests

dotnet test

dotnet test --filter "TestCategory=TestCase1"

dotnet test --filter "TestCategory=TestCase2"

dotnet test --logger "nunit;LogFilePath=test-result.xml"
