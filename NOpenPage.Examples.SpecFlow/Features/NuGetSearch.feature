Feature: NuGetOrgSearch
	Let's click through the nuget home page to provide a simple example of NOpenPage usage.

Scenario: Click through the nuget home page
	Given I have opened a nuget home page
	When I search for 'Selenium'
	Then I can see 20 packages

Scenario: Custom WebElementResolver
    Given I have opened a nuget home page
    Then it should use custom web element resolver for configured page control
